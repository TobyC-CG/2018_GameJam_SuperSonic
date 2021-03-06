﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPier : MonoBehaviour {

    AudioSource AS;
    public float[] samples = new float[512];
    public float[] freqBand = new float[8];
    public float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];



	void Start () {
        AS = GetComponent<AudioSource>();
	}
	
	void Update () {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
    }

    void BandBuffer()
    {
        for(int g = 0; g < 8; g++)
        {
            if(freqBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = freqBand[g];
                bufferDecrease[g] = 0.005f;
            }

            if(freqBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= freqBand[g];
                bufferDecrease[g] *= 1.2f;

            }

        }
    }

    void GetSpectrumAudioSource()
    {
        AS.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands()
    {
        float average = 0;
        int count = 0;


        for(int i = 0; i<8; i++)
        {
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++) {
                average += samples[count] * (count + 1);
                    count++;
            }

            average /= count;
            freqBand[i] = average * 10;

        }
    }
}
