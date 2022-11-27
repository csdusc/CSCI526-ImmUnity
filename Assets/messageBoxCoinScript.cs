using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messageBoxCoinScript : MonoBehaviour
{
    public GameObject messageboxSuccess, messageboxFailure;
    public CoinBarScript coinBar;
    public int coins;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (coinBar.totalCoinsCollected < coins){
                messageboxFailure.SetActive(true);
                messageboxSuccess.SetActive(false);
            }
            else{
                messageboxSuccess.SetActive(true);
                messageboxFailure.SetActive(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        messageboxFailure.SetActive(false);
        messageboxSuccess.SetActive(false);
    }
}
