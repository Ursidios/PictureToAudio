using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public SoundGenerator soundGenerator;
    public PixelExtractor pixelExtractor;

    [Space (30)]
    public bool Play;
    public float musicSpeedInicial;
    public float musicSpeed;
    public int NoteIndex;

    public TMP_Text indexText;
    // Start is called before the first frame update
    void Start()
    {
        musicSpeed = musicSpeedInicial;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        indexText.text = NoteIndex.ToString("Pixel index: 0");
        //soundGenerator.gain = pixelExtractor.volume[NoteIndex];
        soundGenerator.frequency = pixelExtractor.frequencysOutput[NoteIndex];
        if(Play)
        {
            musicSpeed -= Time.deltaTime;
            soundGenerator.gain = 0.5f;
            if(musicSpeed < 0)
            {
                musicSpeed = musicSpeedInicial;
                NoteIndex ++;
            }
        }
        else
        {
            soundGenerator.gain = 0;
        }
    }
    public void PlayMusic()
    {
        Play = true;
    }
    public void PauseMusic()
    {
        Play = false;
    }
    public void ResetMusic()
    {
        NoteIndex = 0;
    }
}
