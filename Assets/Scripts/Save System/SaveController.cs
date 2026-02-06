using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using System;

public class SaveController : MonoBehaviour
{
    [SerializeField]
    private bool _restoreSaveDataOnStart;

    private SaveableObject[] _saveableObjects;
    private IPostRestoreSaveDataComponent[] _postRestoreSaveDataComponents;
    private JObject _saveData;
    private string _saveDataFilePath;
    private string _sceneIndexFilePath;

    private void Awake()
    {
        _saveDataFilePath = Path.Combine(Application.persistentDataPath, "Save.dat");
        _sceneIndexFilePath = Path.Combine(Application.persistentDataPath, "Index.dat");

        _saveableObjects = FindObjectsOfType<SaveableObject>();
        _postRestoreSaveDataComponents = FindObjectsOfType<MonoBehaviour>().OfType<IPostRestoreSaveDataComponent>().ToArray();
    }

    private void Start()
    {
        if (_restoreSaveDataOnStart)
        {
            RestoreSaveData();
        }
    }

    public void SaveGame()
    {
        if (_saveData == null)
        {
            _saveData = LoadSaveData();
        }

        foreach (var saveableObject in _saveableObjects)
        {
            _saveData[saveableObject.Identifier] = saveableObject.GetSaveData();
        }

        File.WriteAllText(_saveDataFilePath, _saveData.ToString());
        File.WriteAllText(_sceneIndexFilePath, SceneManager.GetActiveScene().buildIndex.ToString());
    }

    public bool SaveDataExists()
    {
        return File.Exists(_saveDataFilePath);
    }

    public int GetSavedSceneIndex()
    {
        string sceneIndexString = File.ReadAllText(_sceneIndexFilePath);

        return Convert.ToInt32(sceneIndexString);
    }

    public void ClearSaveData()
    {
        if (File.Exists(_saveDataFilePath))
        {
            File.Delete(_saveDataFilePath);
        }

        if (File.Exists(_sceneIndexFilePath))
        {
            File.Delete(_sceneIndexFilePath);
        }
    }

    public void RestoreSaveData()
    {
        if (_saveData == null)
        {
            _saveData = LoadSaveData();
        }

        foreach (var saveableObject in _saveableObjects)
        {
            if (_saveData.ContainsKey(saveableObject.Identifier))
            {
                var objectSaveData = _saveData[saveableObject.Identifier];
                saveableObject.RestoreSaveData((JObject)objectSaveData);
            }
        }

        foreach (var postRestoreSaveDataComponent in _postRestoreSaveDataComponents)
        {
            postRestoreSaveDataComponent.PostRestoreSaveData();
        }
    }

    private JObject LoadSaveData()
    {
        if (File.Exists(_saveDataFilePath) == false)
        {
            return new JObject();
        }

        string fileContent = File.ReadAllText(_saveDataFilePath);

        return JObject.Parse(fileContent);
    }    
}
