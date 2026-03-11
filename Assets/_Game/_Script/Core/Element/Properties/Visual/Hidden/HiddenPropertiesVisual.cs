using DG.Tweening;
using TMPro;
using UnityEngine;

public class HiddenPropertiesVisual<T> : BasePropertiesVisual<T>
{
    [SerializeField] private TextMeshProUGUI text;

    public override void OnExit()
    {
        base.OnExit();
        text.transform.DOKill();
        text.transform.DOScale(Vector3.zero, .3f).SetEase(Ease.InBack);
    }
}
