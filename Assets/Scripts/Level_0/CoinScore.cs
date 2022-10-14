using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScore : MonoBehaviour
{
    TextMeshProUGUI counterText;
    public bool textFieldEnabledcoins = false;
    public string textFieldTextcoins = "Good Job on collecting required coins!";
    public Platform2_Script plat_scr;
    public GameObject Panel_coins_all;
    public GameObject Insuff_coins_panel;
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();   
    }

   void OnGUI() 
	{
         if (textFieldEnabledcoins) 
	  {
             textFieldTextcoins = GUI.TextField(new Rect(480, 120, 232, 27), textFieldTextcoins);
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
                      
                      if (Panel_coins_all.activeInHierarchy == true)

				{
                           Panel_coins_all.SetActive(false);
				}
				else

			{	    
                            Insuff_coins_panel.SetActive(false);
                            Panel_coins_all.SetActive(true);
				    
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
