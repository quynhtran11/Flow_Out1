using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager :Singleton<LoadSceneManager>
{
    [SerializeField] private LoadingUI load;
    protected override bool dondestroy => true;

    public void LoadScene(ESceneType sceneType)
    {
        AsyncOperation ope = SceneManager.LoadSceneAsync((int)sceneType);
        if (load == null) return;
        load.gameObject.SetActive(true);
        load.Open();
        load.Loading(ope);
    }
}
