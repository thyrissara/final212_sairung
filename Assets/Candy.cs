using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CandyState
{
    NotDragging,
    Dragging,
    Inactive,
}

public class Candy : MonoBehaviour
{
    public RectTransform canvasRectangle;
    public RectTransform selfRectTransform;
    public Camera mainCamera;
    public CandyType candyType;

    [Space]

    public Cloud[] clouds;
    public TextMeshProUGUI numberText;

    [Space]

    public float smoothTime = 1;
    public float outOfScreenOffsetY = 100;

    private Vector3 startingPosition;
    private Vector3 currentVelocity;

    public CandyState State { get; private set; }

    public void Start()
    {
        startingPosition = this.transform.position;
    }

    public void Update()
    {
        if(State == CandyState.NotDragging)
        {
            Vector3 result = Vector3.SmoothDamp(this.transform.position, startingPosition, ref currentVelocity, smoothTime);

            this.transform.position = result;
        }
        else if(State == CandyState.Inactive)
        {
            this.transform.position = startingPosition - new Vector3(0, outOfScreenOffsetY, 0);
        }

        numberText.text = clouds.Sum(x => x.candyGot).ToString();
    }

    public void InactiveState() => this.State = CandyState.Inactive;
    public void RestoreToActive() => this.State = CandyState.NotDragging;

    public void DragUp(BaseEventData baseEventData)
    {
        if(State == CandyState.Inactive)
        {
            return;
        }
        State = CandyState.NotDragging;
    }

    public PointerEventData CurrentPointerEventData { get; private set; }

    public void Drag(BaseEventData baseEventData)
    {
        if(State == CandyState.Inactive)
        {
            return;
        }

        PointerEventData ped = (PointerEventData)baseEventData; 
        UpdateToPointerEventData(ped);
        State = CandyState.Dragging;

        CurrentPointerEventData = ped;
    }

    private void UpdateToPointerEventData(PointerEventData pointerEventData)
    {
        bool succeed = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectangle, pointerEventData.position, Camera.main, out Vector2 outLocalPoint);
        outLocalPoint = outLocalPoint + new Vector2(canvasRectangle.rect.width / 2f, canvasRectangle.rect.height / 2f);
        selfRectTransform.anchoredPosition = outLocalPoint;
    }
}
