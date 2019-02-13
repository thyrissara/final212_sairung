using UnityEngine;
using UnityEngine.UI;

public enum CloudState
{
    NotAccepting,
    Accepting,
    Dropping,
    Success,
}

public enum CandyType 
{
    Blue,
    Purple
}

public class Cloud : MonoBehaviour
{
    public CandyType acceptingCandyType;
    public CloudState CloudState { get; private set; }
    public RectTransform selfRect;
    public Candy candy;
    public Image cloudToRecolor;

    public Transform resetTransform;
    public float speedPerFrame = 0.1f;
    public Transform dropTransform;

    public Animator cloudAnimator;

    public Gradient colorGradient;

    public int candyGot = 0;
    public int targetCandy = 5;

    public float distanceThreshold = 10;

    public void GetCandy()
    {
        candyGot++;
        nowInThreshold = false;
        CloudState = CloudState.NotAccepting;
        cloudAnimator.SetTrigger("Chew");
        cloudAnimator.SetTrigger("StopFlashing");
        UpdateColor();
        if(candyGot == targetCandy)
        {
            cloudAnimator.SetTrigger("Success");
        }
    }

    public void Start()
    {
        UpdateColor();
    }

    public void UpdateColor()
    {
        float relativeProgress = candyGot / (float)targetCandy;
        Color result = colorGradient.Evaluate(relativeProgress);
        cloudToRecolor.color = result;
    }

    public void EnterDroppingState()
    {
        CloudState = CloudState.Dropping;
    }

    bool nowInThreshold = false;

    public void Update()
    {
        if(CloudState == CloudState.Accepting)
        {
            float distance = Vector3.Distance(this.transform.position, candy.transform.position);
            //Debug.Log("Distance " + distance);
            if( distance < distanceThreshold && nowInThreshold == false)
            {
                nowInThreshold = true;
                cloudAnimator.SetTrigger("MouthOpen");
            }
            else if(distance >= distanceThreshold && nowInThreshold == true)
            {
                Debug.Log("Closed");
                nowInThreshold = false;
                cloudAnimator.SetTrigger("MouthClose");
            }
        }
        else if(CloudState == CloudState.Dropping)
        {
            dropTransform.Translate(0, -speedPerFrame, 0);
            if(dropTransform.position.y < resetTransform.position.y)
            {
                dropTransform.localPosition = Vector3.zero;
            }
        }
    }

    public void SetToAccepting()
    {
        CloudState = CloudState.Accepting;
        cloudAnimator.SetTrigger("Flashing");
    }

    public void StopDropping()
    {
        CloudState = CloudState.Success;
    }

}
