using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] barracks;
    [SerializeField] GameObject tent,blacksmith,armory;
    [SerializeField] List<GameObject> soldiers,soldiers2,soldiers3;
    [SerializeField] GameObject soldierPrefab,player;
    GameObject spawner;


    public static LevelManager Instance { get; private set; }
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
        GenerateBuildings();
        SpawnSoldiers();
    }

     void GenerateBuildings()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("Barracks") + 1; i++) //turn on barracks
        {
            barracks[i].transform.GetChild(0).gameObject.SetActive(false);
            barracks[i].transform.GetChild(1).gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Tent") == 1)
        {
            tent.transform.GetChild(0).gameObject.SetActive(false);
            tent.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Blacksmith") == 1)
        {
            blacksmith.transform.GetChild(0).gameObject.SetActive(false);
            blacksmith.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Armory") == 1)
        {
            armory.transform.GetChild(0).gameObject.SetActive(false);
            armory.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
     

    void SpawnSoldiers()
    {
        float row = 0;
        int axis = 0;
        spawner = barracks[0].transform.GetChild(2).gameObject;
        for (int i = 0; i < PlayerPrefs.GetInt("Soldiers")+ PlayerPrefs.GetInt("SoldierLevel1")+ PlayerPrefs.GetInt("SoldierLevel2"); i++)
        {
            Vector3 spawnLoc = Vector3.zero;

            if ((axis % 2) == 0)
                    {
                spawnLoc.x = 0.4f; 
            }
            else
                spawnLoc.x = -0.4f;
            spawnLoc.z = (int)(row / 2) * 0.5f;


            GameObject soldier = Instantiate(soldierPrefab, spawner.transform.position+spawnLoc, Quaternion.Euler(0, 180, 0), spawner.transform);
            soldiers.Add(soldier);

            row++;
            axis++;


            if(i==19) //switch to second barracks
            {
                row = 0;
                axis = 0;
                spawner = barracks[1].transform.GetChild(2).gameObject;

            }
            if (i == 39) //switch to third barracks
            {
                row = 0;
                axis = 0;
                spawner = barracks[2].transform.GetChild(2).gameObject;
            }

            

        }
        GenerateUpgrades();
    }  
    void GenerateUpgrades()
        {
            if (PlayerPrefs.GetInt("SoldierLevel1")>0 || PlayerPrefs.GetInt("SoldierLevel2") > 0)
            {
           
            if (PlayerPrefs.GetInt("SoldierLevel2") > 0) //check if player has level 2 soldiers
            {

                for(int i=0;i< PlayerPrefs.GetInt("SoldierLevel2");i++)
                {
                    soldiers[i].GetComponent<Soldier>().SoldierLevel2();
                 }

            }
          
            for (int i = PlayerPrefs.GetInt("SoldierLevel2"); i < PlayerPrefs.GetInt("SoldierLevel2")+ PlayerPrefs.GetInt("SoldierLevel1"); i++) //upgrade soldiers to level 2 after level 3s
                {
                    soldiers[i].GetComponent<Soldier>().SoldierLevel1();
             
                 }

            }

        int PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
        switch(PlayerLevel)
        {
            case 0:break;
            case 1:player.GetComponent<Soldier>().SoldierLevel1();break;
            case 2: player.GetComponent<Soldier>().SoldierLevel2(); break;
        }
        }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddSingleSoldier()
    {
        int barrackNo = PlayerPrefs.GetInt("Soldiers")+ PlayerPrefs.GetInt("SoldierLevel1")+PlayerPrefs.GetInt("SoldierLevel2"); //get all soldier count
        int soldierNo=0;
        switch((barrackNo-1)/20) //switch barracks if they are full
        {
            case 0: spawner = barracks[0].transform.GetChild(2).gameObject;soldierNo = barrackNo; break;
            case 1: spawner = barracks[1].transform.GetChild(2).gameObject; soldierNo = barrackNo - 20; break;
            case 2: spawner = barracks[2].transform.GetChild(2).gameObject; soldierNo = barrackNo - 40; break;
        }
        if(soldierNo!=0)
        {
        soldierNo++;
        }
        Vector3 spawnLoc = Vector3.zero;
        if ((soldierNo % 2) == 0)  //set their x pos according to their soldier number 
        {
            spawnLoc.x = 0.4f; 
        }
        else
            spawnLoc.x = -0.4f;
        spawnLoc.z = (int)(soldierNo / 2) * 0.5f; //increase row


        GameObject soldier = Instantiate(soldierPrefab, spawner.transform.position + spawnLoc, Quaternion.Euler(0, 180, 0), spawner.transform); //create soldier instance
        soldiers.Add(soldier);
    }
    public bool UpgradeSoldierLevel1()
    {
        for(int i= 0; i < soldiers.Count; i++)
        {
            if (soldiers[i].GetComponent<Soldier>().getSoldierLevel()==SoldierLevel.Level0) // if level 0 upgrade
            {
                soldiers[i].GetComponent<Soldier>().SoldierLevel1();

                return true;
            }    
        }
        return false; // if there is no level 0 soldiers dont upgrade
    }
    public bool UpgradeSoldierLevel2()
    {
        for (int i = 0; i < soldiers.Count; i++)
        {
            if (soldiers[i].GetComponent<Soldier>().getSoldierLevel() == SoldierLevel.Level1) // if level 1 upgrade
            {
                soldiers[i].GetComponent<Soldier>().SoldierLevel2();

                return true;
            }
        }
        return false; // if there is no level 1 soldiers dont upgrade
    }
    public bool UpgradePlayerLevel1()
    {
     
            if (player.GetComponent<Soldier>().getSoldierLevel() == SoldierLevel.Level0) // if level 0 upgrade
            {
               player.GetComponent<Soldier>().SoldierLevel1();

                return true;
            }
            else
                return false; // if there is no level 0 soldiers dont upgrade
    }
    public bool UpgradePlayerLevel2()
    {
       
            if (player.GetComponent<Soldier>().getSoldierLevel() == SoldierLevel.Level1) // if level 1 upgrade
            {
                player.GetComponent<Soldier>().SoldierLevel2();

                return true;
            }
            else
                 return false; // if there is no level 1 soldiers dont upgrade
    }
    public void EasyWar()
    {
        int enemyCount = (int)(soldiers.Count * 0.6);
        if (enemyCount == 0)
        {
            enemyCount = 1;
        }
        Debug.Log(enemyCount);
        PlayerPrefs.SetInt("EnemyCreated", enemyCount);
        SceneManager.LoadScene(1);
    }
    public void MediumWar()
    {
        int enemyCount = (int)(soldiers.Count * 1);
        if (enemyCount == 0)
        {
            enemyCount = 1;
        }
        Debug.Log(enemyCount);
        PlayerPrefs.SetInt("EnemyCreated", enemyCount);
        SceneManager.LoadScene(1);
    }
    public void HardWar()
    {
        int enemyCount = (int)(soldiers.Count * 1.5);
        if (enemyCount == 0)
        {
            enemyCount = 1;
        }
        Debug.Log(enemyCount);
        PlayerPrefs.SetInt("EnemyCreated", enemyCount);
        SceneManager.LoadScene(1);
    }
}
