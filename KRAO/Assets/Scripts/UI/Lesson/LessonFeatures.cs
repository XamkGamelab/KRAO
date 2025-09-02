using UnityEngine;
using UnityEngine.UI;

public class LessonFeatures : MonoBehaviour
{
    private UIRectScaling UIRectScaling => FindFirstObjectByType<UIRectScaling>();
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    [SerializeField] private RectTransform sidePanel;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private RectTransform buttonParent;
    public GameObject Tooltip;

    public void EnableLessonFeatures(LessonFeature[] _features)
    {
        GenerateButtons(_features);

        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisableLessonFeatures()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        DestroyButtons();
    }

    public void GenerateButtons(LessonFeature[] _features)
    {
        foreach(LessonFeature _feature in _features)
        {
            var _newFeature = Instantiate(buttonPrefab, buttonParent.transform);
            Button _featureButton = _newFeature.GetComponent<Button>();
            _featureButton.onClick = _feature.ButtonEvent;
            _featureButton.GetComponent<LessonFeatureTooltip>().SetTooltipText(_feature.ButtonPrompt);
        }

        // Scale side panel height
        UIRectScaling.ScaleHeightUpByChildren(buttonParent, sidePanel, 0);
    }

    public void DestroyButtons()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonParent.transform.GetChild(i).gameObject);
        }
    }
}