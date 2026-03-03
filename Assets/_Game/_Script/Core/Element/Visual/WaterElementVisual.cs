using DG.Tweening;
using UnityEngine;

public class WaterElementVisual : BaseElementVisual<WaterData>
{
    [SerializeField] private SpriteRenderer mesh;
    private Transform targetEnd;
    private float speed;
    private void LoadColor(EColorType type)
    {
        Color c = GameData.Instance.ColorData.GetData(type).color;

        mesh.color = c;
    }
    public override void AfterInit()
    {
        LoadColor(data.color);
        Tf.position = new Vector3(Tf.position.x, Tf.position.y, Tf.position.z + 10f);
        Tf.DOKill();
        float delay = (data.waterID * .1f) + .2f;
        Tf.DOMove(centerPos, .5f).SetDelay(delay).SetEase(Ease.OutBack, .3f);
        speed = Mathf.Clamp(GameData.Instance.SpeedWaterFill, 0, GameData.Instance.SpeedWaterFill);
    }
    public void RegisterTarget(Transform tf)
    {
        targetEnd = tf; 
    }
    public void WaterFill()
    {
        Tf.DOMove(targetEnd.position, speed).OnComplete(() =>
        {
            // test
            mesh.gameObject.SetActive(false);
        });
    }
    public void ChangeSpeedWater()
    {
        speed = Mathf.Clamp(GameData.Instance.SpeedWaterFillEndGame, 0, GameData.Instance.SpeedWaterFillEndGame);
    }
}