using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    public Sprite logGood;
    public Sprite logBad;
    public Image logImage;
    public bool isBad;
    public Animation ani;
    public Game4Logic game4Logic;

    public void Touched()
    {
        if(isBad == false)
        {
            game4Logic.GetLog();
            ani.Stop();
            ani.Play();
        }
    }
     
    public void RandomizeValidOrNot()
    {
        if( Random.value < 0.5f)
        {
            logImage.sprite = logGood;
            isBad = false;
        }
        else
        {
            logImage.sprite = logBad;
            isBad = true;
        }
    }
}
