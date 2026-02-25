using DG.Tweening;
using UnityEngine;
public abstract class UIBase : BLBMono
{
    [Header("ANIMATION")]
    [SerializeField] protected float duration = .25f;
    [SerializeField] protected float value = .75f;
    [SerializeField] protected Ease ease = Ease.OutBack;
    [Header("OBJECT")]
    [SerializeField] protected RectTransform bg;
    [SerializeField] protected RectTransform mainFrame;
    [SerializeField] protected CanvasGroup canvasGroup;

    private void Start()
    {
        Register();
    }
    protected virtual void Register(){}
    public virtual void Open(float delay = 0f)
    {
        if (mainFrame == null)
        {
            Debug.LogError($"mainFrame in {this} is null");
            return;
        }
        if (bg != null)
        {
            bg.gameObject.SetActive(true);
        }
        mainFrame.transform.localScale = Vector3.zero *value;
        mainFrame.transform.DOKill();
        mainFrame.transform.DOScale(Vector3.one, duration).SetEase(ease).SetDelay(delay);
        if (canvasGroup == null) return;
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, duration);
    }
    public virtual void Close(float delay = .3f)
    {
        if (mainFrame == null)
        {
            Debug.LogError($"mainFrame in {this} is null");
            return;
        }
        if (bg != null)
        {
            bg.gameObject.SetActive(false);
        }
        mainFrame.transform.DOScale(Vector3.zero *value, duration).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
        if (canvasGroup == null) return;
        canvasGroup.DOFade(0, duration);
    }
}
