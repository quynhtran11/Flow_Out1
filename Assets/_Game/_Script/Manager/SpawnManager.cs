using UnityEngine;

public class SpawnManager : BLBMono
{
    [SerializeField] private GameObject cupPrefab;

    private CupElementService cupService;
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
        SpawnAllObject(param.level);
        InitAllObject(param.level);
    }
    private void SpawnAllObject(LevelInfor lev)
    {
        LevelInfor lv = lev;
        cupService = new CupElementService();
        for (int i = 0; i < lv.AllCups.Length; i++)
        {
            CupElement cup = SpanwObject<CupElement>(cupPrefab);
            cup.gameObject.name = "Block_" + i;
            Vector3 pos = new Vector3(lv.AllCups[i].pos.x, 0, lv.AllCups[i].pos.y);
            cup.Tf.position = pos;
            cup.Initilize(lv.AllCups[i]);
            cupService.RegisterBlock(cup);
        }
    }
    private void InitAllObject(LevelInfor level)
    {
        this.cupService.InitElement(level);
    }
    private T SpanwObject<T>(GameObject go)
    {
        T t = Instantiate(go, transform).GetComponent<T>();
        return t;
    }
}
