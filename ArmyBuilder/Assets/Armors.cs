using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armors : MonoBehaviour
{
    [SerializeField] List<GameObject> armor1, armor2;
    // Start is called before the first frame update
    void Start()
    {
        
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
}

