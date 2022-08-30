using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarLevel : MonoBehaviour
{
    [SerializeField] GameObject soldierSpawner, enemySpawner;
    [SerializeField] GameObject soldierPrefab, enemyPrefab,player;
    [SerializeField] List<GameObject> soldiers, enemySoldiers;
     int enemy1, enemy2, enemy3;
    void Start()
    {
        SpawnSoldiers();
        for(int i = 0; i < PlayerPrefs.GetInt("EnemyCreated"); i++)
        {
            int random = Random.Range(0, 10);
            if (random < 7)
            {
                enemy1++;
            }
            else if (random == 9)
            {
                enemy3++;
            }
            else
                enemy2++;
            
        }
        SpawnEnemySoldiers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnSoldiers()
    {
        float row = 0;
        int axis = 0;
       
        for (int i = 0; i < PlayerPrefs.GetInt("Soldiers") + PlayerPrefs.GetInt("SoldierLevel1") + PlayerPrefs.GetInt("SoldierLevel2"); i++)
        {
            Vector3 spawnLoc = Vector3.zero;

       
            spawnLoc.x = (axis % 10)*(0.6f);
            spawnLoc.z = (int)(row / 10) * 0.5f;


            GameObject soldier = Instantiate(soldierPrefab, soldierSpawner.transform.position + spawnLoc, Quaternion.Euler(0, 0, 0), soldierSpawner.transform);
            soldiers.Add(soldier);

            row++;
            axis++;



        }
        GenerateUpgrades();
    }
    void GenerateUpgrades()
    {
        if (PlayerPrefs.GetInt("SoldierLevel1") > 0 || PlayerPrefs.GetInt("SoldierLevel2") > 0)
        {

            if (PlayerPrefs.GetInt("SoldierLevel2") > 0) //check if player has level 2 soldiers
            {

                for (int i = 0; i < PlayerPrefs.GetInt("SoldierLevel2"); i++)
                {
                    soldiers[i].GetComponent<Soldier>().SoldierLevel2();
                }

            }

            for (int i = PlayerPrefs.GetInt("SoldierLevel2"); i < PlayerPrefs.GetInt("SoldierLevel2") + PlayerPrefs.GetInt("SoldierLevel1"); i++) //upgrade soldiers to level 2 after level 3s
            {
                soldiers[i].GetComponent<Soldier>().SoldierLevel1();

            }

        }

        int PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
        switch (PlayerLevel)
        {
            case 0: break;
            case 1: player.GetComponent<Soldier>().SoldierLevel1(); break;
            case 2: player.GetComponent<Soldier>().SoldierLevel2(); break;
        }
    }
    void GenerateEnemyUpgrades()
    {
     //   if (PlayerPrefs.GetInt("EnemyLevel1") > 0 || PlayerPrefs.GetInt("EnemyLevel2") > 0)
         if (enemy2 > 0 || enemy3 > 0)
        {

          //  if (PlayerPrefs.GetInt("EnemyLevel2") > 0) //check if player has level 2 soldiers
                if (enemy3 > 0)
                {

                //  for (int i = 0; i < PlayerPrefs.GetInt("EnemyLevel2"); i++)
                for (int i = 0; i < enemy3; i++)
                {
                    enemySoldiers[i].GetComponent<Soldier>().SoldierLevel2();
                }

            }

          //  for (int i = PlayerPrefs.GetInt("EnemyLevel2"); i < PlayerPrefs.GetInt("EnemyLevel2") + PlayerPrefs.GetInt("EnemyLevel1"); i++) //upgrade soldiers to level 2 after level 3s
                for (int i = enemy3; i < enemy3 +enemy2; i++) //upgrade soldiers to level 2 after level 3s
                {
                enemySoldiers[i].GetComponent<Soldier>().SoldierLevel1();

            }

        }

    }
    void SpawnEnemySoldiers()
    {
        float row = 0;
        int axis = 0;

       // for (int i = 0; i < PlayerPrefs.GetInt("Enemies") + PlayerPrefs.GetInt("EnemyLevel1") + PlayerPrefs.GetInt("EnemyLevel2"); i++)
            for (int i = 0; i < enemy1 +enemy2+ enemy3; i++)
            {
            Vector3 spawnLoc = Vector3.zero;


            spawnLoc.x = (axis % 10) * (0.6f);
            spawnLoc.z = (int)(row / 10) * 0.5f;


            GameObject soldier = Instantiate(enemyPrefab, enemySpawner.transform.position + spawnLoc, Quaternion.Euler(0, 180, 0), enemySpawner.transform);
            enemySoldiers.Add(soldier);

            row++;
            axis++;



        }
        GenerateEnemyUpgrades();
    }
}
