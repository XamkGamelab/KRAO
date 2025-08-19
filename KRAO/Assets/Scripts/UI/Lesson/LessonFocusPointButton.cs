using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class LessonFocusPointButton : MonoBehaviour
{
    public LessonViewPoint ViewPoint;
    public TMP_Text Text => GetComponentInChildren<TMP_Text>(true);
    private LessonManager lessonManager => FindFirstObjectByType<LessonManager>();
    public void SwitchFocus()
    {
        lessonManager.SetFocus(ViewPoint);
    }
}
