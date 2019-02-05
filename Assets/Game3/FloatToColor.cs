using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class FloatToColor : MonoBehaviour
{
    public Image image;

    [Range(0f,1f)]
    public float floatValue;

    public void IncreaseFloat(float increaseBy) => floatValue += increaseBy;

    public Gradient gradient;

    public void Update()
    {
        image.color = gradient.Evaluate(floatValue);
    }
}