using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControllerScript : MonoBehaviour
{
    #region Variables
    [SerializeField] AudioClip _mainMenuMusic;


    #endregion

    private void Start()
    {
        AudioHelper.PlayClip2D(_mainMenuMusic, 1f);
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

    public void GoToGame()
    {
        //have to make sure that the build index for the actual game scene is always 1 + the buildIndex of the main menu scene.
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
