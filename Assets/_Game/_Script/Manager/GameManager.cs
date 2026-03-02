using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override bool dondestroy => true;
    private ESceneType sceneType = ESceneType.Splash;
    private EGameState gameState;

    public EGameState GameState => gameState;
    public ESceneType SceneType => sceneType;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RegisterEvent<ContinueGameEvent>(OnContinueGame);
        EventDispatcher.RegisterEvent<PauseGameEvent>(OnPauseGame);
        EventDispatcher.RegisterEvent<EndGameEvent>(OnEndGame);
        EventDispatcher.RegisterEvent<ChangeSceneEvent>(OnChangeScene);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RemoveEvent<ContinueGameEvent>(OnContinueGame);
        EventDispatcher.RemoveEvent<PauseGameEvent>(OnPauseGame);
        EventDispatcher.RemoveEvent<EndGameEvent>(OnEndGame);
        EventDispatcher.RemoveEvent<ChangeSceneEvent>(OnChangeScene);
    }
    void Start()
    {
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
}
