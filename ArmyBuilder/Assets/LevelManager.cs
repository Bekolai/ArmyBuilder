using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] barracks;
    [SerializeField] GameObject tent,blacksmith,armory;
    [SerializeField] List<GameObject> soldiers,soldiers2,soldiers3;
    [SerializeField] GameObject soldierPrefab;
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
        for (int i = 0; i < PlayerPrefs.GetInt("Soldiers");i++)
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

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddSingleSoldier()
    {
        int barrackNo = PlayerPrefs.GetInt("Soldiers") ;
        int soldierNo=0;
        switch((barrackNo-1)/20)
        {
            case 0: spawner = barracks[0].transform.GetChild(2).gameObject;soldierNo = PlayerPrefs.GetInt("Soldiers"); break;
            case 1: spawner = barracks[1].transform.GetChild(2).gameObject; soldierNo = PlayerPrefs.GetInt("Soldiers")-20; break;
            case 2: spawner = barracks[2].transform.GetChild(2).gameObject; soldierNo = PlayerPrefs.GetInt("Soldiers")-40; break;
        }
        if(soldierNo!=0)
        {
        soldierNo++;
        }
        Vector3 spawnLoc = Vector3.zero;
        if ((soldierNo % 2) == 0)
        {
            spawnLoc.x = 0.4f;
        }
        else
            spawnLoc.x = -0.4f;
        spawnLoc.z = (int)(soldierNo / 2) * 0.5f;


        GameObject soldier = Instantiate(soldierPrefab, spawner.transform.position + spawnLoc, Quaternion.Euler(0, 180, 0), spawner.transform);
        soldiers.Add(soldier);
    }
}
