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
        float value = (float)param.level.Map.x / 2f;
        Tf.position = new Vector3(value, Tf.position.y, Tf.position.z);
    }
}
