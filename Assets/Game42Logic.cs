using UnityEngine;
using UnityEngine.EventSystems;

public class Game42Logic : MonoBehaviour
{
    public Log2[] logs;
    public Fix[] fixes;

    public void Update()
    {
        foreach (Log2 log in logs)
        {
            if (log.State == CandyState.Dragging)
            {
                PointerEventData ped = log.CurrentPointerEventData;

                foreach (Fix f in fixes)
                {
                    //Debug.Log($" {cloud.name} {CheckPedInRect(cloud.selfRect, ped)}");
                    if (
                       f.alreadyFixed == false && CheckPedInRect(f.selfRect, ped)
                    )
                    {
                        f.FixIt();
                        log.InactiveState();
                    }
                }
            }
        }
    }

    bool CheckPedInRect(RectTransform rect, PointerEventData pointerEventData)
    {
        if (rect == null)
        {
            return false;
        }

        return RectTransformUtility.RectangleContainsScreenPoint(rect, pointerEventData.position, Camera.main);
    }
}
