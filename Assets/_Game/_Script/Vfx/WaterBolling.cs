using System.Collections.Generic;
using UnityEngine;

public class WaterBolling : BaseVFX
{
    [SerializeField] private ParticleSystem jumpingFx;
    [SerializeField] private List<float> allPos = new List<float>();
    public void OnInit(Color c)
    {
        var v = vfx.emission;
        v.enabled = false;
        var vfxJump = jumpingFx.main;
        vfxJump.startColor = c;
    }
    public void SetPos(Vector3 pos, int index)
    {
        if (index > allPos.Count) return;
        Tf.position = pos;
        Tf.localPosition = new Vector3(0,allPos[index-1],pos.z);
    }
}
