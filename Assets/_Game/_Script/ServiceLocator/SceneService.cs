using UnityEngine;

public class SceneService : BaseService
{
    public void LoadScene(ESceneType sceneType)
    {
        LoadSceneManager.Instance.LoadScene(sceneType);
    } 
    public override void Proc()
    {
    }
}