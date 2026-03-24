using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class ShuffleBoosterUI : UIBase
{
    [SerializeField] private Image icon;
    public void OnInit()
    {
        float t = GameData.Instance.TimeShuffle / .25f;

        icon.DOKill();
        icon.color = Color.white;
        icon.transform.localScale = Vector3.zero;
        icon.transform.localEulerAngles = new Vector3(0, 0, 180);
        icon.transform.DOScale(Vector3.one, .3f);
        icon.transform.DORotate(new Vector3(0,0,360),.25f).SetEase(Ease.Linear).SetLoops((int)t, LoopType.Incremental).OnComplete(() =>
        {
            icon.transform.DOScale(Vector3.one*2f, .5f).SetDelay(.5f).OnComplete(() =>
            {
                GameHUD.Instance.CloseUI<ShuffleBoosterUI>();
            });
            icon.DOFade(0, .5f).SetDelay(.5f);
        });
    }

}