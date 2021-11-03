using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControllerScript : MonoBehaviour
{
    #region Variables



    #endregion

    public void ExitTheGame()
    {
        Application.Quit();
    }

    public void GoToGame()
    {
        //have to make sure that the build index for the actual game scene is always 1 + the buildIndex of the main menu scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
