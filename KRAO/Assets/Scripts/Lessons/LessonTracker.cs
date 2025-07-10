using System;
using System.Collections.Generic;
using UnityEngine;

public class LessonTracker : MonoBehaviour
{
    public List<LessonScene> lessonScenes;

    private JournalWindow journalWindow => FindFirstObjectByType<JournalWindow>();

    private void Start()
    {
        //InitTrackers();
    }

    private void InitTrackers()
    {

    }

    public void AddLessonToTracker(int _sceneId)
    {
        for (int i = 0; i< lessonScenes.Count; i++)
        {
            if (lessonScenes[i].SceneId == _sceneId)
            {
                lessonScenes[i].FoundLessons++;
                UpdateTrackers(_sceneId);
            }
        }
    }

    private void UpdateTrackers(int _sceneId)
    {
        //progressbar

        //journal
        for (int i=0;i< journalWindow.JournalDropdowns.Count; i++)
        {
            if (journalWindow.JournalDropdowns[i].SceneIndex == _sceneId)
            {
                //journalWindow.JournalDropdowns[i].SetLessonsFoundText(GetLessonSceneBySceneId(_sceneId).FoundLessons);
            }
        }
        //sceneselection
    }

    private LessonScene GetLessonSceneBySceneId(int _id)
    {
        LessonScene scene = null;

        for (int i = 0; i < lessonScenes.Count; i++)
        {
            if (lessonScenes[i].SceneId == _id)
            {
                scene = lessonScenes[i];
            }
        }
        return scene;
    }

    private int LessonsInScene(int _sceneId)
    {


        return 0;
    }
}

[Serializable]
public class LessonScene
{
    public int SceneId;
    public int FoundLessons = 0;
    //public int MaxLessons;
}