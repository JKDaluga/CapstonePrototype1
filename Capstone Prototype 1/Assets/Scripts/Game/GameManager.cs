using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class GameManager : MonoBehaviour
{
    public static System.Action<bool> DisableOnPause;
    [SerializeField]
    PauseMenu pauseMenu;
    [HideInInspector]
    public ObjectPooler objectPooler;
    [HideInInspector]
    public bool gameIsPaused = false;


    private static GameManager m_instance;
    public static GameManager Instance
    {
        get 
        {
            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance) Destroy(this);
        else
        {
            m_instance = this;
            objectPooler = new ObjectPooler();
            Cursor.lockState = CursorLockMode.Locked; 
        }
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        pauseMenu.ToggleMenu();
        Cursor.lockState = gameIsPaused ? CursorLockMode.None : CursorLockMode.Locked;
        if (DisableOnPause != null)
        {
            DisableOnPause(gameIsPaused);
        }
    }
}
