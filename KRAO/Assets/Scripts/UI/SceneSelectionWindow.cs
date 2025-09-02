using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneSelectionWindow : Window
{
    public GameObject SceneElementPrefab;
    public GameObject SceneElementsContainer;
    public List<SceneElement> SceneElements { get; set; }

    // Method to create scene elements to scene selection window, called in LessonTracker
    public void CreateSceneElements(List<SceneItem> _sceneItems)
    {
        // Go through SceneItems list
        for (int i = 0; i < _sceneItems.Count; i++)
        {
            // Set indexes, headers and images for each SceneElement
            SceneElementPrefab.GetComponent<SceneElement>().SetInitValues(_sceneItems[i].SceneIndex,
                _sceneItems[i].SceneHeader, _sceneItems[i].SceneImage);
            // Instantiate SceneElements
            Instantiate(SceneElementPrefab, SceneElementsContainer.transform);
        }
        // Put the instantiates SceneElements to list
        SceneElements = GetComponentsInChildren<SceneElement>().ToList();
    }
}