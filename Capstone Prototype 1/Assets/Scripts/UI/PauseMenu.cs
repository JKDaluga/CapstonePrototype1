using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject overlay;

    bool paused = false;

    public void ToggleMenu()
    {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        overlay.SetActive(paused);
        gameObject.SetActive(paused);
    }

    public void ResumeGame()
    {
        GameManager.Instance.PauseGame();
    }

    public void Reset()
    {
        GameManager.Instance.PauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Debug.Log("Player tried to quit the Game. Functionality is disbaled at this time.");
        //Application.Quit();
    }
}
