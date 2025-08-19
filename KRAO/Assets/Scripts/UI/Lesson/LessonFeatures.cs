using TMPro;
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


    private float buttonParentPosY = 0;

    private void Start()
    {
        buttonParentPosY = buttonParent.position.y;
    }

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
            _featureButton.GetComponent<LessonFeatureTooltip>().SetValues(_feature.ButtonPrompt);

            //_newFeature.GetComponentInChildren<TMP_Text>(true).text = _feature.ButtonPrompt;
            //Tooltip.GetComponentInChildren<TMP_Text>(true).text = _feature.ButtonPrompt;
        }

        UIRectScaling.ScaleHeightUpByChildren(buttonParent, sidePanel, buttonParentPosY);
    }

    public void DestroyButtons()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonParent.transform.GetChild(i).gameObject);
        }
    }
}