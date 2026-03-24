using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CupElementVisual : BaseElementVisual<CupData>
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform open;
    [SerializeField] private Transform close;

    [SerializeField] private Transform parentVfx;
    [SerializeField] private Transform parentWater;
    [SerializeField] private TextMeshProUGUI textAmount;
    [SerializeField] private WaterSinVfx waterSin;
    [SerializeField] private SpriteRenderer skinBoder;
    [SerializeField] private SpriteRenderer water;
    [SerializeField] private SpriteRenderer skinBoderClose;
    [SerializeField] private SpriteRenderer[] subSkins;
    [SerializeField] private Transform mask;
    [SerializeField] private Transform skinAll;
    [SerializeField] private RectTransform textPosUnclick;
    [SerializeField] private RectTransform textPosClick;
    [SerializeField] private RectTransform textPosConveyor;
    [SerializeField] protected CupSkinVisual skinVisualAll;
    private Transform parent;
    private ParticleSystem starVFX;
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
            CancelInvoke();
            float t = .4f;
            Invoke(nameof(ClearCup), t-.05f);
            Tf.DOScale(Vector3.zero, t).SetEase(Ease.InBack).OnComplete(() =>
            {
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
        Color c = GameData.Instance.ColorData.GetData(type).color;
        skinBoder.color = c;
        parentWater.transform.localScale = new Vector3(.8f, 0, .8f);
        water.color = c;
        skinBoderClose.color = c;

        float r = Mathf.Lerp(c.r, Color.white.r, 1f);
        float g = Mathf.Lerp(c.g, Color.white.g, 1f);
        float b = Mathf.Lerp(c.b, Color.white.b, 1f);
        Color col = new Color(r, g, b, (200f/255f));
        if (color == EColorType.Black) return;
        for (int i = 0; i < subSkins.Length; i++)
        {
            subSkins[i].color = col;
        }
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
            Tf.DORotate(new Vector3(0, 0, 0), .3f).OnComplete(() =>
            {
                currentPos = Tf.transform.localEulerAngles;
            });
            ChangeTextPosition(textPosUnclick);
            open.gameObject.SetActive(false);
            close.gameObject.SetActive(true);
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
            open.gameObject.SetActive(true);
            close.gameObject.SetActive(false);
            starVFX = VFXManager.Instance.GetObject(EVfxType.VFX_Star);
            starVFX.gameObject.transform.SetParent(Tf);
            starVFX.gameObject.transform.localPosition = new Vector2(0f, 1f);
            //skin.transform.localEulerAngles = new Vector3(-50f, 0, 0);
        }
        textAmount.transform.localScale = Vector3.one;
    }
    private void ActiveInteract(bool isBusy)
    {
        ActiveTextAmount(isBusy);
    }
    private void CupShake()
    {
        Tf.DOKill();
        Tf.localRotation = Quaternion.Euler(currentPos);

        float t = GameData.Instance.GetSpeedWaterFill();
        t += t * .3f;
        float delay = t * .3f;

        Tf.DOShakeRotation(
            0.3f,
            8f,
            15,
            20f,
            true
        )
        .SetDelay(delay)
        .SetLoops(Mathf.CeilToInt((t - delay) / 0.3f));
    }
    IEnumerator FillWater(float target)
    {
        yield return null;
        mask.transform.DOKill();
        float t = GameData.Instance.GetSpeedWaterFill();
        Vector3 pos = new Vector3(0, target, 0);
        mask.transform.DOLocalMove(pos, t+.1f);
    }
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
        waterSin.gameObject.SetActive(false);
        mask.transform.position = startPoint.transform.position;
        mask.gameObject.SetActive(false);
    }
    public override void SetBusy(bool isBusy)
    {
        base.SetBusy(isBusy);
        ActiveInteract(isBusy);
    }
    public void MoveNextMatrix(Vector3 pos,Vector2Int map,float t)
    {
        Tf.DOKill();
        Tf.DOMove(pos, .5f).SetEase(Ease.InOutBack).SetDelay(t).OnComplete(() =>
        {
            EventDispatcher.Dispatch(new CupPlaceSlotEvent()
            {
                colorType = color,
                map = map,
                timeDelay = t
            });
        });
    }
    public void OutMatrix() // test
    {
        if(starVFX == null) return; 
        StarVFX star = starVFX.GetComponent<StarVFX>();
        if (star == null) return;
        star.DisableVFX();
    }
    public void MoveToConveyor(Vector3 pos, Action callBack)
    {
        elementCollider.enabled = false;

        Tf.DOKill();
        Tf.localScale = Vector3.one;
        skinAll.DOKill();
        skinAll.transform.localScale = Vector3.one; 
        float timeMove = 1.5f;
        skinAll.DOScale(Vector3.one * 1.1f, timeMove).SetEase(Ease.OutBack);
        var vfx = VFXManager.Instance.GetObject(EVfxType.VFX_BubleSpark).GetComponent<BubleSparkVfx>();
        vfx.OnInit(timeMove, Tf, GameData.Instance.ColorData.GetData(color).color);
        float startY = Tf.localPosition.y;
        ChangeTextPosition(textPosConveyor, true);

        Sequence seq = DOTween.Sequence();

        seq.Append(Tf.DOScale(new Vector3(1.035f, 0.93f, 1.035f), 0.08f).SetEase(Ease.OutQuad));
        seq.Append(Tf.DOScale(new Vector3(0.935f, 1.02f, 0.935f), 0.1f).SetEase(Ease.OutQuad));
        seq.Join(Tf.DOLocalMoveY(startY, 0.1f).SetEase(Ease.OutQuad));
        seq.Append(Tf.DOLocalJump(new Vector3(0, timeMove, -.5f), 2.5f, 1, 0.45f).SetEase(Ease.OutCubic));
        seq.Join(Tf.DOLocalRotate(new Vector3(60, 0, 0), .3f));
        seq.Insert(seq.Duration() - 0.12f,Tf.DOScale(new Vector3(1.035f, 0.92f, 1.035f), 0.1f).SetEase(Ease.InQuad));

        seq.Append(Tf.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutBack));

        seq.OnComplete(() =>
        {
            mask.gameObject.SetActive(true);
            currentPos = Tf.localEulerAngles;
            callBack?.Invoke();
        });
    }
    public void MoveFailed()
    {
        Tf.DOKill();
        Tf.localRotation = Quaternion.Euler(currentPos);

        Tf.DOShakeRotation(0.25f,14f,20,90f,true);
    }
    public void WaterFill()
    {
        amount--;
        ChangeTextAmount(amount);

        WaterBolling go = VFXManager.Instance.GetObject(EVfxType.VFX_WaterBolling).GetComponent<WaterBolling>();
        if (go == null) return;
        float t = (float)(maxAmount - amount) / maxAmount;
        Color c = GameData.Instance.ColorData.GetData(color).color;
        go.OnInit(c);
        go.transform.SetParent(Tf);
        go.transform.position = parentVfx.position;
        go.SetPos(parentVfx.position, (maxAmount - amount));

        float shaderValue = Mathf.Lerp(startPoint.transform.localPosition.y, endPoint.transform.localPosition.y, t);
        FillWater();
        StopAllCoroutines();
        StartCoroutine(FillWater(shaderValue));
        CupShake();

        float lerpY = Mathf.Lerp(startPoint.localPosition.y, endPoint.localPosition.y, t);
        float t2 = (float)(maxAmount - (amount + 1)) / maxAmount;

        float lerpY2 = Mathf.Lerp(startPoint.localPosition.y, endPoint.localPosition.y, t2);
        waterSin.gameObject.SetActive(true);
        waterSin.OnInit(c,t, (maxAmount - amount));
        //var vfx = VFXManager.Instance.GetObject(EVfxType.VFX_BubleSpin).GetComponent<BubleSpin>();
        //vfx.OnInit(new Vector3(0, lerpY, 0), new Vector3(0, lerpY2, 0), Tf, c, amount);
    }
    public void PushSorting()
    {
        skinVisualAll.PushSorting();
        if (open == null || close == null) return;
        open.gameObject.SetActive(true);
        close.gameObject.SetActive(false);
    }
    public void PopSorting()
    {
        skinVisualAll.PopSorting();
        if (open == null || close == null) return;
        open.gameObject.SetActive(false);
        close.gameObject.SetActive(true);
    }
    public void Shuffle(EColorType type)
    {
        StartCoroutine(ShuffleEffect(type));
    }
    private IEnumerator ShuffleEffect(EColorType finalType)
    {
        float t = GameData.Instance.TimeShuffle / 0.05f;
        int shuffleCount = (int)t;

        for (int i = 0; i < shuffleCount; i++)
        {
            EColorType rand = (EColorType)UnityEngine.Random.Range(0, (int)EColorType.Black);
            LoadColor(rand);

            yield return new WaitForSeconds(0.05f);
        }
        this.color = finalType;
        LoadColor(finalType);
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
