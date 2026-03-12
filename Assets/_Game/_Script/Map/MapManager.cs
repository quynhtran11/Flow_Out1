using UnityEngine;

public class MapManager : BLBMono
{
    [SerializeField] private Transform boderLeft;
    [SerializeField] private Transform boderRight;
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
        float offset = 2;
        float value = GameUntilities.SizeMap(param.level.AllCups) / 2f;
        Tf.position = new Vector3(value, Tf.position.y, Tf.position.z);
        float X = (param.level.Map.x + 2) * offset;
        boderLeft.transform.localPosition = new Vector3(-X, boderLeft.transform.localPosition.y, boderLeft.transform.localPosition.z);
        boderRight.transform.localPosition = new Vector3(X, boderRight.transform.localPosition.y, boderRight.transform.localPosition.z);

    }
}
