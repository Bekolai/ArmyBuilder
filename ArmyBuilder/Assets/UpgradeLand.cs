using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLand : MonoBehaviour
{
    [SerializeField] PlayerTouchMovement playerTouch;
    [SerializeField] GameObject uiObject;
    // Start is called before the first frame update
     void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerTouch.StopMovement();
            uiObject.SetActive(true);
        }
    }

}
