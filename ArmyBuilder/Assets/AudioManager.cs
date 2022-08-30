using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buyClip, buyfailCLip, buildClip, upgradeClip,warHornClip;

    public static AudioManager Instance { get; private set; }
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBuy()
    {
        audioSource.PlayOneShot(buyClip, 0.6f);
    }
    public void PlayBuyFail()
    {
        audioSource.PlayOneShot(buyfailCLip, 0.6f);
    }
    public void PlayBuild()
    {
        audioSource.PlayOneShot(buildClip, 0.6f);
    }
    public void PlayUpgrade()
    {
        audioSource.PlayOneShot(upgradeClip, 0.6f);
    }
    public void PlayWarHorn()
    {
        audioSource.PlayOneShot(warHornClip, 0.6f);
    }

}
