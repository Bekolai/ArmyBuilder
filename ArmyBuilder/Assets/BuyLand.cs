using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuyLand : MonoBehaviour
{
    [SerializeField] int gold;
    [SerializeField] GameObject land;
    [SerializeField] Slider slider;
    [SerializeField] float BuyTime=2f;
    [SerializeField] TMP_Text goldText;
    bool canBuy;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
          
          if (PlayerPrefs.GetInt("Gold") >= gold)
            {
            canBuy = true;
            slider.value = 0;
            slider.gameObject.SetActive(true);
            StartCoroutine(startBuying());
            }
          else
            {
                Debug.Log("not enough gold");
            }
        }
       
       
    }
    void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            turnoffSlider();
        }
      
    }
    IEnumerator startBuying()
    {
        for(int i = 0; i < 100; i++)
        {
            if(!canBuy)
            {
                break;
            }
            slider.value+=1;  
            yield return new WaitForSeconds(BuyTime/100);
        }
        if(slider.value>=100)
        {

            turnoffSlider();
            this.gameObject.SetActive(false);
            land.SetActive(true);
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - gold);
            ShopManager.Instance.BoughtLand(transform.parent.gameObject.name);
            GameManager.Instance.UpdateTextUI();
            Debug.Log("Bougt");
        }
       
     
    }
    void turnoffSlider()
    {
        canBuy = false;
        slider.value = 0;
        slider.gameObject.SetActive(false);
    }
    void Start()
    {
        goldText.text = $"BUY {gameObject.transform.parent.name} \n {gold} GOLD";
    }
}
