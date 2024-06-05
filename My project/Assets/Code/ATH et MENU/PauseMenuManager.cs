using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private PlayerMovement playerMovement;

    void Start()
    {
        Debug.Log("                             START");
        pauseMenuUI.SetActive(false);
        playerMovement = FindObjectOfType<PlayerMovement>(); // Find the PlayerMovement script in the scene
    
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found in the scene.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        Debug.Log("                             pauseGame");
        pauseMenuUI.SetActive(true);

        if (playerMovement != null)
        {
            playerMovement.SetCanMove(false);
        }

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("                             resumeGame");
        pauseMenuUI.SetActive(false);

        if (playerMovement != null)
        {
            playerMovement.SetCanMove(true);
        }

        Time.timeScale = 1f;
    }

    public void QuitToMainMenu()
    {
        
        Debug.Log("                             Quit");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
