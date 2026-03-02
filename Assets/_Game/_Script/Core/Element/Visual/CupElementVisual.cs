using DG.Tweening;
using NUnit.Framework.Interfaces;
using System;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class CupElementVisual : BaseElementVisual<CupData>
{
    [SerializeField] private TextMeshProUGUI textAmount;
    [SerializeField] private MeshRenderer mesh;
    private MaterialPropertyBlock matBlock;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<ClearCupEvent>(OnClearCup);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<ClearCupEvent>(OnClearCup);
    }
    private void OnClearCup(ClearCupEvent param)
    {
        if (param.cup != this) return;
        Tf.DOKill();
        Tf.DOScale(Vector3.zero, .4f).SetEase(Ease.InBack);
    }
    private void LoadColor(EColorType type)
    {
        if(matBlock == null)
        {
            matBlock = new MaterialPropertyBlock();
        }
        mesh.GetPropertyBlock(matBlock,0);
        Color c = GameData.Instance.ColorData.GetData(type).color;
        matBlock.SetColor("_BaseColor", c);
        mesh.SetPropertyBlock(matBlock,0);
    }
    private void ChangeTextAmount(string text)
    {
        textAmount.SetText(text);
    }
    private void ActiveTextAmount(bool isBusy)
    {
        if (isBusy)
        {
            textAmount.color = new Color(1, 1, 1, .3f);
            skin.transform.localEulerAngles = new Vector3(-180f, 0, 0);
        }
        else
        {
            textAmount.color = new Color(1, 1, 1, 1f);
            skin.transform.localEulerAngles = new Vector3(-5f, 0, 0);
        }
    }
    private void ActiveInteract(bool isBusy)
    {
        ActiveTextAmount(isBusy);
        if (isBusy) return;
        float delay = (float)data.id * .05f;
        skin.DOKill();
        Vector3 scaleInit = new Vector3(.98f, .95f, 1f);
        Vector3 scaleAfter = new Vector3(1.03f, 1.05f, 1f);
        skin.transform.localScale = scaleInit;
        skin.DOScaleX(scaleAfter.x, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(delay);
        skin.DOScaleY(scaleAfter.y, 1.15f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(delay + .2f);
    }
    public override void AfterInit()
    {
        LoadColor(data.color);
        ChangeTextAmount(data.amount.ToString());
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
    public void MoveToConveyor(Vector3 pos, Action callBack)
    {
            Tf.DOLocalMove(new Vector3(0, .5f, 0), .3f).OnComplete(() =>
            {
                callBack?.Invoke();
            });
    }
    public void MoveFailed()
    {
        skin.DOKill();
        skin.DOPunchRotation(Vector3.forward * 5.75f, .25f);
    }
}
