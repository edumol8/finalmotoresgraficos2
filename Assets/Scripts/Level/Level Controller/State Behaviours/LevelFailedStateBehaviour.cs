using UnityEngine.SceneManagement;

public class LevelFailedStateBehaviour : StateBehaviour
{
    private readonly LivesController _livesController;
    private readonly PlayerInventory _playerInventory;
    private readonly SaveController _saveController;
    private readonly LevelCheckpointController _levelCheckpointController;
    private readonly LoadSceneStateBehavior _resetSceneStateBehaviour;
    private readonly LoadSceneStateBehavior _gameOverSceneStateBehaviour;

    public LevelFailedStateBehaviour(
        LivesController livesController,
        PlayerInventory playerInventory,
        SaveController saveController,
        SceneController sceneController,
        LevelCheckpointController levelCheckpointController,
        float resetSceneDelay)
    {
        _livesController = livesController;
        _playerInventory = playerInventory;
        _saveController = saveController;
        _levelCheckpointController = levelCheckpointController;
        _resetSceneStateBehaviour = new LoadSceneStateBehavior(sceneController, (SceneIndex)SceneManager.GetActiveScene().buildIndex, resetSceneDelay);
        _gameOverSceneStateBehaviour = new LoadSceneStateBehavior(sceneController, SceneIndex.GameOver, resetSceneDelay);
    }

    public override void Enter(float transitionDuration)
    {
        _livesController.RemoveLife();

        if (_livesController.NumberOfLives == 0)
        {
            _playerInventory.RemoveAllCoins();
            _levelCheckpointController.ClearCheckpoint();

            _gameOverSceneStateBehaviour.Enter(transitionDuration);
        }
        else
        {
            _resetSceneStateBehaviour.Enter(transitionDuration);
        }

        _saveController.SaveGame();
    }

    public override void Exit()
    {
        if (_livesController.NumberOfLives == 0)
        {
            _gameOverSceneStateBehaviour.Exit();
        }
        else
        {
            _resetSceneStateBehaviour.Exit();
        }
    }

    public override void Update()
    {
        if (_livesController.NumberOfLives == 0)
        {
            _gameOverSceneStateBehaviour.Update();
        }
        else
        {
            _resetSceneStateBehaviour.Update();
        }
    }
}