using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        musicSpeed = musicSpeedInicial;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        soundGenerator.gain = pixelExtractor.volume[NoteIndex];
        soundGenerator.frequency = pixelExtractor.frequencysOutput[NoteIndex];
        if(Play)
        {
            musicSpeed -= Time.deltaTime;

            if(musicSpeed < 0)
            {
                musicSpeed = musicSpeedInicial;
                NoteIndex ++;
            }
        }
    }
}
