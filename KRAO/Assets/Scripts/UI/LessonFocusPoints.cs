using System.Drawing;
using UnityEngine;

public class LessonFocusPoints : MonoBehaviour
{
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    [SerializeField] private GameObject focusPointButtonPrefab;
    [SerializeField] private RectTransform buttonParent;
    
    public void EnableFocusPoints(LessonViewPoint[] points)
    {
        GenerateButtons(points);

        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisableFocusPoints()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        DestroyButtons();
    }

    private void GenerateButtons(LessonViewPoint[] points)
    {
        for(int i = 0; i < points.Length; i++)
        {
            var newButton = Instantiate(focusPointButtonPrefab, buttonParent);
            LessonFocusPointButton focusPointButton = newButton.GetComponent<LessonFocusPointButton>();
            focusPointButton.ViewPoint = points[i];
            focusPointButton.Text.text = (i + 1).ToString();
        }
    }

    private void DestroyButtons()
    {
        for (int i = buttonParent.childCount - 1; i > -1; i--)
        {
            Destroy(buttonParent.GetChild(i).gameObject);
        }
    }
}
