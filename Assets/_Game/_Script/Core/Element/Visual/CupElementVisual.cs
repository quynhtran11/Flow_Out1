using DG.Tweening;
using TMPro;
using UnityEngine;

public class CupElementVisual : BaseElementVisual<CupData>
{
    [SerializeField] private TextMeshProUGUI textAmount;
    [SerializeField] private MeshRenderer mesh;
    private MaterialPropertyBlock matBlock;
    private void LoadColor(EColorType type)
    {
        if(matBlock == null)
        {
            matBlock = new MaterialPropertyBlock();
        }
        mesh.GetPropertyBlock(matBlock);
        Color c = GameData.Instance.ColorData.GetData(type).color;
        matBlock.SetColor("_BaseColor", c);
        mesh.SetPropertyBlock(matBlock);
    }
    private void ActiveTextAmount(bool isBusy)
    {
        if (isBusy)
        {
            textAmount.color = new Color(1, 1, 1, .3f);
        }
        else
        {
            textAmount.color = new Color(1, 1, 1, 1f);
        }
    }
    private void ActiveInteract(bool isBusy)
    {
        ActiveTextAmount(isBusy);
        if (isBusy) return;
        float delay = (float)data.id * .05f;
        skin.DOKill();
        Vector3 scaleInit = new Vector3(.975f, .95f, 1f);
        Vector3 scaleAfter = new Vector3(1.025f, 1.05f, 1f);
        skin.transform.localScale = scaleInit;
        skin.DOScaleX(scaleAfter.x, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(delay);
        skin.DOScaleY(scaleAfter.y, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(delay);
    }
    public override void AfterInit()
    {
        LoadColor(data.color);
        Tf.DOKill();
        Tf.position = new Vector3(Tf.position.x, Tf.position.y, Tf.position.z - 10);
        float delay = (float)data.id * .05f;
        Tf.DOMove(centerPos, .5f).SetEase(Ease.OutBack, .4f).SetDelay(delay);
    }
    public override void SetBusy(bool isBusy)
    {
        base.SetBusy(isBusy);
        ActiveInteract(isBusy);
    }
    public void MoveNextMatrix(Vector3 pos)
    {
        Tf.DOKill();
        Tf.DOMove(pos, .5f);
    }
    public void OutMatrix() // test
    {
        //Tf.DOKill();
        //Vector3 pos = new Vector3(Tf.position.x, Tf.position.y, Tf.position.z + 5f);
        //Tf.DOMove(pos, 1f);
    }
}
