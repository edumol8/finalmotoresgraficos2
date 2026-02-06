using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, ISaveableComponent
{
    private int _numberOfCoins;

    public int NumberOfCoins
    {
        get
        {
            return _numberOfCoins;
        }
        private set
        {
            _numberOfCoins = value;
            OnNumberOfCoinsChanged?.Invoke();
        }
    }

    public event Action OnNumberOfCoinsChanged;

    public void AddCoin()
    {
        NumberOfCoins += 1;
    }

    public void RemoveAllCoins()
    {
        NumberOfCoins = 0;
    }

    public JToken GetSaveData()
    {
        var jObject = new JObject();

        jObject["Coins"] = NumberOfCoins;

        return jObject;
    }

    public void RestoreSaveData(JToken saveData)
    {
        var jObject = saveData.ToObject<JObject>();

        if (jObject.ContainsKey("Coins"))
        {
            NumberOfCoins = jObject["Coins"].Value<int>();
        }
    }
}
