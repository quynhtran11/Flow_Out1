using DG.Tweening;
using UnityEngine;

public class WaterElementVisual : BaseElementVisual<WaterData>
{
    [SerializeField] private SpriteRenderer mesh;
    [SerializeField] private SpriteRenderer meshBorder;
    private Transform targetEnd;
    private float speed;
    private Color color;
    public SpriteRenderer Mesh => mesh;
    private void LoadColor(EColorType type)
    {
        color = GameData.Instance.ColorData.GetData(type).color;
        mesh.color = color;
        float r = Mathf.Lerp(color.r, Color.white.r, .5f);
        float g = Mathf.Lerp(color.g, Color.white.g, .5f);
        float b = Mathf.Lerp(color.b, Color.white.b, .5f);
        Color col = new Color(r, g, b, 1);
        meshBorder.color = col;
    }
    public override void AfterInit()
    {
        LoadColor(data.color);
        Tf.position = new Vector3(Tf.position.x, Tf.position.y + 10f, Tf.position.z );
        Tf.DOKill();
        //float delay = (data.waterID * .1f) + .2f;
        float delay = (data.waterGroupID * .1f) + .2f;
        Tf.DOMove(centerPos, .5f).SetDelay(delay).SetEase(Ease.OutBack, .4f);
        speed = Mathf.Clamp(GameData.Instance.GetSpeedWaterFill(), 0, GameData.Instance.GetSpeedWaterFill());
    }
    public void RegisterTarget(Transform tf,bool isLast)
    {
        targetEnd = tf;
        meshBorder.sortingOrder = isLast ? 11 : 9;
    }
    public void WaterFill()
    {
        Tf.DOMove(targetEnd.position, speed+.05f).OnComplete(() =>
        {
            // test
            mesh.gameObject.SetActive(false);
            meshBorder.gameObject.SetActive(false);
        });
    }
    public void ChangeSpeedWater()
    {
        speed = Mathf.Clamp(GameData.Instance.GetSpeedWaterFill(), 0, GameData.Instance.GetSpeedWaterFill());
    }
    public void StartHidden()
    {
        mesh.color = GameData.Instance.ColorData.GetHiddenColor();
        meshBorder.color = GameData.Instance.ColorData.GetHiddenColor();
    }
    public void ShowColor()
    {
        mesh.DOKill();
        mesh.DOColor(color,.3f);
        meshBorder.DOKill();
        meshBorder.DOColor(color, .3f);
    }
    public void ShowText()
    {

    }
}