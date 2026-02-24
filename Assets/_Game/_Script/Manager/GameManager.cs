using UnityEngine;

public class GameManager : BLBMono
{
    void Start()
    {
        ServiceLocator.Installer();
        var scene = ServiceLocator.Get<SceneService>();
        scene.LoadScene(ESceneType.Home);
    }
}
