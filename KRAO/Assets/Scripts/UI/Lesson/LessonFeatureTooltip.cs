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
        // Get tooltip GameObject from lessonfeatures class
        tooltip = lessonFeatures.Tooltip;
        // Get CanvasGroup from the tooltip GameObject
        tooltipCanvasGroup = tooltip.GetComponent<CanvasGroup>();
    }

    public void SetTooltipText(string _text)
    {
        tooltipText = _text;
    }

    // When pointer enters lesson feature button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        //show tooltip with correct text
        tooltipCanvasGroup.alpha = 1.0f;
        tooltipCanvasGroup.interactable = true;
        tooltipCanvasGroup.blocksRaycasts = true;
        tooltip.GetComponentInChildren<TMP_Text>(true).text = tooltipText;
    }
    // When pointer exits lesson feature button area
    public void OnPointerExit(PointerEventData eventData)
    {
        //hide tooltip
        tooltipCanvasGroup.alpha = 0.0f;
        tooltipCanvasGroup.interactable = false;
        tooltipCanvasGroup.blocksRaycasts = false;
    }
}