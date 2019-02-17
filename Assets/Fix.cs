using UnityEngine;

public class Fix : MonoBehaviour
{
    public RectTransform selfRect;
    public Animation fixAnimation;
    public bool alreadyFixed;

    public void FixIt()
    {
        if (!alreadyFixed)
        {
            fixAnimation.Play();
            alreadyFixed = true;
        }
    }
}
