using UnityEngine;

public class Drop : MonoBehaviour
{
    public Cloud selfCloud;

    public void Touched()
    {
        selfCloud.StopDropping();
        this.gameObject.SetActive(false);
    }
}