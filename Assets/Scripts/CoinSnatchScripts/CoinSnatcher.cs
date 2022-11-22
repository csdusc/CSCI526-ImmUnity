using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSnatcher : MonoBehaviour
{
    public CoinBarScript coinBar;
    public Transform playerTransform;
    private bool canSnatchCoins;
    public int range;
    public CoinSnatcherAnimation snatcherAnimate;

    void Start()
    {
        canSnatchCoins = true;
    }

    void FixedUpdate()
    {
        Debug.Log(Vector2.Distance(transform.position, playerTransform.position));
        if(Vector2.Distance(transform.position, playerTransform.position) <= range){
            if(coinBar.currentCoins > 0 && canSnatchCoins){
                canSnatchCoins = false;
                coinBar.SubCoins(1);
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
