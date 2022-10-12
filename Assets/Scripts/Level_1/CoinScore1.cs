using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScore1 : MonoBehaviour
{
    TextMeshProUGUI counterText;
    public bool textFieldEnabledcoins = false;
    // public string textFieldTextcoins = "Good Job on collecting required coins!";
    // public Platform2_Script plat_scr;
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
