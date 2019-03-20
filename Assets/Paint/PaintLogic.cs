using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public enum PaintState
{
    Normal,
    Brush,
    Eraser
}

public class PaintLogic : MonoBehaviour
{
    public PaintState paintState;
    public GameObject brush_hi;
    public GameObject eraser_hi;
    public Color selectedColor;

    public ColorPalette[] colorPalettes;

    public GameObject image1;
    public GameObject image2;

    public RenderTexture paintingTexture;


    public void Start()
    {
        if (UnityEngine.Random.value < 0.5f)
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
        Directory.CreateDirectory(Application.persistentDataPath + "/gallery");
        string randomName = Guid.NewGuid().ToString();

        if(image1.activeSelf == true)
        {
            randomName = "1-" + randomName;
        }
        else
        {
            randomName = "2-" + randomName;
        }

        string savePath = Application.persistentDataPath + $"/gallery/{randomName}.png";
        File.WriteAllBytes(savePath, pngBytes);
        NativeShare.Share("hi", savePath, "", "", "image/png", true, "");
    }

    public void BrushTouched()
    {
        paintState = PaintState.Brush;
        brush_hi.SetActive(true);
        eraser_hi.SetActive(false);
    }

    public void EraserTouched()
    {
        paintState = PaintState.Eraser;
        brush_hi.SetActive(false);
        eraser_hi.SetActive(true);
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

     public void AfterWinSequence()
    {
        Debug.Log("After Win");
        SceneManager.LoadScene("Home");
    }
}
