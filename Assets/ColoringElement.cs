using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColoringElement : MonoBehaviour
{
    public void Touched()
    {
        var paintLogic = GameObject.FindObjectOfType<PaintLogic>();
        if(paintLogic.paintState == PaintState.Normal || paintLogic.selectedColor == default(Color))
        {
            return;
        }

        Image image = GetComponent<Image>();
        var plogic = GameObject.FindObjectOfType<PaintLogic>();
        image.color = plogic.selectedColor;
    }
}
