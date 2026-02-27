using UnityEngine;

public class SpawnManager : BLBMono
{
    [SerializeField] private GameObject waterPrefab;

    private CupElementService cupService;
    private StorageElementService storageService;
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
    private void SpawnCup(LevelInfor lv)
    {
        cupService = new CupElementService();
        for (int i = 0; i < lv.AllCups.Length; i++)
        {
            CupElement cup = SpanwObject<CupElement>(GameData.Instance.ElementInfor.GetData(EElementType.Cup).prefab);
            cup.gameObject.name = "Cup_" + i;
            Vector3 pos = new Vector3(lv.AllCups[i].pos.x, 0, lv.AllCups[i].pos.y);
            cup.Tf.position = pos;
            cup.Initilize(lv.AllCups[i]);
            cupService.RegisterObject(cup);
        }
    }
    private void SpawnStorage(LevelInfor lev)
    {
        storageService = new StorageElementService();
        int count = lev.AllStorages.Length;
        float spacing = 1f;
        GameObject storagePrefab = GameData.Instance.ElementInfor.GetData(EElementType.Storage).prefab;
        float storageWidth = storagePrefab.GetComponentInChildren<Renderer>().bounds.size.x;
        float totalWidth = count * storageWidth + (count - 1) * spacing;
        float center = GameUntilities.SizeMap(lev.AllCups) / 2f;
        for (int i = 0; i < count; i++)
        {
            StorageElement storage = SpanwObject<StorageElement>(storagePrefab);
            storage.gameObject.name = "Storage_" + i;
            float x = center- totalWidth / 2f+ storageWidth / 2f+ i * (storageWidth + spacing);
            storage.Tf.position = new Vector3(x, 0, 17f);
            storage.Initilize(lev.AllStorages[i]);
            storageService.RegisterObject(storage);
        }
    }
    private void SpawnAllObject(LevelInfor lev)
    {
        SpawnCup(lev);
        SpawnStorage(lev);
    }
    private void InitAllObject(LevelInfor level)
    {
        this.cupService.InitElement(level);
        this.storageService.InitElement(level);
    }
    private T SpanwObject<T>(GameObject go)
    {
        T t = Instantiate(go, transform).GetComponent<T>();
        return t;
    }
}
