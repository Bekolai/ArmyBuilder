using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum SoldierLevel
{
    Level0,Level1,Level2
};

public class GameManager : MonoBehaviour
{
    [SerializeField] int gold;
    [SerializeField] Text goldText, armorText, swordText,soldierText,soldierLv1Text,soldierLv2Text;
    bool isWarMap;


    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        if(SceneManager.GetActiveScene().name!="Village")
        {
            isWarMap = true;
        }
    }
    void Start()
    {
        UpdateTextUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateTextUI() 
    {
        goldText.text = PlayerPrefs.GetInt("Gold").ToString();
        armorText.text = PlayerPrefs.GetInt("Armor").ToString();
        swordText.text = PlayerPrefs.GetInt("Sword").ToString();
        soldierText.text=((PlayerPrefs.GetInt("Soldiers")+ PlayerPrefs.GetInt("SoldierLevel1")+ PlayerPrefs.GetInt("SoldierLevel2")).ToString())
            +"/"+((PlayerPrefs.GetInt("Barracks")+1)*20).ToString();
        soldierLv1Text.text = (PlayerPrefs.GetInt("SoldierLevel1").ToString());
        soldierLv2Text.text = (PlayerPrefs.GetInt("SoldierLevel2").ToString());
    }
    public bool WarMap()
    {
        return isWarMap;
    }
    public void LoadVillage()
    {
        SceneManager.LoadScene("Village");
    }
}
