using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
public class WarLevel : MonoBehaviour
{
    [SerializeField] GameObject soldierSpawner, enemySpawner;
    [SerializeField] GameObject soldierPrefab, enemyPrefab,player,spectatingText,gameoverMenu,finishMenu;
    [SerializeField] List<GameObject> soldiers, enemySoldiers;
     int enemy1, enemy2, enemy3;
    [SerializeField] Slider slider;
    bool isFinished;
    public Cinemachine.CinemachineVirtualCamera virtualCam;
    int soldierCount,enemyCount;
    public Text goldText, goldText2;
    public TMP_Text lostText;
    public static WarLevel Instance { get; private set; }
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
        soldiers.Add(player);
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
        SetTargetAll();
        UpdateSlider();
        soldierCount = soldiers.Count;
        enemyCount = enemySoldiers.Count;
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

                for (int i = 1; i < PlayerPrefs.GetInt("SoldierLevel2")+1; i++)
                {
                    soldiers[i].GetComponent<Soldier>().SoldierLevel2();
                }

            }

            for (int i = PlayerPrefs.GetInt("SoldierLevel2")+1; i < PlayerPrefs.GetInt("SoldierLevel2") +1+ PlayerPrefs.GetInt("SoldierLevel1"); i++) //upgrade soldiers to level 2 after level 3s
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
    void SetTargetAll()
    {
        for (int i=1;i<soldiers.Count;i++)
        {
            soldiers[i].GetComponent<EnemyMovement>().setTarget(enemySoldiers[Random.Range(0, enemySoldiers.Count)].transform);
        }
        for (int i = 0; i < enemySoldiers.Count; i++)
        {
            enemySoldiers[i].GetComponent<EnemyMovement>().setTarget(soldiers[Random.Range(0, soldiers.Count)].transform);
        }
    }
    public Transform SetTargetEnemy()
    {
        if (enemySoldiers.Count > 0)
        {
            return enemySoldiers[Random.Range(0, enemySoldiers.Count)].transform;
        }
        else return soldiers[Random.Range(0, soldiers.Count)].transform;


    }
    public Transform SetTargetSoldier()
    {
        if (soldiers.Count > 0)
        {
            return soldiers[Random.Range(0, soldiers.Count)].transform;
        }
        else return enemySoldiers[Random.Range(0, enemySoldiers.Count)].transform;


    }
    public void SoldierDied(GameObject gameObject)
    {
        if (soldiers.IndexOf(gameObject)==0)
        {
            soldiers.Remove(gameObject);
            PlayerDied();
        }
        else
        {
         soldiers.Remove(gameObject);  
        }
     
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
       if( gameObject.TryGetComponent<EnemyMovement>(out var enemyMov))
        {
            enemyMov.GetComponent<EnemyMovement>().Died();
            enemyMov.enabled = false;
        }
       else
        {
            gameObject.GetComponent<PlayerTouchMovement>().enabled = false;

        }
        if(soldiers.Count<1)
        {
            LevelFinishedEnemy();
        }
        UpdateSlider();
    }
    public void EnemyDied(GameObject gameObject)
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<EnemyMovement>().Died();
        gameObject.GetComponent<EnemyMovement>().enabled = false;
        enemySoldiers.Remove(gameObject);
        if (enemySoldiers.Count < 1)
        {

            LevelFinishedSoldier();
        }
        UpdateSlider();
    }
    void LevelFinishedSoldier()
    {
       
        if (!isFinished)
        {
            isFinished = true;
            Debug.Log("Soldiers won");
            AudioManager.Instance.PlayBattleCryClip();
            for (int i = 0; i < soldiers.Count; i++)
            {
                soldiers[i].GetComponent<AnimController>().PlayCheer();
            }
            
            PlayerPrefs.SetInt("Soldiers", 0);
             PlayerPrefs.SetInt("SoldierLevel1", 0);
             PlayerPrefs.SetInt("SoldierLevel2", 0);
            for(int i=1;i<soldiers.Count;i++)
            {
               
                switch (soldiers[i].GetComponent<Soldier>().getSoldierLevel())
                {
                    case SoldierLevel.Level0: PlayerPrefs.SetInt("Soldiers",PlayerPrefs.GetInt("Soldiers")+1); break;
                    case SoldierLevel.Level1: PlayerPrefs.SetInt("SoldierLevel1", PlayerPrefs.GetInt("SoldierLevel1") + 1); break;
                    case SoldierLevel.Level2: PlayerPrefs.SetInt("SoldierLevel2", PlayerPrefs.GetInt("SoldierLevel2") + 1); break;
                }

            }
            int gold = PlayerPrefs.GetInt("EnemyCreated") * 10;
            goldText.text = gold.ToString();
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + gold);
            lostText.text = "YOU LOST "+(soldierCount - soldiers.Count).ToString()+" SOLDIERS";
            finishMenu.SetActive(true); 
        }
    }
    void LevelFinishedEnemy()
    {
        if (!isFinished)
        {
        isFinished = true;
        Debug.Log("Enemies won");
        AudioManager.Instance.PlayBattleCryClip();
        for (int i = 0; i < enemySoldiers.Count; i++)
        {
            enemySoldiers[i].GetComponent<AnimController>().PlayCheer();
        }
        
            PlayerPrefs.SetInt("Soldiers", 0);
            PlayerPrefs.SetInt("SoldierLevel1", 0);
            PlayerPrefs.SetInt("SoldierLevel2", 0);
            int gold = (PlayerPrefs.GetInt("EnemyCreated")-enemySoldiers.Count) * 10;
            goldText2.text = gold.ToString();
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + gold);
            gameoverMenu.SetActive(true);
        }
    }
    void UpdateSlider()
    {
        slider.maxValue = (soldiers.Count + enemySoldiers.Count);
        slider.value = soldiers.Count;
    }
    void PlayerDied()
    {
        spectatingText.SetActive(true);
        virtualCam.Follow = soldiers[0].transform;
        virtualCam.LookAt = soldiers[0].transform;
    }
}
