using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armors : MonoBehaviour
{
    [SerializeField] List<GameObject> armor1, armor2;
    [SerializeField] List<GameObject> swordBelt, swordHand;
    bool WarMap;
    // Start is called before the first frame update
    void Start()
    {
        WarMap = GameManager.Instance.WarMap();

      if (WarMap)
        {
            swordHand[0].SetActive(true);
        }
      else
        {
            swordBelt[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    /* if(Input.GetKeyDown(KeyCode.Z))
        {
            foreach(GameObject armor in armor1)
            {
                armor.SetActive(true);
                foreach (GameObject otherarmor in armor2)
                { 
                otherarmor.SetActive(false);
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (GameObject armor in armor2)
            {
                armor.SetActive(true);
                foreach (GameObject otherarmor in armor1)
                {
                    otherarmor.SetActive(false);
                }

            }
        }*/
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
        if (WarMap)
        { 
            swordHand[0].SetActive(true);
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
        if (WarMap)
        {
            swordHand[0].SetActive(false);
            swordHand[1].SetActive(false);
            swordHand[2].SetActive(true);
        }
        else
        {
            swordBelt[0].SetActive(false);
            swordBelt[1].SetActive(false);
            swordBelt[2].SetActive(true);
        }
    }
}

