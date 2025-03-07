using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundGenerator : MonoBehaviour
{
    public float frequency = 440f; // Frequência em Hz
    public float newFrequency;
    public float gain = 0.5f; // Ganho do som

    private float increment;
    private float phase;
    private float samplingFrequency = 48000f;

    public TMP_Text colorFrequencyText;
    public TMP_Text frequencyText;

    // Estrutura para armazenar os intervalos de frequência e os valores correspondentes
    private struct FrequencyRange
    {
        public float min;
        public float max;
        public float newFrequency;
    }

    // Lista de intervalos de frequência e seus valores correspondentes
    FrequencyRange[] ranges = {
        new FrequencyRange { min = 405 / 3, max = 480 / 3, newFrequency = 440 / 3 },
        new FrequencyRange { min = 480 / 3, max = 510 / 3, newFrequency = 493.88f / 3 },
        new FrequencyRange { min = 510 / 3, max = 570 / 3, newFrequency = 523.25f / 3 },
        new FrequencyRange { min = 570 / 3, max = 640 / 3, newFrequency = 587.33f / 3 },
        new FrequencyRange { min = 640 / 3, max = 680 / 3, newFrequency = 659.26f / 3 },
        new FrequencyRange { min = 680 / 3, max = 770 / 3, newFrequency = 698.46f / 3 },
        new FrequencyRange { min = 770 / 3, max = 870 / 3, newFrequency = 783.99f / 3 },
        new FrequencyRange { min = 870 / 3, max = 900 / 3, newFrequency = 880 / 3 },

        new FrequencyRange { min = 405 / 2, max = 480 / 2, newFrequency = 440 / 2 },
        new FrequencyRange { min = 480 / 2, max = 510 / 2, newFrequency = 493.88f / 2 },
        new FrequencyRange { min = 510 / 2, max = 570 / 2, newFrequency = 523.25f / 2 },
        new FrequencyRange { min = 570 / 2, max = 640 / 2, newFrequency = 587.33f / 2 },
        new FrequencyRange { min = 640 / 2, max = 680 / 2, newFrequency = 659.26f / 2 },
        new FrequencyRange { min = 680 / 2, max = 770 / 2, newFrequency = 698.46f / 2 },
        new FrequencyRange { min = 770 / 2, max = 870 / 2, newFrequency = 783.99f / 2 },
        new FrequencyRange { min = 870 / 2, max = 900 / 2, newFrequency = 880 / 2 },

        new FrequencyRange { min = 405, max = 480, newFrequency = 440 },
        new FrequencyRange { min = 480, max = 510, newFrequency = 493.88f },
        new FrequencyRange { min = 510, max = 570, newFrequency = 523.25f },
        new FrequencyRange { min = 570, max = 640, newFrequency = 587.33f },
        new FrequencyRange { min = 640, max = 680, newFrequency = 659.26f },
        new FrequencyRange { min = 680, max = 770, newFrequency = 698.46f },
        new FrequencyRange { min = 770, max = 870, newFrequency = 783.99f },
        new FrequencyRange { min = 870, max = 900, newFrequency = 880 },

        new FrequencyRange { min = 405 * 2, max = 480 * 2, newFrequency = 440 * 2 },
        new FrequencyRange { min = 480 * 2, max = 510 * 2, newFrequency = 493.88f * 2 },
        new FrequencyRange { min = 510 * 2, max = 570 * 2, newFrequency = 523.25f * 2 },
        new FrequencyRange { min = 570 * 2, max = 640 * 2, newFrequency = 587.33f * 2 },
        new FrequencyRange { min = 640 * 2, max = 680 * 2, newFrequency = 659.26f * 2 },
        new FrequencyRange { min = 680 * 2, max = 770 * 2, newFrequency = 698.46f * 2 },
        new FrequencyRange { min = 770 * 2, max = 870 * 2, newFrequency = 783.99f * 2 },
        new FrequencyRange { min = 870 * 2, max = 900 * 2, newFrequency = 880 * 2 }
    };

    void OnAudioFilterRead(float[] data, int channels)
    {
        float _newFrequency = CalculateNewFrequency(frequency);
        increment = _newFrequency * 2f * Mathf.PI / samplingFrequency;

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

    // Método para calcular a nova frequência com base nos intervalos
    private float CalculateNewFrequency(float frequency)
    {
        for (int i = 1; i <= 4; i++)
        {
            foreach (var range in ranges)
            {
                if (frequency >= range.min / i && frequency <= range.max / i)
                {
                    return range.newFrequency / i;
                }
            }
        }

        return frequency; // Retorna a frequência original se não estiver em nenhum intervalo
    }

    public void Update()
    {
        colorFrequencyText.text = frequency.ToString("Color Frequency: 0");
        frequencyText.text = CalculateNewFrequency(frequency).ToString("Frequency: 0");

    }
}