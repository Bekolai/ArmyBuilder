using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int gold;



    public static GameManager Instance { get; private set; }
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

    }

    // Update is called once per frame
    void Update()
    {

    }
    public int GetGold()
    {
        return gold;
    }
    public void BuySuccesfull(int price,string Bought)
    {
        gold -= price;
        switch(Bought)
        {
            case "Blacksmith":break;
            case "Armory": break;
            case "Barracks": break;
        }
    }
}
