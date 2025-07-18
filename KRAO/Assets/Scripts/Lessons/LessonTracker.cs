using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LessonTracker : MonoBehaviour
{
    private LessonProgressBar progressBar => FindFirstObjectByType<LessonProgressBar>();
    private JournalWindow journalWindow => FindFirstObjectByType<JournalWindow>();
    private SceneSelectionWindow sceneSelectionWindow => FindFirstObjectByType<SceneSelectionWindow>();

    public List<SceneItem> SceneItems;
    

    #region private methods
    private void Awake()
    {
        journalWindow.CreateDropdowns(SceneItems);
        journalWindow.CreateDropdownButtons(SceneItems);
        sceneSelectionWindow.CreateSceneElements(SceneItems);

        InitTrackers();

        SceneLoader.SetProgressBarValues += OnSceneChange;
    }

    private void OnSceneChange(int _sceneId)
    {
        if (_sceneId == 0)
        {
            return;
        }
        else
        {
            progressBar.ChangeSliderValue(SceneItemById(_sceneId).FoundLessons, SceneItemById(_sceneId).MaxLessons);
        }
    }

    private void InitTrackers()
    {
        for (int i = 0; i < SceneItems.Count; i++)
        {
            SceneItems[i].MaxLessons = SceneItems[i].lessons.Count;
            
            //journal
            journalWindow.JournalDropdowns[i].SetLessonsFoundText(SceneItems[i].FoundLessons, SceneItems[i].MaxLessons);

            //sceneselection
            sceneSelectionWindow.SceneElements[i].SetLessonsFoundText(SceneItems[i].FoundLessons, SceneItems[i].MaxLessons);
        }
    }

    private void UpdateTrackers(int _sceneId)
    {
        //progressbar
        progressBar.ChangeSliderValue(SceneItemById(_sceneId).FoundLessons, SceneItemById(_sceneId).MaxLessons);
        
        for (int i = 0; i < SceneItems.Count; i++)
        {
            //journal
            journalWindow.JournalDropdowns[i].SetLessonsFoundText(SceneItems[i].FoundLessons, SceneItems[i].MaxLessons);

            //sceneselection
            sceneSelectionWindow.SceneElements[i].SetLessonsFoundText(SceneItems[i].FoundLessons, SceneItems[i].MaxLessons);
        }
    }

    private SceneItem SceneItemById(int _sceneId)
    {
        SceneItem scene = null;

        for (int i = 0; i < SceneItems.Count; i++)
        {
            if (SceneItems[i].SceneIndex == _sceneId)
            {
                scene = SceneItems[i];
            }
        }
        return scene;
    }
    #endregion

    #region public methods
    public LessonItem LessonItemById(int _lessonId)
    {
        LessonItem _lessonItem = null;

        for (int i = 0; i < SceneItems.Count; i++)
        {
            if (SceneItems[i].SceneIndex == SceneManager.GetActiveScene().buildIndex)
            {
                for (int j = 0; j < SceneItems[i].lessons.Count; j++)
                {
                    if (SceneItems[i].lessons[j].LessonId == _lessonId)
                    {
                        _lessonItem = SceneItems[i].lessons[j];
                    }
                }
            }
        }

        return _lessonItem;
    }

    public void AddLessonToTracker(int _sceneId)
    {
        for (int i = 0; i < SceneItems.Count; i++)
        {
            if (SceneItems[i].SceneIndex == _sceneId)
            {
                SceneItems[i].FoundLessons++;
                UpdateTrackers(_sceneId);
            }
        }
    }

    public void SetProgressBarValue(int _sceneId)
    {
        progressBar.ChangeSliderValue(SceneItemById(_sceneId).FoundLessons, SceneItemById(_sceneId).MaxLessons);
    }
    #endregion
}

[Serializable]
public class SceneItem
{
    public string SceneHeader;
    public int SceneIndex;
    public Sprite SceneImage;
    public List<LessonItem> lessons;
    public int FoundLessons { get; set; } = 0;
    public int MaxLessons { get; set; }
}

[Serializable]
public class LessonItem
{
    public string HeaderText;
    public int LessonId;
    [TextArea(15, 15)] public string ContentText;
}