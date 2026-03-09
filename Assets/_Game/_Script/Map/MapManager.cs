using UnityEngine;

public class MapManager : BLBMono
{
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
        float value = GameUntilities.SizeMap(param.level.AllCups) / 2f;
        Tf.position = new Vector3(value, Tf.position.y, Tf.position.z);
    }
}
