using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToWar : MonoBehaviour
{
    [SerializeField] PlayerTouchMovement playerTouch;
    [SerializeField] GameObject uiObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTouch.StopMovement();
            uiObject.SetActive(true);
            AudioManager.Instance.PlayWarHorn();
        }
    }
}
