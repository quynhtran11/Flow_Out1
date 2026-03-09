using UnityEngine;

public class WaterBolling : BaseVFX
{
    [SerializeField] private ParticleSystem jumpingFx;
    public void OnInit(Color c)
    {
        var v = vfx.emission;
        v.enabled = false;
        var vfxJump = jumpingFx.main;
        vfxJump.startColor = c;
    }
}
