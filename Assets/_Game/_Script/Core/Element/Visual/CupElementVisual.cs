using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CupElementVisual : BaseElementVisual<CupData>
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [SerializeField] private Transform parentVfx;
    [SerializeField] private Transform parentWater;
    [SerializeField] private TextMeshProUGUI textAmount;
    //[SerializeField] private MeshRenderer mesh;
    //[SerializeField] private MeshRenderer mesh2;
    //[SerializeField] private MeshRenderer waterMesh;
    [SerializeField] private SpriteRenderer skinBoder;
    [SerializeField] private RectTransform textPosUnclick;
    [SerializeField] private RectTransform textPosClick;
    [SerializeField] private RectTransform textPosConveyor;
    //private MaterialPropertyBlock matBlock;
    //private MaterialPropertyBlock matBlock2;
    //private MaterialPropertyBlock matWater;
    private Transform parent;
    private int amount;
    private int maxAmount;
    private Vector3 currentPos;
    protected EColorType color;
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
        CancelInvoke();
        Invoke(nameof(DelayClear), GameData.Instance.GetTimeActiveFill() + .15f);
    }
    private void DelayClear()
    {
        Tf.DOKill();
        Tf.parent = parent;
        Tf.transform.DOJump(new Vector3(Tf.position.x, Tf.position.y + 3, Tf.position.z), 1, 1, .3f).OnComplete(() =>
        {
            Tf.DOScale(Vector3.zero, .4f).SetEase(Ease.InBack).OnComplete(() =>
            {
                ClearCup();
            });
        });
    }
    private void ClearCup()
    {
        ParticleSystem go = VFXManager.Instance.GetObject(EVfxType.VFX_Explode);
        go.transform.position = Tf.position;
    }
    private void LoadColor(EColorType type)
    {

        //if (matBlock == null)
        //{
        //    matBlock = new MaterialPropertyBlock();
        //}
        //if (matBlock2 == null)
        //{
        //    matBlock2 = new MaterialPropertyBlock();
        //}
        //if (matWater == null)
        //{
        //    matWater = new MaterialPropertyBlock();
        //}
        //matBlock.Clear();
        //matBlock.SetColor("_BaseColor", c);
        //mesh.SetPropertyBlock(matBlock, 0);
        //matBlock.Clear();
        //var colorR = Mathf.Lerp(Color.white.r, c.r, .4f);
        //var colorG = Mathf.Lerp(Color.white.g, c.g, .4f);
        //var colorB = Mathf.Lerp(Color.white.b, c.b, .4f);
        //Color color = c;
        //color.a = .4f;
        //matBlock.SetColor("_BaseColor", color);
        //mesh.SetPropertyBlock(matBlock, 1);

        //mesh2.GetPropertyBlock(matBlock2, 0);
        //matBlock2.SetColor("_BaseColor", c);
        //mesh2.SetPropertyBlock(matBlock2, 0);

        //waterMesh.GetPropertyBlock(matWater);
        //matWater.SetColor("_BaseColor", c);
        //waterMesh.SetPropertyBlock(matWater);
        Color c = GameData.Instance.ColorData.GetData(type).color;
        skinBoder.color = c;
        parentWater.transform.localScale = new Vector3(.8f, 0, .8f);

        // use shader 
        //waterMesh.GetPropertyBlock(matWater);
        //matWater.SetColor("Color_E9C3FC1D", c); // top color 

        //matWater.SetColor("Color_1B2A4228", c*.8f); // bottom color 
        //matWater.SetColor("Color_12DEDFED", c);// foam color
        //matWater.SetFloat("Vector1_86B367DE", 2f); // fill 
        //waterMesh.SetPropertyBlock(matWater);
    }
    private void ChangeTextAmount(int text)
    {
        textAmount.SetText(text.ToString());
        if (text <= 0)
        {
            textAmount.transform.DOKill();
            textAmount.transform.DOScale(Vector3.zero, .3f).SetEase(Ease.InBack);
        }
    }
    private void ChangeTextPosition(RectTransform tf, bool isAnim = false)
    {
        if (isAnim)
        {
            textAmount.transform.DOKill();
            textAmount.transform.DOLocalMove(tf.localPosition, .3f);
            textAmount.transform.DOLocalRotate(tf.localEulerAngles, .3f);
        }
        else
        {
            textAmount.rectTransform.localPosition = tf.localPosition;
            textAmount.rectTransform.localEulerAngles = tf.localEulerAngles;
        }
    }
    private void ScaleText(bool isScale)
    {
        textAmount.DOKill();
        if (isScale)
        {
            textAmount.transform.DOScale(Vector3.one, .4f);
        }
        else
        {
            textAmount.transform.DOScale(Vector3.zero, .4f);
        }
    }
    private void ActiveTextAmount(bool isBusy)
    {
        if (isBusy)
        {
            textAmount.color = new Color(1, 1, 1, .3f);
            Tf.DORotate(new Vector3(-240f, 0, 0), .3f).OnComplete(() =>
            {
                currentPos = Tf.transform.localEulerAngles;
            });
            ChangeTextPosition(textPosUnclick);
            //skin.transform.localEulerAngles =new Vector3(-240f, 0, 0), .3f) ;
        }
        else
        {
            textAmount.color = new Color(1, 1, 1, 1f);
            Tf.DORotate(new Vector3(0, 0, 0), .3f).OnComplete(() =>
            {
                currentPos = Tf.transform.localEulerAngles;
            }); ;
            ChangeTextPosition(textPosClick);
            //skin.transform.localEulerAngles = new Vector3(-50f, 0, 0);
        }
    }
    private void ActiveInteract(bool isBusy)
    {
        ActiveTextAmount(isBusy);
        //if (isBusy) return;
        //float delay = (float)data.id * .05f;
        ////skin.DOKill();
        //Vector3 scaleInit = new Vector3(.98f, .95f, 1f);
        //Vector3 scaleAfter = new Vector3(1.03f, 1.05f, 1f);
        //skin.transform.localScale = scaleInit;
        //skin.DOScaleX(scaleAfter.x, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(delay);
        //skin.DOScaleY(scaleAfter.y, 1.15f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(delay + .2f);
    }
    private void CupShake()
    {
        Tf.DOKill();
        Tf.localRotation = Quaternion.Euler(currentPos);

        float t = GameData.Instance.GetSpeedWaterFill();
        float delay = t * .3f;

        Tf.DOShakeRotation(
            0.2f,
            10f,
            20,
            90f,
            true
        )
        .SetDelay(delay)
        .SetLoops(Mathf.CeilToInt((t - delay) / 0.2f));
    }
    //IEnumerator FillWater(float target)
    //{
    //    waterMesh.GetPropertyBlock(matWater);
    //    float current = matWater.GetFloat("Vector1_86B367DE");

    //    float t = 0;
    //    while (t < 1)
    //    {
    //        t += Time.deltaTime * 3f;
    //        float value = Mathf.Lerp(current, target, t);
    //        matWater.SetFloat("Vector1_86B367DE", value);
    //        waterMesh.SetPropertyBlock(matWater);
    //        yield return null;
    //    }
    //}
    private void FillWater()
    {
        EventDispatcher.Dispatch(new FillPauseGameEvent() { });

        float v = (float)(maxAmount - amount) / (float)maxAmount;
        Vector3 size = Vector3.one * v;
        size.x = 1;
        size.z = 1;
        parentWater.DOKill();
        parentWater.DOScale(size, GameData.Instance.GetSpeedWaterFill()).OnComplete(() =>
        {
            EventDispatcher.Dispatch(new FillContinueGame() { });
        });

    }
    public override void AfterInit()
    {
        LoadColor(data.color);
        amount = data.amount;
        maxAmount = data.amount;
        ChangeTextAmount(amount);
        Tf.DOKill();
        Tf.position = new Vector3(Tf.position.x, Tf.position.y - 10, Tf.position.z);
        float delay = (float)data.id * .05f;
        Tf.DOMove(centerPos, .5f).SetEase(Ease.OutBack, .4f).SetDelay(delay);
        parent = Tf.parent;
        elementCollider.enabled = true;
        currentPos = Tf.transform.localEulerAngles;
        color = data.color;
    }
    public override void SetBusy(bool isBusy)
    {
        base.SetBusy(isBusy);
        ActiveInteract(isBusy);
    }
    public void MoveNextMatrix(Vector3 pos)
    {
        Tf.DOKill();
        Tf.DOMove(pos, .5f).SetEase(Ease.InOutBack);
    }
    public void OutMatrix() // test
    {
        //Tf.DOKill();
        //Vector3 pos = new Vector3(Tf.position.x, Tf.position.y, Tf.position.z + 5f);
        //Tf.DOMove(pos, 1f);
    }
    public void MoveToConveyor(Vector3 pos, Action callBack)
    {
        elementCollider.enabled = false;

        Tf.DOKill();
        Tf.localScale = Vector3.one;
        float timeMove = 1.5f;
        var vfx = VFXManager.Instance.GetObject(EVfxType.VFX_BubleSpark).GetComponent<BubleSparkVfx>();
        vfx.OnInit(timeMove, Tf, GameData.Instance.ColorData.GetData(color).color);
        float startY = Tf.localPosition.y;
        ChangeTextPosition(textPosConveyor, true);

        Sequence seq = DOTween.Sequence();

        seq.Append(
            Tf.DOScale(new Vector3(1.1f, 0.85f, 1.1f), 0.08f)
              .SetEase(Ease.OutQuad)
        );

        //seq.Join(
        //    Tf.DOLocalMoveY(startY - 0.15f, 0.08f)
        //      .SetEase(Ease.InQuad)
        //);

        seq.Append(
            Tf.DOScale(new Vector3(0.9f, 1.15f, 0.9f), 0.1f)
              .SetEase(Ease.OutQuad)
        );

        seq.Join(
            Tf.DOLocalMoveY(startY, 0.1f)
              .SetEase(Ease.OutQuad)
        );
        seq.Append(
            Tf.DOLocalJump(new Vector3(0, timeMove, -.5f), 2.5f, 1, 0.45f)
              .SetEase(Ease.OutCubic)
        );
        seq.Join(
    Tf.DOLocalRotate(new Vector3(60, 0, 0), .3f));

        seq.Insert(seq.Duration() - 0.12f,
            Tf.DOScale(new Vector3(1.15f, 0.85f, 1.15f), 0.1f)
              .SetEase(Ease.InQuad)
        );

        seq.Append(
            Tf.DOScale(Vector3.one, 0.15f)
              .SetEase(Ease.OutBack)
        );

        seq.OnComplete(() =>
        {
            currentPos = Tf.localEulerAngles;
            callBack?.Invoke();
        });
    }
    public void MoveFailed()
    {
        Tf.DOKill();
        Tf.localRotation = Quaternion.Euler(currentPos);

        Tf.DOShakeRotation(
            0.25f,
            14f,
            20,
            90f,
            true
        );
    }
    public void WaterFill()
    {
        amount--;
        ChangeTextAmount(amount);

        WaterBolling go = VFXManager.Instance.GetObject(EVfxType.VFX_WaterBolling).GetComponent<WaterBolling>();
        if (go == null) return;
        Color c = GameData.Instance.ColorData.GetData(color).color;
        go.OnInit(c);
        go.transform.SetParent(Tf);
        go.transform.position = parentVfx.position;

        float t = (float)(maxAmount - amount) / maxAmount;

        float shaderValue = Mathf.Lerp(2f, -1f, t);
        FillWater();
        //StopAllCoroutines();
        //StartCoroutine(FillWater(shaderValue));
        CupShake();

        float lerpY = Mathf.Lerp(startPoint.localPosition.y, endPoint.localPosition.y, t);
        float t2 = (float)(maxAmount - (amount + 1)) / maxAmount;

        float lerpY2 = Mathf.Lerp(startPoint.localPosition.y, endPoint.localPosition.y, t2);


        var vfx = VFXManager.Instance.GetObject(EVfxType.VFX_BubleSpin).GetComponent<BubleSpin>();
        vfx.OnInit(new Vector3(0, lerpY, 0), new Vector3(0, lerpY2, 0), Tf, c, amount);
    }
    public void Toggle(EColorType type, int amount)
    {
        maxAmount = amount;
        this.amount = maxAmount;
        LoadColor(type);
        this.color = type;
        ChangeTextAmount(maxAmount);
    }
    public void StartHidden()
    {
        LoadColor(EColorType.Black);
        ScaleText(false);
    }
    public void StopHidden()
    {
        LoadColor(color);
        ScaleText(true);
        Debug.LogError("aff2");
    }
}
