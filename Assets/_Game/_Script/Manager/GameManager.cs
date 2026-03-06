using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override bool dondestroy => true;
    private ESceneType sceneType = ESceneType.Splash;
    private EGameState gameState;

    public EGameState GameState => gameState;
    public ESceneType SceneType => sceneType;
    private bool isSpeed;
    public bool IsSpeed => isSpeed;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RegisterEvent<ContinueGameEvent>(OnContinueGame);
        EventDispatcher.RegisterEvent<PauseGameEvent>(OnPauseGame);
        EventDispatcher.RegisterEvent<EndGameEvent>(OnEndGame);
        EventDispatcher.RegisterEvent<ChangeSceneEvent>(OnChangeScene);
        EventDispatcher.RegisterEvent<ReviveGameEvent>(OnReviveGame);
        EventDispatcher.RegisterEvent<IncreaseSpeedGameEvent>(OnIncreaseSpeedGame);
        
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RemoveEvent<ContinueGameEvent>(OnContinueGame);
        EventDispatcher.RemoveEvent<PauseGameEvent>(OnPauseGame);
        EventDispatcher.RemoveEvent<EndGameEvent>(OnEndGame);
        EventDispatcher.RemoveEvent<ChangeSceneEvent>(OnChangeScene);
        EventDispatcher.RemoveEvent<ReviveGameEvent>(OnReviveGame);
        EventDispatcher.RemoveEvent<IncreaseSpeedGameEvent>(OnIncreaseSpeedGame);
    }
    void Start()
    {
        Application.targetFrameRate = 120;
        ServiceLocator.Installer();
        var scene = ServiceLocator.Get<SceneService>();
        scene.LoadScene(ESceneType.Home);
        ChangeGameState(EGameState.None);
    }
    private void StartGame()
    {
        ChangeGameState(EGameState.Playing);
    }
    private void ChangeGameState(EGameState state)
    {
        this.gameState = state;
    }
    private void OnStartGame(StartGameplayEvent param)
    {
        isSpeed = false;
        Invoke(nameof(StartGame), 1f);
    }
    private void OnContinueGame(ContinueGameEvent param)
    {
        ChangeGameState(EGameState.Playing);
    }
    private void OnPauseGame(PauseGameEvent param)
    {
        ChangeGameState(EGameState.Pause);
    }
    private void OnChangeScene(ChangeSceneEvent param)
    {
        this.sceneType = param.sceneType;
    }
    private void OnEndGame(EndGameEvent param)
    {
        if (gameState == EGameState.EndGame) return;
        ChangeGameState(EGameState.EndGame);
        if (param.isWin)
        {
            EventDispatcher.Dispatch(new WinGameEvent()
            {

            });
        }
        else
        {
            EventDispatcher.Dispatch(new LoseGameEvent()
            {
                loseType = param.loseType
            });
        }
    }
    private void OnReviveGame(ReviveGameEvent param)
    {
        ChangeGameState(EGameState.Playing);
    }
    private void OnIncreaseSpeedGame(IncreaseSpeedGameEvent param)
    {
        isSpeed = true;
    }
}
