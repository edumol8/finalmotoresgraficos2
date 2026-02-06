using Newtonsoft.Json.Linq;
using UnityEngine;

public class LevelCheckpointController : MonoBehaviour, ISaveableComponent
{
    public Vector3? CurrentCheckpointPosition { get; private set; }

    public Quaternion? CurrentCheckpointRotation { get; private set; }

    public void SetCurrentCheckpoint(LevelCheckpoint checkPoint)
    {
        CurrentCheckpointPosition = checkPoint.transform.position;
        CurrentCheckpointRotation = checkPoint.transform.rotation;
    }

    public void ClearCheckpoint()
    {
        CurrentCheckpointPosition = null;
        CurrentCheckpointRotation = null;
    }

    public JToken GetSaveData()
    {
        var saveData = new JObject();

        if (CurrentCheckpointPosition.HasValue == false)
        {
            return saveData;
        }

        saveData["Position"] = JObject.FromObject(new SerializableVector3(CurrentCheckpointPosition.Value));
        saveData["Rotation"] = JObject.FromObject(new SerializableQuaternion(CurrentCheckpointRotation.Value));

        return saveData;
    }

    public void RestoreSaveData(JToken saveData)
    {
        var jObject = saveData.ToObject<JObject>();

        if (jObject.ContainsKey("Position"))
        {
            CurrentCheckpointPosition = jObject["Position"].ToObject<SerializableVector3>().ToVector3();
        }

        if (jObject.ContainsKey("Rotation"))
        {
            CurrentCheckpointRotation = jObject["Rotation"].ToObject<SerializableQuaternion>().ToQuaternion();
        }
    }
}
