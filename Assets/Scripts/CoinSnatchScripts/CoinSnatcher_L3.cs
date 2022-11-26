using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSnatcher_L3 : MonoBehaviour
{
    public CoinBarScript coinBar;
    public PlayerController_Level3 player;
    private bool canSnatchCoins;
    public float range;
    public CoinSnatcherAnimation snatcherAnimate;

    void Start()
    {
        canSnatchCoins = true;
    }

    void FixedUpdate()
    {
        // Debug.Log(Vector2.Distance(transform.position, playerTransform.position));
        if(!player.isShield && Vector2.Distance(transform.position, player.transform.position) <= range)
        {
            if(coinBar.currentCoins > 0 && canSnatchCoins){
                canSnatchCoins = false;
                coinBar.SubCoins(2);
                snatcherAnimate.startCoinSnatcher(transform.position);
                StartCoroutine(SleepTimer());
            }
        }
    }

    IEnumerator SleepTimer()
    {
        yield return new WaitForSeconds(1);
        canSnatchCoins = true;
    }
}
