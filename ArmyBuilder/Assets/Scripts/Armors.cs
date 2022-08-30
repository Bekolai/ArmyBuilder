using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armors : MonoBehaviour
{
    [SerializeField] List<GameObject> armor1, armor2;
    [SerializeField] List<GameObject> swordBelt, swordHand;
    bool WarMap;
    // Start is called before the first frame update
    void Awake()
    {



        Level0();
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    public void Level0()
    {
        if (GameManager.Instance.WarMap())
        {
            swordHand[0].SetActive(true);
        }
        else
        {
            swordBelt[0].SetActive(true);
        }

    }
    public void Level1()
    {
        foreach (GameObject armor in armor2)
        {
            armor.SetActive(true);
            foreach (GameObject otherarmor in armor1)
            {
                otherarmor.SetActive(false);
            }

        }
        if (GameManager.Instance.WarMap())
        { 
            swordHand[0].SetActive(false);
            swordHand[1].SetActive(true);
    
        }
        else
        {
            swordBelt[0].SetActive(false);
            swordBelt[1].SetActive(true);
            
        }
    }
    public void Level2()
    {
        foreach (GameObject armor in armor1)
        {
            armor.SetActive(true);
            foreach (GameObject otherarmor in armor2)
            {
                otherarmor.SetActive(false);
            }

        }
        
        if (GameManager.Instance.WarMap())
        {
            
            swordHand[2].SetActive(true);
            swordHand[0].SetActive(false);
            swordHand[1].SetActive(false);
        
        }
        else
        {
          
            swordBelt[2].SetActive(true);
            swordBelt[0].SetActive(false);
            swordBelt[1].SetActive(false);
            
        }
    }
}

