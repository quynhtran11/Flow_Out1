using DG.Tweening;
using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class WaterSinVfx : MonoBehaviour
{
    [SerializeField] private SpriteRenderer skin;
    [SerializeField] private SkeletonAnimation skeleton;
    [SerializeField] private MeshRenderer skeletonMesh;

    [SerializeField] private Sprite[] waterSins;
    [SerializeField] private Vector3 sizeEnd;
    [SerializeField] private Vector3 sizeStart;
    [SerializeField] private float posY;

    private bool isLoop = false;
    private int index = 0;
    [SerializeField] private Vector3[] allSize;
    [SerializeField] private Vector3[] allPos;
    public void OnInit(Color c,float t,int amount)
    {
        isLoop = true;
        Color r = Color.Lerp(Color.black, c, .75f);
        Color color = r;
        color.a = 1;
        skin.color = c;
        skin.transform.DOKill();
        skeleton.transform.DOKill();
        skeleton.skeleton.SetColor(color);
        skeleton.enabled = false;
        skeletonMesh.enabled = false;
            index = (amount-1);
        skeletonMesh.transform.DOLocalMove(allPos[index], GameData.Instance.GetSpeedWaterFill());
        skeletonMesh.transform.DOScale(allSize[index], GameData.Instance.GetSpeedWaterFill());
        StopAllCoroutines();
        StartCoroutine(WaterLoop());
    }
    IEnumerator WaterLoop()
    {
        float timeDelay = GameData.Instance.GetSpeedWaterFill() * .4f;
        yield return null;
        yield return new WaitForSeconds(timeDelay);
        skeleton.enabled = true;
        skeletonMesh.enabled = true;

        //skin.enabled = true;
        //float t = 0;
        //float maxTime = 0.1f;
        //int index = 0;
        //skin.sprite = waterSins[index];
        //while (isLoop)
        //{
        //    t += Time.deltaTime;
        //    if (t > maxTime)
        //    {
        //        index++;
        //        t = 0;
        //        if (index >= waterSins.Length)
        //        {
        //            index = 0;
        //        }
        //        skin.sprite = waterSins[index];
        //    }
        //    yield return null;
        //}
    }
}
