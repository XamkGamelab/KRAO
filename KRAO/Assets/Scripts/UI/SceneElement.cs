using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneElement : MonoBehaviour
{
    [SerializeField] private SceneButton OpenSceneButton;

    [SerializeField] private Image SceneElementImage;
    [SerializeField] private Text SceneHeaderText;
    [SerializeField] private Text ScenesFoundText;

    [SerializeField] private Image CheckMark;


    public void SetInitValues(int _sceneIndex, string _header, Image _image)
    {
        OpenSceneButton.sceneToLoadBuildIndex = _sceneIndex;
        SceneHeaderText.text = _header;
        SceneElementImage = _image;
    }

    public void SetScenesFoundText(int _found, int _max)
    {
        ScenesFoundText.text = _found + "/" + _max;

        //if all lessons in scene found - show checkmark
        if (_max == _found)
        {
            CheckMark.gameObject.SetActive(true);
        }
    }
}
