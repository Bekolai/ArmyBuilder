using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] int healthPoint, damage;
    [SerializeField] SoldierLevel soldierLevel;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public SoldierLevel getSoldierLevel()
    {
        return soldierLevel;
    }
    public void SoldierLevel1()
    {
        soldierLevel = SoldierLevel.Level1;
        GetComponent<Armors>().Level1();
        healthPoint += 20;
        damage += 5;
        //hp and damage upgrade
    }
    public void SoldierLevel2()
    {
        soldierLevel = SoldierLevel.Level2;
        GetComponent<Armors>().Level2();
        healthPoint += 50;
        damage += 15;
        //hp and damage upgrade
    }
}
