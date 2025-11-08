using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private int sceneIndex;

    public void TransitionScene()
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1f;
    }

    public void ExitingGame()
    {
        EditorApplication.ExitPlaymode(); // выход из режима игры в редакторе
        Application.Quit(); // выход из игры
    }
}
