﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAramCube : MonoBehaviour {

    public int band;
    public float startscale, scalemultiplier;
    AudioPier AP;
    public bool useBuffer;

    void Start()
    {
        AP = FindObjectOfType<AudioPier>(); 
    }
    void Update()
    {
        if (useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AP.bandBuffer[band] * scalemultiplier) + startscale, transform.localScale.z);
        }

        if (!useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AP.freqBand[band] * scalemultiplier) + startscale, transform.localScale.z);
        }
    }
}
