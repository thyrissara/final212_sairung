﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFilp : MonoBehaviour
{
     public AudioSource audioSource;
    private bool touched;

    // Update is called once per frame
    public void Touched()
    {
        if (!touched)
        {
            audioSource.Play();
        }
    }
}