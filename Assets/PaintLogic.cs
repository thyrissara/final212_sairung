﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public enum PaintState
{
    Normal,
    Brush,
    Eraser
}

public class PaintLogic : MonoBehaviour
{
    public PaintState paintState;
    public GameObject brush;
    public GameObject eraser;
    public Color selectedColor;

    public ColorPalette[] colorPalettes;

    public GameObject image1;
    public GameObject image2;

    public RenderTexture paintingTexture;

    public void Start()
    {
        if( Random.value < 0.5f)
        {
            image1.SetActive(true);
            image2.SetActive(false);
        }
        else
        {
            image1.SetActive(false);
            image2.SetActive(true);
        }
    }

    [ContextMenu("Save Image")]
    public void SaveImage()
    {
        var saved = RenderTexture.active;
        RenderTexture.active = paintingTexture;

        Texture2D t2d = new Texture2D(paintingTexture.width, paintingTexture.height, TextureFormat.ARGB32, mipChain: false);
        t2d.ReadPixels(new Rect(0, 0, paintingTexture.width, paintingTexture.height), 0, 0);

        RenderTexture.active = saved;

        byte[] pngBytes = t2d.EncodeToPNG();

        Debug.Log("SAVING " + Application.persistentDataPath);
        File.WriteAllBytes(Application.persistentDataPath + "/test.png", pngBytes);

    }

    public void BrushTouched()
    {
        paintState = PaintState.Brush;
        brush.SetActive(false);
        eraser.SetActive(true);
    }

    public void EraserTouched()
    {
        paintState = PaintState.Eraser;
        brush.SetActive(true);
        eraser.SetActive(false);
        selectedColor = Color.white;
        foreach(ColorPalette cp in colorPalettes)
        {
            cp.Deselect();
        }
    }

    public void SelectColor(ColorPalette selectedPalette)
    {
        if(paintState == PaintState.Eraser)
        {
            return;
        }

        selectedColor = selectedPalette.color;
        foreach(ColorPalette cp in colorPalettes)
        {
            cp.Deselect();
        }
        selectedPalette.Select();
    }
}
