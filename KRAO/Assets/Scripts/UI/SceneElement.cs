using UnityEngine;
using UnityEngine.UI;

public class SceneElement : MonoBehaviour
{
    [SerializeField] private SceneButton OpenSceneButton;
    [SerializeField] private Image sceneElementImage;
    [SerializeField] private Text sceneHeaderText;
    [SerializeField] private Text lessonsFoundText;
    [SerializeField] private Image checkMark;

    public int SceneIndex { get; private set; }

    // Set scene index, header text and scene image (sprite)
    public void SetInitValues(int _sceneIndex, string _header, Sprite _sprite)
    {
        // Scene the SceneButton will open
        OpenSceneButton.sceneToLoadBuildIndex = _sceneIndex;
        sceneHeaderText.text = _header;
        sceneElementImage.sprite = _sprite;
    }

    public void SetLessonsFoundText(int _found, int _max)
    {
        lessonsFoundText.text = _found + "/" + _max;

        //if all lessons in scene found - show checkmark
        if (_max == _found)
        {
            checkMark.gameObject.SetActive(true);
        }
    }
}