using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;

    public void Play()
    {
        SceneManager.LoadScene("Upgrade_Test");
    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void Credit()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }
}
