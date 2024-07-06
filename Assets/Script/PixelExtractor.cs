using System.Collections.Generic;
using UnityEngine;

public class PixelExtractor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Texture2D texture;

    public List<float> frequencysOutput;
    public bool useHUE;

    public float tunerANote = 400;
    public List<float> volume;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Material material = meshRenderer.material;
        texture = material.mainTexture as Texture2D;

        float textureWidth = texture.width;
        float textureHeight = texture.height;

        float aspectRatio = textureWidth / textureHeight;

        float myScale = transform.localScale.y;
        transform.localScale = new Vector3(aspectRatio * myScale, 1 * myScale, 1 * myScale);

        if (texture == null)
        {
            Debug.LogError("Texture not assigned.");
            return;
        }
        Color[] pixels = texture.GetPixels();

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                int index = y * texture.width + x;

                Color pixelColor = pixels[index];
                float ColorFrequency = RGBToWavelength(pixelColor);

                frequencysOutput.Add(ColorFrequency);

                //Debug.Log($"Pixel at ({x}, {y}) has color: {ColorFrequency}");
            }
        }
    }


    public float RGBToWavelength(Color color)
    {
        // Normaliza os valores RGB para o intervalo [0, 1]
        float r = color.r;
        float g = color.g;
        float b = color.b;
        
        // Aproximação simples linear para conversão
        float wavelength = 0.0f;
        if(!useHUE)
        {
            if (r >= g && r >= b)
            {
                // Vermelho dominante
                wavelength = 700 - (r * 100);  // Intervalo aproximado 620-700 nm
            }
            else if (g >= r && g >= b)
            {
                // Verde dominante
                wavelength = 510 - (g * 70);  // Intervalo aproximado 495-570 nm
            }
            else
            {
                // Azul dominante
                wavelength = 450 - (b * 70);  // Intervalo aproximado 450-495 nm
            }  
            
        }
        else
        {
            float H, S, V;
            Color.RGBToHSV(color, out H, out S, out V);
            wavelength = H * (tunerANote * 2) + (S * tunerANote);
            volume.Add(V);
        }

 
    
        return wavelength;
    }
    // Método para converter comprimento de onda (nm) em frequência (THz)
    public float WavelengthToFrequency(float wavelengthNm)
    {
        // Converte nm para metros
        float wavelengthM = wavelengthNm * 1e-9f;
        // Velocidade da luz em m/s
        float c = 3e8f;
        // Calcula a frequência em Hz
        float frequencyHz = c / wavelengthM;
        // Converte a frequência para THz
        float frequencyTHz = frequencyHz * 1e-12f;
        return frequencyTHz;
    }

}
