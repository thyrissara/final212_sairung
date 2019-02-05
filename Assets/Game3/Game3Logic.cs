using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

    public enum Game3State
    {
        SawState,
        WateringState,
    }

public class Game3Logic : MonoBehaviour
{
    public Game3State currentState;
    [Space]
    public Camera mainCamera;

    public RectTransform canvasRectangle;

    public RectTransform leftBranchRect;
    public RectTransform rightBranchRect;
    public RectTransform birdBranchRect;

    [Space]

    public Animation warningAnimation;
    public AudioSource warningSource;

    [Space]

    public RectTransform leaf1;
    public FloatToColor leaf1Ftc;
    public RectTransform leaf2;
    public FloatToColor leaf2Ftc;

    [Space]

    public TouchFollower[] sawFollowers;
    public TouchFollower[] waterFollowers;

    public TouchFollower[] rememberFollowerPerFinger;

    public float wateringSpeed = 0.01f;

    public void Start()
    {
        rememberFollowerPerFinger = new TouchFollower[2];
        foreach(var saw in sawFollowers)
        {
            saw.Hide();
        }
        foreach(var water in waterFollowers)
        {
            water.Hide();
        }
    }

    public void Down(BaseEventData baseEventData)
    {
        PointerEventData ped = (PointerEventData)baseEventData;
        Debug.Log(ped.pointerId);

        if(ped.pointerId == -1)
        {
            ped.pointerId = 0;
        }

        //if over 1, return.
        if(ped.pointerId >= sawFollowers.Length)
        {
            return;
        }

        if (currentState == Game3State.SawState)
        {
            var selected = sawFollowers[ped.pointerId];
            selected.Show();
            UpdateFollowerToPed(selected, ped);
            rememberFollowerPerFinger[ped.pointerId] = selected;
        }
        else if (currentState == Game3State.WateringState)
        {
            var selected = waterFollowers[ped.pointerId];
            selected.Show();
            UpdateFollowerToPed(selected, ped);
            rememberFollowerPerFinger[ped.pointerId] = selected;
        }
    }

    public void Up(BaseEventData baseEventData)
    {
        PointerEventData ped = (PointerEventData) baseEventData;

        if(ped.pointerId == -1)
        {
            ped.pointerId = 0;
        }

        if(ped.pointerId >= sawFollowers.Length)
        {
            return;
        }

        if (currentState == Game3State.SawState)
        {
            sawFollowers[ped.pointerId].Hide();
        }
        else if (currentState == Game3State.WateringState)
        {
            waterFollowers[ped.pointerId].Hide();
        }
    }

    private void UpdateFollowerToPed(TouchFollower tf, PointerEventData pointerEventData)
    {
        bool succeed = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectangle, pointerEventData.position, mainCamera, out Vector2 outLocalPoint);
        outLocalPoint = outLocalPoint + new Vector2(canvasRectangle.rect.width / 2f, canvasRectangle.rect.height / 2f);
        tf.GetComponent<RectTransform>().anchoredPosition = outLocalPoint;
        //Debug.Log($"{pointerEventData.position} {outLocalPoint}");
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData ped = (PointerEventData)baseEventData; //Check if at saw phase or not
        // Debug.Log( string.Join( " - " , ped.hovered.Select(x => x.name)) );
        // Debug.Log("PED Point " + ped.position);
        if (currentState == Game3State.SawState)
        {
            if (CheckPedInRect(leftBranchRect, ped))
            {
                rememberFollowerPerFinger[ped.pointerId].PlayParticle();
            }
            else if (CheckPedInRect(rightBranchRect, ped))
            {
                rememberFollowerPerFinger[ped.pointerId].PlayParticle();
            }
            else if (CheckPedInRect(birdBranchRect, ped))
            {
                if (warningAnimation.isPlaying == false)
                {
                    warningAnimation.Play();
                    warningSource.Play();
                }
            }
            else
            {
                rememberFollowerPerFinger[ped.pointerId].StopParticle();
            }
        }
        else if (currentState == Game3State.WateringState)
        {
            if (CheckPedInRect(leaf1, ped))
            {
                rememberFollowerPerFinger[ped.pointerId].PlayParticle();
                leaf1Ftc.floatValue += wateringSpeed;
            }
            else if (CheckPedInRect(leaf2, ped))
            {
                rememberFollowerPerFinger[ped.pointerId].PlayParticle();
                leaf2Ftc.floatValue += wateringSpeed;
            }
            else
            {
                rememberFollowerPerFinger[ped.pointerId].StopParticle();
            }
        }

        if(ped.pointerId == -1)
        {
            ped.pointerId = 0;
        }

        UpdateFollowerToPed(rememberFollowerPerFinger[ped.pointerId], ped);

        // RectTransform rectOfBranch = ped.pointerDrag.GetComponent<RectTransform>();
        // var screenPoint = RectTransformUtility.WorldToScreenPoint(mainCamera, ped.pointerCurrentRaycast.worldPosition);
        // if(RectTransformUtility.RectangleContainsScreenPoint(rectOfBranch, screenPoint))
        // {
        //     Debug.Log(ped.pointerDrag.name);
        // }

        bool CheckPedInRect(RectTransform rect, PointerEventData pointerEventData)
        {
            if (rect == null)
            {
                return false;
            }

            return RectTransformUtility.RectangleContainsScreenPoint(rect, pointerEventData.position, mainCamera);
        }
    }

}


