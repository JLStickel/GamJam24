using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity;
using UnityEngine.Events;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance
    {
        get; private set;
    }
    public int playerXPLevel = 0;
    public float curPlayerXP;
    public float playerXPUI;
    public float neededXP = 100;

    public float setXPSpeed;
    public bool setXP;


    public Image XPBar;

    public GameObject UpgradeUI;
    public TMP_Text Upgrade1Text;
    public TMP_Text Upgrade2Text;
    public TMP_Text Upgrade3Text;

    public List<UpgradeList> Upgrades = new List<UpgradeList>();

    private void Awake()
    {
        Instance = this;

        ChangeUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerXPUI = curPlayerXP;
        XPBar.fillAmount = playerXPUI / neededXP;
    }

    // Update is called once per frame
    void Update()
    {
        if (curPlayerXP >= neededXP)
            Upgrade();
        if (setXP)
            SetXP();
    }

    public void Upgrade()
    {
        curPlayerXP = curPlayerXP - neededXP;
        playerXPUI = curPlayerXP;
        Time.timeScale = 0;
        UpgradeUI.SetActive(true);
    }

    public void UpgradeChosen()
    {
        playerXPLevel++;
        Time.timeScale = 1;
        UpgradeUI.SetActive(false);

        ChangeUI();
    }

    public void ChangeUI()
    {
        Upgrade1Text.text = Upgrades[playerXPLevel].upgrade1UI.UpgradeText;
        Upgrade2Text.text = Upgrades[playerXPLevel].upgrade2UI.UpgradeText;
        Upgrade3Text.text = Upgrades[playerXPLevel].upgrade3UI.UpgradeText;
    }

    public void SetXP()
    {
        playerXPUI += setXPSpeed * Time.deltaTime;
        if(playerXPUI >= curPlayerXP)
        {
            playerXPUI = curPlayerXP;
            setXP = false;
        }    
        XPBar.fillAmount = playerXPUI / neededXP;
    }

    public void UseUpgrade1()
    {
        Upgrades[playerXPLevel].Upgrade1.Invoke();
    }
    public void UseUpgrade2()
    {
        Upgrades[playerXPLevel].Upgrade2.Invoke();
    }
    public void UseUpgrade3()
    {
        Upgrades[playerXPLevel].Upgrade3.Invoke();
    }


}
[System.Serializable]
public class UpgradeList
{
    public UnityEvent Upgrade1;
    public Upgrade upgrade1UI;
    public UnityEvent Upgrade2;
    public Upgrade upgrade2UI;
    public UnityEvent Upgrade3;
    public Upgrade upgrade3UI;
}