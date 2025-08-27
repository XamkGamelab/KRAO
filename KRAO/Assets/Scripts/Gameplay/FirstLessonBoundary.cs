using UnityEngine;

public class FirstLessonBoundary : MonoBehaviour
{
    private LessonManager lessonManager => FindFirstObjectByType<LessonManager>();

    [SerializeField] private GameObject[] boundaries;
    [SerializeField] private Lesson firstLesson;
    void Start()
    {
        UpdateBoundary();
    }

    public void UpdateBoundary()
    {
        SetBoundaryState(!HasLessonBeenLearned());
    }

    private void SetBoundaryState(bool state)
    {
        foreach(GameObject boundary in boundaries)
        {
            boundary.SetActive(state);
        }
    }

    private bool HasLessonBeenLearned()
    {
        return lessonManager.CheckIsLessonFound(firstLesson);
    }
}