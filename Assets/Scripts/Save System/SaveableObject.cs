using UnityEngine;
using System;
using Newtonsoft.Json.Linq;


#if UNITY_EDITOR
using UnityEditor;
#endif


[ExecuteAlways]
public class SaveableObject : MonoBehaviour
{
    [field: SerializeField]
    public string Identifier { get; private set; }

    private ISaveableComponent[] _saveableComponents;

    private void Awake()
    {
        _saveableComponents = GetComponents<ISaveableComponent>();

#if UNITY_EDITOR
        if (Application.isPlaying == false && string.IsNullOrEmpty(Identifier))
        {
            Identifier = Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this);
        }
#endif
    }

    public JObject GetSaveData()
    {
        var saveData = new JObject();

        foreach (var saveableComponent in _saveableComponents)
        {
            saveData[saveableComponent.GetType().ToString()] = saveableComponent.GetSaveData();
        }

        return saveData;
    }

    public void RestoreSaveData(JObject saveData)
    {
        foreach (var saveableComponent in _saveableComponents)
        {
            string componentType = saveableComponent.GetType().ToString();

            if (saveData.ContainsKey(componentType))
            {
                var componentSaveData = saveData[componentType];
                saveableComponent.RestoreSaveData(componentSaveData);
            }
        }
    }
}