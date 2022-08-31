using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] int healthPoint, damage;
    [SerializeField] SoldierLevel soldierLevel;
    [SerializeField] AttackRadius attackRadius;
    [SerializeField] Enemy enemy;
    [SerializeField] Player player;
    public bool isPlayer,isVillage;
    void Start()
    {
        if (isPlayer && !isVillage)
        {
            player.UpdateHealth(healthPoint);
            attackRadius.UpdateDamage(damage);
        }
        else if(!isVillage)
        {
            enemy.UpdateHealth(healthPoint);
            attackRadius.UpdateDamage(damage);
        }
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
        //hp and damage upgrade.
        if (isPlayer && !isVillage)
        {
            player.UpdateHealth(healthPoint);
            attackRadius.UpdateDamage(damage);
        }
        else if(!isVillage)
        {
            enemy.UpdateHealth(healthPoint);
            attackRadius.UpdateDamage(damage);
        }
    }
    public void SoldierLevel2()
    {
        soldierLevel = SoldierLevel.Level2;
        GetComponent<Armors>().Level2();
        healthPoint += 50;
        damage += 15;
        //hp and damage upgrade
        if (isPlayer && !isVillage)
        {
            player.UpdateHealth(healthPoint);
            attackRadius.UpdateDamage(damage);
        }
        else if (!isVillage)
        {
            enemy.UpdateHealth(healthPoint);
            attackRadius.UpdateDamage(damage);
        }
    }
    public int GetDamage()
    {
        return damage;
    }
    public int GetHP()
    {
        return healthPoint;
    }
}
