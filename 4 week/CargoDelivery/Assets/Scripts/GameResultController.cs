using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameResultController : MonoBehaviour
{
    public void OnGameOver()
    {
        gameObject.SetActive(true);
    }

    public void OnGameWin()
    {
        gameObject.SetActive(true);
    }

    public void CloseGame()
    {
        Debug.Log("Quit!");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Debug.Log("Next Level!");
    }

}
