using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : BLBMono
{
    [SerializeField] private Transform boderLeft;
    [SerializeField] private Transform boderRight;
    [SerializeField] private Image fillLevel;
    private int currentCup;
    private int maxIndexCup;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RegisterEvent<ClearCupEvent>(OnClearCup);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RemoveEvent<ClearCupEvent>(OnClearCup);
    }
    private void OnStartGame(StartGameplayEvent param)
    {
        float offset = 2;
        float value = GameUntilities.SizeMap(param.level.AllCups) / 2f;
        Tf.position = new Vector3(value, Tf.position.y, Tf.position.z);
        float X = (param.level.Map.x + 2) * offset;
        boderLeft.transform.localPosition = new Vector3(-X, boderLeft.transform.localPosition.y, boderLeft.transform.localPosition.z);
        boderRight.transform.localPosition = new Vector3(X, boderRight.transform.localPosition.y, boderRight.transform.localPosition.z);
        maxIndexCup = param.level.AllCups.Length;
        currentCup = 0;
        FillLevel();
    }
    private void OnClearCup(ClearCupEvent param)
    {
        currentCup++;
        FillLevel();
    }
    private void FillLevel()
    {
        float t = (float)currentCup / (float)maxIndexCup;
        fillLevel.DOKill();
        fillLevel.DOFillAmount(t, .3f);
    }
}
