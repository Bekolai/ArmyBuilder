using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] int swordPrice, armorPrice,soldierPrice;
    [SerializeField] Text swordPriceText, armorPriceText,soldierBuyText,
        solUPGgoldText,solUPGarmorText,solUPGswordText,
        solUPG2goldText, solUPG2armorText, solUPG2swordText,
        playerUPGgoldText,playerUPGarmorText,playerUPGswordText,
        playerUPG2goldText, playerUPG2armorText, playerUPG2swordText;
    [SerializeField] Vector3 soldier1Upgrade, soldier2Upgrade;
    // Start is called before the first frame update
    public static ShopManager Instance { get; private set; }
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
        UpdateUI();
    }
    void UpdateUI()
    {
        swordPriceText.text = swordPrice.ToString();
        armorPriceText.text = armorPrice.ToString();
        soldierBuyText.text = soldierPrice.ToString();
        solUPGgoldText.text = soldier1Upgrade.x.ToString();
        solUPGarmorText.text = soldier1Upgrade.y.ToString();
        solUPGswordText.text = soldier1Upgrade.z.ToString();
        solUPG2goldText.text = soldier2Upgrade.x.ToString();
        solUPG2armorText.text = soldier2Upgrade.y.ToString();
        solUPG2swordText.text = soldier2Upgrade.z.ToString();
        playerUPGgoldText.text = soldier1Upgrade.x.ToString();
        playerUPGarmorText.text = soldier1Upgrade.y.ToString();
        playerUPGswordText.text = soldier1Upgrade.z.ToString();
        playerUPG2goldText.text = soldier2Upgrade.x.ToString();
        playerUPG2armorText.text = soldier2Upgrade.y.ToString();
        playerUPG2swordText.text = soldier2Upgrade.z.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyArmor()
    {
        if (PlayerPrefs.GetInt("Gold") >= armorPrice)
        {
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - armorPrice);
            PlayerPrefs.SetInt("Armor", PlayerPrefs.GetInt("Armor") + 1);
            GameManager.Instance.UpdateTextUI();
            BuySuccesfull();
        }
        else
            BuyFailed();
    }
    public void BuySword()
    {
        if(PlayerPrefs.GetInt("Gold")>=swordPrice)
        {
        PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - swordPrice);
        PlayerPrefs.SetInt("Sword", PlayerPrefs.GetInt("Sword") + 1);
            GameManager.Instance.UpdateTextUI();
            BuySuccesfull();
        }
        else
            BuyFailed();

    }
    public void BuySoldier()
    {
        if (PlayerPrefs.GetInt("Gold") >= soldierPrice && (PlayerPrefs.GetInt("Soldiers")
            + PlayerPrefs.GetInt("SoldierLevel1")+ PlayerPrefs.GetInt("SoldierLevel2")) < ((PlayerPrefs.GetInt("Barracks") + 1) * 20))
        {
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - soldierPrice);
            PlayerPrefs.SetInt("Soldiers", PlayerPrefs.GetInt("Soldiers") + 1);
            LevelManager.Instance.AddSingleSoldier();
            GameManager.Instance.UpdateTextUI();
            BuySuccesfull();
        }
        else
            BuyFailed();

    }
    public void BoughtLand(string ObjName)
    {
       
        switch (ObjName)
        {
            case "Tent": PlayerPrefs.SetInt("Tent",(PlayerPrefs.GetInt("Tent") + 1));break;
            case "Barracks": PlayerPrefs.SetInt("Barracks", (PlayerPrefs.GetInt("Barracks") + 1)); break;
            case "Weapon Shop": PlayerPrefs.SetInt("Blacksmith", (PlayerPrefs.GetInt("Blacksmith") + 1)); break;
            case "Armour Shop": PlayerPrefs.SetInt("Armory", (PlayerPrefs.GetInt("Armory") + 1)); break;
        } 
        BuyLand();
    }
    public void UpgradeSoldier()
    {
        if (PlayerPrefs.GetInt("Gold") >= soldier1Upgrade.x && PlayerPrefs.GetInt("Armor")>= soldier1Upgrade.y && 
            PlayerPrefs.GetInt("Sword") >= soldier1Upgrade.z && PlayerPrefs.GetInt("Soldiers") >= 1)
        {

            if (LevelManager.Instance.UpgradeSoldierLevel1()) // if upgrade succesfull decrease price
            {
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - (int)soldier1Upgrade.x);
                PlayerPrefs.SetInt("Armor", PlayerPrefs.GetInt("Armor") - (int)soldier1Upgrade.y);
                PlayerPrefs.SetInt("Sword", PlayerPrefs.GetInt("Sword") - (int)soldier1Upgrade.z);
                PlayerPrefs.SetInt("Soldiers", PlayerPrefs.GetInt("Soldiers") - 1);
                PlayerPrefs.SetInt("SoldierLevel1", PlayerPrefs.GetInt("SoldierLevel1") + 1);
                GameManager.Instance.UpdateTextUI();
                UpgradeSuccesfull();
            }
            else
                BuyFailed();

        }
        else
            BuyFailed();

    }
    public void UpgradeSoldierLevel2()
    {
        if (PlayerPrefs.GetInt("Gold") >= soldier2Upgrade.x && PlayerPrefs.GetInt("Armor") >= soldier2Upgrade.y &&
            PlayerPrefs.GetInt("Sword") >= soldier2Upgrade.z && PlayerPrefs.GetInt("SoldierLevel1") >= 1)
        {
           
            if (LevelManager.Instance.UpgradeSoldierLevel2()) // if upgrade succesfull decrease price
            {

                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - (int)soldier2Upgrade.x);
                PlayerPrefs.SetInt("Armor", PlayerPrefs.GetInt("Armor") - (int)soldier2Upgrade.y);
                PlayerPrefs.SetInt("Sword", PlayerPrefs.GetInt("Sword") - (int)soldier2Upgrade.z);
                PlayerPrefs.SetInt("SoldierLevel1", PlayerPrefs.GetInt("SoldierLevel1") - 1);
                PlayerPrefs.SetInt("SoldierLevel2", PlayerPrefs.GetInt("SoldierLevel2") + 1);
                GameManager.Instance.UpdateTextUI();
                UpgradeSuccesfull();
            }
            else
                BuyFailed();

        }
        else
            BuyFailed();

    }
    public void UpgradePlayer()
    {
        if (PlayerPrefs.GetInt("Gold") >= soldier1Upgrade.x && PlayerPrefs.GetInt("Armor") >= soldier1Upgrade.y &&
            PlayerPrefs.GetInt("Sword") >= soldier1Upgrade.z)
        {
            if (LevelManager.Instance.UpgradePlayerLevel1()) // if upgrade succesfull decrease price
            {
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - (int)soldier1Upgrade.x);
                PlayerPrefs.SetInt("Armor", PlayerPrefs.GetInt("Armor") - (int)soldier1Upgrade.y);
                PlayerPrefs.SetInt("Sword", PlayerPrefs.GetInt("Sword") - (int)soldier1Upgrade.z);
                PlayerPrefs.SetInt("PlayerLevel", 1);
                GameManager.Instance.UpdateTextUI();
                UpgradeSuccesfull();
            }
            else
                BuyFailed();
        }
        else
            BuyFailed();
    }
    public void UpgradePlayerLevel2()
    {
        if (PlayerPrefs.GetInt("Gold") >= soldier2Upgrade.x && PlayerPrefs.GetInt("Armor") >= soldier2Upgrade.y &&
            PlayerPrefs.GetInt("Sword") >= soldier2Upgrade.z)
        {
            if (LevelManager.Instance.UpgradePlayerLevel2()) // if upgrade succesfull decrease price
            {
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - (int)soldier2Upgrade.x);
                PlayerPrefs.SetInt("Armor", PlayerPrefs.GetInt("Armor") - (int)soldier2Upgrade.y);
                PlayerPrefs.SetInt("Sword", PlayerPrefs.GetInt("Sword") - (int)soldier2Upgrade.z);
                PlayerPrefs.SetInt("PlayerLevel", 2);
                GameManager.Instance.UpdateTextUI();
                UpgradeSuccesfull();
            }
            else
            BuyFailed();
        }
        else
            BuyFailed();
    }
    void BuyFailed()
    {
        AudioManager.Instance.PlayBuyFail();
    }
    void BuySuccesfull()
    {
        AudioManager.Instance.PlayBuy();
    }
    void UpgradeSuccesfull()
    {
        AudioManager.Instance.PlayUpgrade();
    }
    void BuyLand()
    {
        AudioManager.Instance.PlayBuild();
    }
}