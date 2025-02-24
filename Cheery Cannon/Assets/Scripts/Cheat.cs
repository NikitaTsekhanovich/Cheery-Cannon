using PlayerData;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    private int _counter;

    public void ClickLogo()
    {
        _counter++;
        if (_counter % 10 == 0)
        {
            Debug.Log("Cheater!");
            var currentCoins = PlayerPrefs.GetInt($"{PlayerDataKeys.CoinsKey}");
            PlayerPrefs.SetInt($"{PlayerDataKeys.CoinsKey}", currentCoins + 1000);
        }
    }
}
