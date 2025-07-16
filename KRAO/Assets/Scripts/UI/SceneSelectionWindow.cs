using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelectionWindow : Window
{
    public GameObject SceneElementPrefab;
    public GameObject SceneElementsContainer;
    public List<SceneElement> SceneElements { get; set; }

    public void CreateSceneElements(List<SceneItem> _sceneItems)
    {
        for (int i = 0; i < _sceneItems.Count; i++)
        {
            SceneElementPrefab.GetComponent<SceneElement>().SetInitValues(_sceneItems[i].SceneIndex,
                _sceneItems[i].SceneHeader, _sceneItems[i].SceneImage);

            Instantiate(SceneElementPrefab, SceneElementsContainer.transform);
        }

        SceneElements = GetComponentsInChildren<SceneElement>().ToList();
    }
}