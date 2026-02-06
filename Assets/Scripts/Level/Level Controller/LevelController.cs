using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LevelController : MonoBehaviour, ISaveableComponent, IPostRestoreSaveDataComponent
{
    [SerializeField]
    private SceneIndex _nextLevel;

    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private SceneController _sceneController;

    [SerializeField]
    private SaveController _saveController;

    [SerializeField]
    private CutsceneController _cutsceneController;

    [SerializeField]
    private TimelineAsset _levelIntroductionCutscene;

    [SerializeField]
    private TimelineAsset _levelCompleteCutscene;

    [SerializeField]
    private Transform _startPortalCentrePointTransform;

    [SerializeField]
    private float _resetSceneDelay;

    [SerializeField]
    private PlaylistController _playlistController;

    private bool _isFirstTime = true;

    public JToken GetSaveData()
    {
        var jObject = new JObject();
        jObject["IsFirstTime"] = _isFirstTime;

        return jObject;
    }

    public void RestoreSaveData(JToken saveData)
    {
        var jObject = saveData.ToObject<JObject>();

        if (jObject.ContainsKey("IsFirstTime"))
        {
            _isFirstTime = jObject["IsFirstTime"].Value<bool>();            
        }
    }

    public void PostRestoreSaveData()
    {
        var stateMachine = GetComponent<StateMachine>();

        var playerHealthController = _player.GetComponent<HealthController>();
        var playerUnderwaterDetector = _player.GetComponent<WaterDetector>();
        var playerEndPortalDetector = _player.GetComponent<EndPortalDetector>();
        var playerLivesController = _player.GetComponent<LivesController>();
        var playerInventory = _player.GetComponent<PlayerInventory>();
        var playerCheckpointController = _player.GetComponent<LevelCheckpointController>();

        var initialiseState = new State();
        var introCutsceneState = new State();
        var playState = new State();
        var completeCustsceneState = new State();
        var completeState = new State();
        var failedState = new State();

        initialiseState.AddStateBehaviour(new InitialisePlayerPositionStateBehaviour(_player.transform, _startPortalCentrePointTransform, playerCheckpointController));

        if (_isFirstTime)
        {
            initialiseState.AddStateTransition(new InstantStateTransition(introCutsceneState, 0));
            _isFirstTime = false;
        }
        else
        {
            initialiseState.AddStateTransition(new InstantStateTransition(playState, 0f));
        }

        introCutsceneState.AddStateBehaviour(new PlayCutsceneStateBehaviour(_cutsceneController, _levelIntroductionCutscene));
        introCutsceneState.AddStateTransition(new CutsceneNotPlayingStateTransition(playState, 1f, _cutsceneController));

        playState.AddStateBehaviour(new EnableComponentStateBehaviour(_playlistController));
        playState.AddStateTransition(new DiedStateTransition(failedState, 0, playerHealthController));
        playState.AddStateTransition(new PlayerUnderwaterStateTransition(failedState, 0, playerUnderwaterDetector));
        playState.AddStateTransition(new EndPortalReachedStateTransition(completeCustsceneState, 0.5f, playerEndPortalDetector));

        completeCustsceneState.AddStateBehaviour(new PlayCutsceneStateBehaviour(_cutsceneController, _levelCompleteCutscene));
        completeCustsceneState.AddStateTransition(new ElapsedTimeStateTransition(completeState, 0.2f, (float)_levelCompleteCutscene.duration - 1f));

        completeState.AddStateBehaviour(new LevelCompleteStateBehaviour(_saveController, _sceneController, playerCheckpointController, _nextLevel));

        failedState.AddStateBehaviour(new LevelFailedStateBehaviour(playerLivesController, playerInventory, _saveController, _sceneController, playerCheckpointController, _resetSceneDelay));

        stateMachine.SwitchState(initialiseState, 0);
    }    
}
