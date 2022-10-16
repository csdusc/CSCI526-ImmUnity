using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBarScript : MonoBehaviour
{
    public Slider slider;
    public int maxCoins;
    public int currentCoins;
    public int totalCoinsCollected; // use this for analytics

    public void Init()
    {
        currentCoins = 0;
        totalCoinsCollected = 0;
        slider.value = currentCoins;
        slider.maxValue = maxCoins;
    }

    public void AddCoins(int coins)
    {
        totalCoinsCollected++;
        currentCoins = Mathf.Clamp(currentCoins + coins, 0, maxCoins);
        slider.value = currentCoins;
    }
}
