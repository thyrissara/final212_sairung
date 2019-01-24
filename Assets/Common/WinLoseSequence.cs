using UnityEngine;

public enum SequenceType
{
    Win,
    Lose
}

public class WinLoseSequence : MonoBehaviour
{
    public PopupSequence winSequence;
    public PopupSequence loseSequence;


    public void StartSequence(SequenceType sequenceType)
    {
        if (sequenceType == SequenceType.Win)
        {
            winSequence.StartSequence();
        }
        else if (sequenceType == SequenceType.Lose)
        {
            loseSequence.StartSequence();
        }
    }
}
