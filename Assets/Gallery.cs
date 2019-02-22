using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{
    public int page = 0;

    // public Sprite[] allImages;
    // public string[] allImagePaths;

    public (Sprite sprite, string path, DateTime createTime)[] all;

    public Button leftButton;
    public Button rightButton;

    public Image leftImage;
    public Image rightImage;
    public GameObject noImageText;
    public GameObject leftImageObject;
    public GameObject rightImageObject;

    [Space]

    public Image galleryImage;
    public Image galleryImageBig;
    public GameObject text1;
    public GameObject text2;
    public GameObject viewObject;

    public int TotalPages
    {
        get{
            return Mathf.CeilToInt(all.Length / 2f);
        }
    }

    private void LoadImages()
    {
        Debug.Log("Loading image");
        string[] allImagePaths = Directory.GetFiles(Application.persistentDataPath + "/gallery", "*.png");
        //foreach(string imagePath in allImagePaths)
        Sprite[] allImages = new Sprite[allImagePaths.Length];
        DateTime[] allCreationTime = new DateTime[allImagePaths.Length];

        for (int i = 0; i < allImagePaths.Length; i++)
        {
            Texture2D t2d = new Texture2D(2, 2);

            byte[] imageBytes = File.ReadAllBytes(allImagePaths[i]);
            DateTime creationTime = File.GetCreationTime(allImagePaths[i]);

            t2d.LoadImage(imageBytes);
            Sprite createdSprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), 100);

            allImages[i] = createdSprite;
            allCreationTime[i] = creationTime;
        }

        //all = new (Sprite sprite, string path, DateTime createTime)[allImagePaths.Length];
        var list = new List<(Sprite sprite, string path, DateTime createTime)>();
        for(int i = 0 ; i < allImagePaths.Length; i++)
        {
            list.Add((allImages[i], allImagePaths[i], allCreationTime[i]));
        }

        list.Sort(new MyComparer());
        all = list.ToArray();
    }

    public struct MyComparer : IComparer<(Sprite sprite, string path, DateTime createTime)>
    {
        public int Compare((Sprite sprite, string path, DateTime createTime) a, (Sprite sprite, string path, DateTime createTime) b)
        {
            return a.createTime.CompareTo(b.createTime);
        }
    }

    public void Start()
    {
        LoadImages();
        SwitchPage();
    }

    public void Left()
    {
        page--;
        SwitchPage();
    }

    public void Right()
    {
        page++;
        SwitchPage();
    }

    private void SwitchPage()
    {
        // LR button show/hide

        if (page == 0)
        {
            leftButton.gameObject.SetActive(false);
        }
        else
        {
            leftButton.gameObject.SetActive(true);
        }

        if (page >= TotalPages - 1)
        {
            rightButton.gameObject.SetActive(false);
        }
        else
        {
            rightButton.gameObject.SetActive(true);
        }


        //display image

        if ((page * 2) + 1 > all.Length - 1)
        {
            rightImageObject.SetActive(false);

            if (all.Length == 0)
            {
                leftImageObject.SetActive(false);
                noImageText.SetActive(true);
            }
            else
            {
                leftImageObject.SetActive(true);
                noImageText.SetActive(false);
                leftImage.sprite = all[page * 2].sprite;
            }

            return;
        }
        else
        {
            rightImageObject.SetActive(true);
            leftImageObject.SetActive(true);
            noImageText.SetActive(false);

            leftImage.sprite = all[page * 2].sprite;
            rightImage.sprite = all[(page * 2) + 1].sprite;
        }
    }

    public void PressLeftImage()
    {
        View(page * 2);
    }

    public void PressRightImage()
    {
        View((page * 2) + 1);
    }

    private int rememberViewing;
    public void View(int index)
    {
        rememberViewing = index;
        galleryImage.sprite = all[index].sprite;
        galleryImageBig.sprite = all[index].sprite;
        gameObject.SetActive(false);
        viewObject.SetActive(true);
        string filePath = all[index].path;
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        Debug.Log("File name " +  fileName);
        string[] splitted = fileName.Split('-');

        if(splitted[0] == "1")
        {
            text1.SetActive(true);
            text2.SetActive(false);
        }
        else 
        {
            text1.SetActive(false);
            text2.SetActive(true);
        }
    }

    public void Delete()
    {
        File.Delete( all[rememberViewing].path );

        bool isOdd = all.Length % 2 != 0;
        bool isDeletingLast = rememberViewing == all.Length -1;

        LoadImages();
        //---

        if(isOdd && isDeletingLast)
        {
            if(page != 0)
            {
                page--;
            }
        }
        //---
        SwitchPage();
    }
}
