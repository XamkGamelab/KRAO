using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelectionWindow : Window
{
    public GameObject SceneElementPrefab;
    public GameObject SceneElementsContainer;
    public List<SceneElementStruct> SceneElements;

    private void Start()
    {
        gameObject.SetActive(true);
        InstantiateElements();
    }

    private void InstantiateElements()
    {
        for (int i = 0; i < SceneElements.Count; i++)
        {
            SceneElementPrefab.GetComponent<SceneElement>().SetInitValues(SceneElements[i].SceneIndex,
                SceneElements[i].SceneHeader, SceneElements[i].Image);

            Instantiate(SceneElementPrefab, SceneElementsContainer.transform);
        }
    }
}

[Serializable]
public struct SceneElementStruct
{
    public int SceneIndex;
    public string SceneHeader;
    public Image Image;
}