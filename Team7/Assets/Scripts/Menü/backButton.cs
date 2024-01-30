using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backButton : MonoBehaviour
{
    [SerializeField] GameObject back;
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");

    }
}
