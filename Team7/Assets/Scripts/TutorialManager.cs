using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Tutorialscript : MonoBehaviour
{
    public GameObject barrier;
    public GameObject shild;

    bool WButton = false;
    bool AButton = false;
    bool SButton = false;
    bool DButton = false;
    bool SpaceButton = false;
    bool activator = false;
    bool activator2 = false;
    bool activator3 = true;
    bool slowM = false;
    bool activator4 = false;
    bool rightButton = false;
    bool activator5 = false;

    public TMP_Text tText1;
    // Start is called before the first frame update
    void Start()
    {
        activator = false;
        activator2 = false;
        WButton = false;
        rightButton = false;
        AButton = false;
        SButton = false;
        DButton = false;
        SpaceButton = false;
        slowM = false;
        activator3 = true;
        activator4 = false;
        activator5 = false;
        tText1.text = "Welcome to our tutorial!\r\n\r\nPress W, A, S, D to move ";
        shild.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WButton = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AButton = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SButton = true;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DButton = true;

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceButton = true;

        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            slowM = true;

        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            rightButton = true;

        }
        if (WButton == true && AButton == true && SButton == true && DButton == true && activator == false)
        {
            SpaceButton = false;
            activator = true;
            tText1.text = "Good!\r\nNow press SPACE to dash";
        }
        if (SpaceButton == true && activator == true && activator4 == false)
        {
            tText1.text = "If you get the slowmotion ability in the arena\r\nYou have to press SHIFT to slow down time and use it to your advantage.\r\n\r\nBe careful though, the cooldown is 10 seconds long!";
            slowM = false;
            activator2 = true;
            activator4 = true;
        }
        if (slowM == true && activator2 == true && activator3 == true)
        {
            tText1.text = "Nice, now go up and make the enemies shoot themselfs.\r\nTry to dodge and use your shild to reflect the bulet back to them\r\nShild cooldown 5 secs\r\nUse rightclick on mouse to use shield";
            activator3 = false;
            Destroy(barrier);
            rightButton = false;
            activator5 = true;
        }
        if (rightButton == true && activator5 == true)
        {
            tText1.text = "";
            activator5 = false;
        }

        if (AllEnemiesDefeated() == true)
        {
            SceneManager.LoadScene("Upgrade_Test");
        }
    }
    bool AllEnemiesDefeated()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                return false;
            }
        }

        return true;
    }
}
