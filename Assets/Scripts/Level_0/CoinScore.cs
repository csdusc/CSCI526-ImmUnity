using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScore : MonoBehaviour
{
    TextMeshProUGUI counterText;
    public bool textFieldEnabledcoins = false;
    public string textFieldTextcoins = "Good Job on collecting all coins!";
    public Platform2_Script plat_scr;
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();   
    }

   void OnGUI() 
	{
         if (textFieldEnabledcoins) 
	  {
             textFieldTextcoins = GUI.TextField(new Rect(480, 120, 220, 30), textFieldTextcoins);
         }
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

                  if (CoinCollection.totalCoins == 3)
                  {
                   textFieldEnabledcoins=true;
                   if (plat_scr.textFieldEnabled2==true)
                   {
                   	plat_scr.textFieldEnabled2=false;
                   }
                   
              
                  }
            }
            else
            {
                counterText.text = "3";
            }
        }
    }
}
