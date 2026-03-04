using UnityEngine;

public abstract class BaseVFX : BLBMono
{
    [SerializeField] protected float activeTime;
    [SerializeField] protected EVfxType type;
    [SerializeField] protected ParticleSystem vfx;
    private void OnEnable()
    {
        Deactive();
    }
    protected void Deactive()
    {
        Invoke(nameof(DelayDeactive),activeTime);
    }
    protected void DelayDeactive()
    {
        VFXManager.Instance.ReturnObject(type, vfx);
    }
}
