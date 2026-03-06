using System.Collections;
using UnityEngine;

public class WaterFill : BaseVFX
{
    [SerializeField] protected float speedOffset = 2.5f;
    [SerializeField] protected LineRenderer line;
    [SerializeField] protected SpriteRenderer icon;
    private float maxTime;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<ChangeSceneEvent>(OnChangeScene);
        EventDispatcher.RegisterEvent<IncreaseSpeedWaterEvent>(OnIncreaseSpeedWater);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<ChangeSceneEvent>(OnChangeScene);
        EventDispatcher.RemoveEvent<IncreaseSpeedWaterEvent>(OnIncreaseSpeedWater);
    }
    private void OnIncreaseSpeedWater(IncreaseSpeedWaterEvent param)
    {
        maxTime = GameData.Instance.GetTimeActiveFill();
    }
    private void OnChangeScene(ChangeSceneEvent param)
    {
        VFXManager.Instance.ReturnObject(EVfxType.VFX_WaterFill, vfx);
    }
    public void OnInit(Transform tf, Transform tf2, float t, Color c)
    {
        line.positionCount = 2;

        Vector3 start = tf.position;
        Vector3 end = tf2.position;

        line.SetPosition(0, start);
        line.SetPosition(1, start);

        maxTime = t;

        line.startColor = c;
        line.endColor = c;

        StopAllCoroutines();
        StartCoroutine(PlayLine(start, end));
    }
    IEnumerator PlayLine(Vector3 startPos, Vector3 endPos)
    {
        float extendTime = 0.1f;  
        float retractTime = 0.125f;  
        float activeTime = maxTime;

        float t = 0;

        while (t < extendTime)
        {
            t += Time.deltaTime;
            float percent = Mathf.Clamp01(t / extendTime);
            percent = Mathf.SmoothStep(0, 1, percent);

            Vector3 newEnd = Vector3.Lerp(startPos, endPos, percent);
            line.SetPosition(1, newEnd);

            yield return null;
        }

        line.SetPosition(1, endPos);
        yield return new WaitForSeconds(activeTime - extendTime - retractTime);

        float rt = 0;
        while (rt < retractTime)
        {
            rt += Time.deltaTime;
            float percent = Mathf.Clamp01(rt / retractTime);
            percent = Mathf.SmoothStep(0, 1, percent);

            Vector3 newStart = Vector3.Lerp(startPos, endPos, percent);
            line.SetPosition(0, newStart);

            yield return null;
        }

        line.SetPosition(0, endPos);

        VFXManager.Instance.ReturnObject(EVfxType.VFX_WaterFill, vfx);
    }
    private void Update()
    {
        line.material.SetTextureOffset("_MainTex", new Vector2(-speedOffset * Time.time, 0));
    }
   // IEnumerator DeActive()
    //{
    //    float retractTime = 0.2f; 
    //    float t = 0;
    //    while (t < maxTime - retractTime)
    //    {
    //        t += Time.deltaTime;
    //        yield return null;
    //    }

    //    float rt = 0;

    //    Vector3 startPos = line.GetPosition(0);
    //    Vector3 endPos = line.GetPosition(1);

    //    while (rt < retractTime)
    //    {
    //        rt += Time.deltaTime;
    //        float percent = rt / retractTime;

    //        percent = Mathf.SmoothStep(0, 1, percent);

    //        Vector3 newPos = Vector3.Lerp(startPos, endPos, percent);
    //        line.SetPosition(0, newPos);

    //        yield return null;
    //    }

    //    line.SetPosition(0, endPos);

    //    VFXManager.Instance.ReturnObject(EVfxType.VFX_WaterFill, vfx);
    //}
}
