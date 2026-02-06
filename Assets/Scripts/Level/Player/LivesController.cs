using Newtonsoft.Json.Linq;
using UnityEngine;

public class LivesController : MonoBehaviour, ISaveableComponent
{
    [field:SerializeField]
    public int NumberOfLives { get; private set; }
    
    public void RemoveLife()
    {
        if (NumberOfLives == 0)
        {
            return;
        }

        NumberOfLives--;
    }

    public void AddLife()
    {
        NumberOfLives++;
    }

    public JToken GetSaveData()
    {
        var jObject = new JObject();
        jObject["Lives"] = NumberOfLives;

        return jObject;
    }

    public void RestoreSaveData(JToken saveData)
    {
        var jObject = saveData.ToObject<JObject>();

        if (jObject.ContainsKey("Lives"))
        {
            int savedNumberOfLives = jObject["Lives"].Value<int>();

            if (savedNumberOfLives > 0)
            {
                NumberOfLives = savedNumberOfLives;
            }
        }
    }
}
