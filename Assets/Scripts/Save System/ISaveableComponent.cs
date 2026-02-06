using Newtonsoft.Json.Linq;

public interface ISaveableComponent
{
    JToken GetSaveData();

    void RestoreSaveData(JToken saveData);
}
