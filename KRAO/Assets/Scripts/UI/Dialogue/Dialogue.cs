using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogueData", menuName ="KRAO/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string header;
    public string speakerName;
    [TextArea(10,1000)]
    public string content;

    public Dialogue NextDialogue;
}
