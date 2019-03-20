using UnityEngine;

[CreateAssetMenu]
public class StoryText : ScriptableObject
{
    public string who;

    [Multiline]
    public string text;
    public Sprite backgroundImage;
}
