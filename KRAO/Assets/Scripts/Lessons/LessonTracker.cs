using System;
using System.Collections.Generic;
using UnityEngine;

public class LessonTracker : MonoBehaviour
{
    private LessonProgressBar progressBar => FindFirstObjectByType<LessonProgressBar>();
    private JournalWindow journalWindow => FindFirstObjectByType<JournalWindow>();
    private SceneSelectionWindow sceneSelectionWindow => FindFirstObjectByType<SceneSelectionWindow>();

    //List of scenes and lessons, details filled in editor
    public List<SceneItem> SceneItems;


    #region private methods
    private void Awake()
    {
        // Instantiate GameObjects (buttons/dropdowns) with values from SceneItems
        journalWindow.CreateDropdowns(SceneItems);
        journalWindow.CreateDropdownButtons(SceneItems);
        sceneSelectionWindow.CreateSceneElements(SceneItems);

        // Set initial values to trackers
        InitTrackers();

        // Start listening to scene changes
        SceneLoader.SetProgressBarValues += OnSceneChange;
    }

    // Update progress bar values when scene changes
    private void OnSceneChange(int _sceneId)
    {
        // return if scene is main menu
        if (_sceneId == 0)
        {
            return;
        }
        else
        {
            SetProgressBarValue(_sceneId);
        }
    }

    // Set initial values to trackers
    private void InitTrackers()
    {
        for (int i = 0; i < SceneItems.Count; i++)
        {
            // Set max lessons values
            SceneItems[i].MaxLessons = SceneItems[i].lessons.Count;

            //journal
            journalWindow.JournalDropdowns[i].SetLessonsFoundText(SceneItems[i].FoundLessons, SceneItems[i].MaxLessons);
            journalWindow.JournalDropdowns[i].SceneIndex = SceneItems[i].SceneIndex;
            //sceneselection
            sceneSelectionWindow.SceneElements[i].SetLessonsFoundText(SceneItems[i].FoundLessons, SceneItems[i].MaxLessons);
        }
    }

    // Set values to trackers from the specified scene
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

    // Get amount of found/opened lessons in the specified scene
    private int FoundLessons(int _sceneId)
    {
        int foundLessons = 0;

        // For each lesson in the scene
        SceneItemById(_sceneId).lessons.ForEach(l =>
        {
            // If the lesson has been found
            if (l.IsNew == false)
            {
                // Increment foundLessons
                foundLessons++;
            }
        });

        return foundLessons;
    }
    #endregion

    #region public methods
    // Get a SceneItem by its SceneIndex
    public SceneItem SceneItemById(int _sceneId)
    {
        SceneItem scene = null;

        // Go through the SceneItems
        for (int i = 0; i < SceneItems.Count; i++)
        {
            // If scene with SceneIndex of _sceneId is found, return that scene
            if (SceneItems[i].SceneIndex == _sceneId)
            {
                scene = SceneItems[i];
            }
            // else returns null
        }
        return scene;
    }

    // Get a LessonItem by its LessonId
    public LessonItem LessonItemById(int _lessonId)
    {
        LessonItem _lessonItem = null;

        // Go through all SceneItems and their LessonItems
        for (int i = 0; i < SceneItems.Count; i++)
        {
            for (int j = 0; j < SceneItems[i].lessons.Count; j++)
            {
                // If Lesson with _lessonId is found, return it
                if (SceneItems[i].lessons[j].LessonId == _lessonId)
                {
                    _lessonItem = SceneItems[i].lessons[j];
                }
                // else return null
            }
        }
        return _lessonItem;
    }

    //Do this when new Lesson is found (called in LessonManager)
    public void AddLessonToTracker(int _sceneId, int _lessonId)
    {
        //Set lesson as found (old)
        LessonItemById(_lessonId).IsNew = false;
        //Update found lessons amount
        SceneItemById(_sceneId).FoundLessons = FoundLessons(_sceneId);
        //Update trackers' values
        UpdateTrackers(_sceneId);
    }

    // Set progress bar value (to some scenes FoundLessons out of its MaxLessons) with a sceneId
    public void SetProgressBarValue(int _sceneId)
    {
        progressBar.ChangeSliderValue(SceneItemById(_sceneId).FoundLessons, SceneItemById(_sceneId).MaxLessons);
    }
    #endregion
}

// "Struct" for scenes
[Serializable]
public class SceneItem
{
    public string SceneHeader;      //Scene's name
    public int SceneIndex;          //Build index
    public Sprite SceneImage;       //Image used in scene selection and journal
    public List<LessonItem> lessons;    //List of Lesson in this scene
    public int FoundLessons { get; set; } = 0;      //number of found lessons in scene
    public int MaxLessons { get; set; }         //Total amount of lessons in scene
}

// "Struct" for Lessons
[Serializable]
public class LessonItem
{
    public string HeaderText;       //Lesson's name
    public int LessonId;            //id
    public Sprite LessonImage;      //image used in journal
    [TextArea(15, 15)] public string ContentText;       //lesson's content
    public bool IsNew { get; set; } = true;         //has lesson not been found (is it new)
}