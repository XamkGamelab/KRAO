using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class LessonFocusPoints : MonoBehaviour
{
    private UIRectScaling UIRectScaling => FindFirstObjectByType<UIRectScaling>();
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    [SerializeField] private RectTransform sidePanel;
    [SerializeField] private GameObject focusPointButtonPrefab;
    [SerializeField] private GameObject cameraImage;
    [SerializeField] private RectTransform buttonParent;

    private float buttonParentPosY = 0;

    private void Start()
    {
        buttonParentPosY = buttonParent.position.y;
    }

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
        Instantiate(cameraImage, buttonParent);

        for(int i = 0; i < points.Length; i++)
        {
            var newButton = Instantiate(focusPointButtonPrefab, buttonParent);
            LessonFocusPointButton focusPointButton = newButton.GetComponent<LessonFocusPointButton>();
            focusPointButton.ViewPoint = points[i];
            focusPointButton.Text.text = (i + 1).ToString();
        }

        UIRectScaling.ScaleHeightUpByChildren(buttonParent, sidePanel, buttonParentPosY);
    }

    private void DestroyButtons()
    {
        for (int i = buttonParent.childCount - 1; i > -1; i--)
        {
            Destroy(buttonParent.GetChild(i).gameObject);
        }
    }
}
