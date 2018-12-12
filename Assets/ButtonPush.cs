using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPush : MonoBehaviour
{
    [SerializeField] private Sprite unpressed;
    [SerializeField] private Sprite pressed;

    [SerializeField] private Image buttonImage;
    [SerializeField] private Animation showAnimation;

    public void Awake()
    {
        showAnimation.Play("ButtonHide");
    }

    public void ShowButton()
    {
        showAnimation.Play("ButtonShow");
    }

    public void Press()
    {
        buttonImage.sprite = pressed;
    }

    public void Unpress()
    {
        buttonImage.sprite = unpressed;
    }


}
