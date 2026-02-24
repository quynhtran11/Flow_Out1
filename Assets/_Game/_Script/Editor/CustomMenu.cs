using UnityEditor;
using UnityEditor.SceneManagement;
public class CustomMenu
{
    [MenuItem("Water_Loop/PlayGame")]
    private static void ClickMe()
    {
        string path = "Assets/_Game/_Scenes/Splash.unity";
        if (System.IO.File.Exists(path))
        {
            EditorSceneManager.OpenScene(path);
            EditorApplication.isPlaying = true;
        }
        else
        {
            EditorUtility.DisplayDialog("Scene", "Not Find Scene", "Ok");
        }
    }
}
