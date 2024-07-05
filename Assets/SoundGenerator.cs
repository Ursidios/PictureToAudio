using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundGenerator : MonoBehaviour
{  public float frequency = 440f; // Frequência em Hz
    public float gain = 0.5f; // Ganho do som

    private float increment;
    private float phase;
    private float samplingFrequency = 48000f;

    void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2f * Mathf.PI / samplingFrequency;

        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            data[i] = gain * Mathf.Sin(phase);

            if (channels == 2)
            {
                data[i + 1] = data[i]; // Stereo: copia o canal esquerdo para o direito
            }

            if (phase > Mathf.PI * 2)
            {
                phase -= Mathf.PI * 2;
            }
        }
    }
}
