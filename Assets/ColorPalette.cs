using UnityEngine;
using UnityEngine.UI;

public class ColorPalette: MonoBehaviour
{
    public Color color;
    public PaintLogic paintLogic;
    public Image targetImage;
    public Sprite normalImage;
    public Sprite selectedImage;

    public void Touched()
    {
        paintLogic.SelectColor(this);
    }

    public void Select()
    {
        targetImage.sprite = selectedImage;
    }

    public void Deselect()
    {
        targetImage.sprite = normalImage;
    }

}
