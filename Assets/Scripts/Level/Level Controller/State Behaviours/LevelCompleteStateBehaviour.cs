using UnityEngine.SceneManagement;

public class LevelCompleteStateBehaviour : StateBehaviour
{
    private readonly SaveController _saveController;
    private readonly LevelCheckpointController _levelCheckpointController;
    private readonly LoadSceneStateBehavior _loadNextSceneStateBehaviour;

    public LevelCompleteStateBehaviour(
        SaveController saveController,
        SceneController sceneController,
        LevelCheckpointController levelCheckpointController,
        SceneIndex nextScene)
    {
        _saveController = saveController;
        _levelCheckpointController = levelCheckpointController;
        _loadNextSceneStateBehaviour = new LoadSceneStateBehavior(sceneController, nextScene, 0);
    }

    public override void Enter(float transitionDuration)
    {
        _levelCheckpointController.ClearCheckpoint();
        _loadNextSceneStateBehaviour.Enter(transitionDuration);

        _saveController.SaveGame();
    }

    public override void Exit()
    {
        _loadNextSceneStateBehaviour.Exit();
    }

    public override void Update()
    {
        _loadNextSceneStateBehaviour.Update();
    }
}