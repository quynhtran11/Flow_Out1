using UnityEngine;

public class CameraGame : BLBMono
{
    [SerializeField] private Camera camMain;

    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<StartGameplayEvent>(OnStartGame);
    }

    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<StartGameplayEvent>(OnStartGame);
    }

    private void OnStartGame(StartGameplayEvent param)
    {
        float mapWidth = GameUntilities.SizeMap(param.level.AllCups);
        float mapHeight = param.level.Map.x;

        Tf.position = new Vector3(mapWidth / 2f, Tf.position.y, Tf.position.z);

        FitCamera(mapWidth, mapHeight);
    }

    private void FitCamera(float mapWidth, float mapHeight)
    {
        float padding = GameData.Instance.SizePaddingCam;

        mapWidth += padding;
        mapHeight += padding;

        float screenAspect = (float)Screen.width / Screen.height;
        float targetAspect = mapWidth / mapHeight;

        if (screenAspect >= targetAspect)
        {
            camMain.orthographicSize = mapHeight / 2f;
        }
        else
        {
            float difference = targetAspect / screenAspect;
            camMain.orthographicSize = mapHeight / 2f * difference;
        }
    }
}