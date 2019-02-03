using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FallingDown : MonoBehaviour
{
    public enum FruitType
    {
        Orange,
        Banana,
        Rock
    }

    public float fallDownSpeed;
    public UnityEvent touchEvent;
    public bool noDestroy;
    public FruitType fruitType;
    public RectTransform rectTransform;
    public GameObject countNumber;
    public Canvas mainCanvas;

    public void Update()
    {
        //Vector3 pos = this.transform.position;
        //pos.y -= fallDownSpeed;
        //this.transform.position = pos;

        Vector2 ap = rectTransform.anchoredPosition;
        ap.y -= fallDownSpeed;
        rectTransform.anchoredPosition = ap;

        float destroyY = GameObject.Find("DestroyLine").transform.position.y;
        if (this.transform.position.y < destroyY && !noDestroy)
        {
            GameObject.Destroy(this.gameObject);
            GameObject.FindObjectOfType<Game2Logic>().OutOfBound(fruitType == FruitType.Rock ? true : false);
        }
    }

    public void Touched()
    {
        touchEvent.Invoke();
        GameObject number = GameObject.Instantiate(countNumber, this.transform.position, Quaternion.identity, mainCanvas.transform);
        var tmp = number.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var logic = GameObject.FindObjectOfType<Game2Logic>();

        if (fruitType == FruitType.Banana)
        {
            tmp.text = (logic.collectedBanana).ToString();
        }
        else if (fruitType == FruitType.Orange)
        {
            tmp.text = (logic.collectedOrange).ToString();
        }

        GameObject.Destroy(this.gameObject);
    }
}
