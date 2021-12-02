using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPageScript : MonoBehaviour
{
    [SerializeField] GameObject _infoPageOne;
    [SerializeField] GameObject _infoPageTwo;
    [SerializeField] GameObject _mainMenuPage;


    public void InfoPageOne()
    {
        _infoPageOne.SetActive(true);
        _infoPageTwo.SetActive(false);
        _mainMenuPage.SetActive(false);
    }

    public void InfoPageTwo()
    {
        _infoPageOne.SetActive(false);
        _infoPageTwo.SetActive(true);
        _mainMenuPage.SetActive(false);
    }

    public void MainMenuPage()
    {
        _infoPageOne.SetActive(false);
        _infoPageTwo.SetActive(false);
        _mainMenuPage.SetActive(true);
    }

}
