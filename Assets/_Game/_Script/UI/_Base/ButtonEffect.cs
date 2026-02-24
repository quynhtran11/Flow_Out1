using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class ButtonEffect : Button
{
    [SerializeField] private float scaleAmount = 1.1f; 
    [SerializeField] private float duration = 0.25f;   
    [SerializeField] private Ease easeType = Ease.OutBack;
    private Vector3 originalScale;
    private RectTransform rectTransform;

    protected override void Start()
    {
        base.Start();
        if(rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        originalScale = rectTransform.localScale;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable) return;
        base.OnPointerDown(eventData);
        rectTransform.DOKill();
        rectTransform.DOScale(originalScale * scaleAmount, duration).SetEase(easeType);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable) return;
        base.OnPointerUp(eventData);
        rectTransform.DOKill();
        rectTransform.DOScale(originalScale, duration).SetEase(easeType);
        SFX.Instance.PlaySound(ESoundKey.Click);
    }
}
