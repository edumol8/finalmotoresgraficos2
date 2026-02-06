using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneStateBehavior : StateBehaviour
{
    private SceneController _sceneController;
    private SceneIndex _sceneIndex;
    private float _loadSceneDelay;

    private float _delayCountdown;
    private bool _sceneLoaded;

    public LoadSceneStateBehavior(SceneController sceneController, SceneIndex sceneIndex, float loadSceneDelay)
    {
        _sceneController = sceneController;
        _sceneIndex = sceneIndex;
        _loadSceneDelay = loadSceneDelay;
    }

    public override void Enter(float transitionDuration)
    {
        _delayCountdown = _loadSceneDelay;
        _sceneLoaded = false;
    }

    public override void Update()
    {
        _delayCountdown -= Time.deltaTime;

        if (_delayCountdown <= 0 && _sceneLoaded == false)
        {
            _sceneController.LoadScene(_sceneIndex);
            _sceneLoaded = true;
        }
    }
}
