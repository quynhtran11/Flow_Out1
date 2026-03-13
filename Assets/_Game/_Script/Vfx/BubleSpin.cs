using DG.Tweening;
using UnityEngine;

public class BubleSpin : BLBMono
{
    [SerializeField] private ParticleSystem fx;
    [SerializeField] private EVfxType type;
    public void OnInit(Vector3 pos,Vector3 pos2,Transform parent, Color c,int amount)
    {
        // call cupelementvisual  WaterFill
        //Tf.SetParent(parent);
        //Tf.transform.localPosition = pos2;
        //Tf.DOLocalMove(pos, GameData.Instance.GetTimeActiveFill());
        //var vfx = fx.main;
        //float r = Mathf.Lerp(c.r, Color.white.r, .5f);
        //float g = Mathf.Lerp(c.g, Color.white.g, .5f);
        //float b = Mathf.Lerp(c.b, Color.white.b, .5f);
        //Color color = new Color(r, g, b, 1);
        //vfx.startColor = color;
        ////float t = amount > 0 ? .5f : 0.1f;
        //Invoke(nameof(ReturnObject),GameData.Instance.GetTimeActiveFill()/2f);
    }
    private void ReturnObject()
    {
        var em = fx.emission;
        em.enabled = false;
        Invoke(nameof(Return), 1f);
    }

    private void Return()
    {
        VFXManager.Instance.ReturnObject(type, fx);
    }
}
