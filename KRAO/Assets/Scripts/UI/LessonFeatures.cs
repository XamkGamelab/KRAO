using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LessonFeatures : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    public void GenerateButtons(LessonFeature[] _features)
    {
        foreach(LessonFeature _feature in _features)
        {
            var _newFeature = Instantiate(buttonPrefab, transform);
            Button _featureButton = _newFeature.GetComponent<Button>();
            _featureButton.onClick = _feature.ButtonEvent;
            _newFeature.GetComponentInChildren<TMP_Text>(true).text = _feature.ButtonPrompt;
        }
    }

    public void DestroyButtons()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
