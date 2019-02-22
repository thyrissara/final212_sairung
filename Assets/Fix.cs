using UnityEngine;

public class Fix : MonoBehaviour
{
    public RectTransform selfRect;
    public bool alreadyFixed;
    public Animator FixLog;


    public void FixIt()
    {
        if (!alreadyFixed)
        {
            FixLog.SetTrigger("Logfix");
            alreadyFixed = true;
        }
    }
}
