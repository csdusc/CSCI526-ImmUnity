using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBarScript : MonoBehaviour
{
    public Slider slider;
    public int maxCoins;
    public int currentCoins;

    public void Init()
    {
        currentCoins = 0;
        slider.value = currentCoins;
        slider.maxValue = maxCoins;
    }

    public void AddCoins(int coins)
    {
        currentCoins = Mathf.Clamp(currentCoins + coins, 0, maxCoins);
        slider.value = currentCoins;
    }
}
