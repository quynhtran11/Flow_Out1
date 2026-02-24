using UnityEngine;

public class PlayIngameButton : BaseButton
{
    protected override void Click()
    {
        var scene = ServiceLocator.Get<SceneService>();
        scene.LoadScene(ESceneType.Ingame);
    }
}
