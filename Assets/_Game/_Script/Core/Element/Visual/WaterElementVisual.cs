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
        Tf.position = new Vector3(Tf.position.x, Tf.position.y + 10f, Tf.position.z );
        Tf.DOKill();
        float delay = (data.waterID * .1f) + .2f;
        Tf.DOMove(centerPos, .5f).SetDelay(delay).SetEase(Ease.OutBack, .3f);
        speed = Mathf.Clamp(GameData.Instance.GetSpeedWaterFill(), 0, GameData.Instance.GetSpeedWaterFill());
    }
    public void RegisterTarget(Transform tf)
    {
        targetEnd = tf; 
    }
    public void WaterFill()
    {
        Tf.DOMove(targetEnd.position, speed+.05f).OnComplete(() =>
        {
            // test
            mesh.gameObject.SetActive(false);
        });
    }
    public void ChangeSpeedWater()
    {
        speed = Mathf.Clamp(GameData.Instance.GetSpeedWaterFill(), 0, GameData.Instance.GetSpeedWaterFill());
    }
}