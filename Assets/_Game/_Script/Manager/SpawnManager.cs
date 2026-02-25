using UnityEngine;

public class SpawnManager : BLBMono
{
    [SerializeField] private GameObject blockPrefab;

    private BlockElementService blockService;
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
        blockService = new BlockElementService();
        for (int i = 0; i < lv.AllBlocks.Length; i++)
        {
            BlockElement block = SpanwObject<BlockElement>(blockPrefab);
            block.gameObject.name = "Block_" + i;
            Vector3 pos = new Vector3(lv.AllBlocks[i].pos.x, 0, lv.AllBlocks[i].pos.y);
            block.Tf.position = pos;
            block.Initilize(lv.AllBlocks[i]);
            blockService.RegisterBlock(block);
        }
    }
    private void InitAllObject(LevelInfor level)
    {
        this.blockService.InitElement(level);
    }
    private T SpanwObject<T>(GameObject go)
    {
        T t = Instantiate(go, transform).GetComponent<T>();
        return t;
    }
}
