using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int gold;
    [SerializeField] Text goldText, armorText, swordText,soldierText;



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
        soldierText.text=(PlayerPrefs.GetInt("Soldiers").ToString())+"/"+((PlayerPrefs.GetInt("Barracks")+1)*20).ToString();
    }
}
