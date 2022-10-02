using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScore : MonoBehaviour
{
    TextMeshProUGUI counterText;
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (counterText.text != CoinCollection.totalCoins.ToString()) 
        {
            counterText.text = CoinCollection.totalCoins.ToString();
            if(CoinCollection.totalCoins <= 3 )
            {
                counterText.text = CoinCollection.totalCoins.ToString();
            }
            else
            {
                counterText.text = "3";
            }
        }
    }
}
