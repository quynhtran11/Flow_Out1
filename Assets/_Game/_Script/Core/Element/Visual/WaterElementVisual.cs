using DG.Tweening;
using UnityEngine;

public class WaterElementVisual : BaseElementVisual<WaterData>
{
    [SerializeField] private SpriteRenderer mesh;
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
        Tf.DOMove(centerPos, .5f).SetDelay(delay).SetEase(Ease.OutBack,.3f);
    }
}
