using UnityEngine;

public class BubleSparkVfx :BLBMono
{
    [SerializeField] protected EVfxType type;
    [SerializeField] protected ParticleSystem vfx;

    public void OnInit(float time,Transform parent,Color c)
    {
        Tf.SetParent(parent);
        Tf.localPosition = Vector3.zero;
        var main = vfx.main;
        main.startColor = c;
        Invoke(nameof(DeActive), time);
    }
    protected void DeActive()
    {
        var main = vfx.emission;
        main.enabled = false;
    }
    protected virtual void ReturnObj()
    {
        var main = vfx.emission;
        main.enabled = true;
        VFXManager.Instance.ReturnObject(type, vfx);
    }

}
