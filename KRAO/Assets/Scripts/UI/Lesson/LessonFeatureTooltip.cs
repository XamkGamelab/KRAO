using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LessonFeatureTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private LessonFeatures lessonFeatures => FindFirstObjectByType<LessonFeatures>();
    private GameObject tooltip { get; set; }
    private CanvasGroup tooltipCanvasGroup;
    private string tooltipText { get; set; }

    private void Start()
    {
        tooltip = lessonFeatures.Tooltip;
        tooltipCanvasGroup = tooltip.GetComponent<CanvasGroup>();
    }

    public void SetValues(string _text)
    {
        tooltipText = _text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //show tooltip with correct text
        tooltipCanvasGroup.alpha = 1.0f;
        tooltipCanvasGroup.interactable = true;
        tooltipCanvasGroup.blocksRaycasts = true;
        tooltip.GetComponentInChildren<TMP_Text>(true).text = tooltipText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //hide tooltip
        tooltipCanvasGroup.alpha = 0.0f;
        tooltipCanvasGroup.interactable = false;
        tooltipCanvasGroup.blocksRaycasts = false;
    }
}