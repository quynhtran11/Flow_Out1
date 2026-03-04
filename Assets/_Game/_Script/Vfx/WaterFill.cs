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
        maxTime = GameData.Instance.TimeActiveFillEndGame;
    }
    private void OnChangeScene(ChangeSceneEvent param)
    {
        VFXManager.Instance.ReturnObject(EVfxType.VFX_WaterFill, vfx);
    }
    public void OnInit(Transform tf, Transform tf2,float t,Color c)
    {
        line.positionCount = 0;
        line.positionCount = 2;
        line.SetPosition(0, tf.position);
        line.SetPosition(1, tf2.position);
        maxTime = t;
        StopAllCoroutines();
        StartCoroutine(DeActive());
        line.startColor = c;
        line.endColor = c;
    }
    private void Update()
    {
        line.material.SetTextureOffset("_MainTex", new Vector2(-speedOffset * Time.time, 0));
    }
    IEnumerator DeActive()
    {
        float retractTime = 0.05f; 
        float t = 0;
        while (t < maxTime - retractTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        float rt = 0;

        Vector3 startPos = line.GetPosition(0);
        Vector3 endPos = line.GetPosition(1);

        while (rt < retractTime)
        {
            rt += Time.deltaTime;
            float percent = rt / retractTime;

            percent = Mathf.SmoothStep(0, 1, percent);

            Vector3 newPos = Vector3.Lerp(startPos, endPos, percent);
            line.SetPosition(0, newPos);

            yield return null;
        }

        line.SetPosition(0, endPos);

        VFXManager.Instance.ReturnObject(EVfxType.VFX_WaterFill, vfx);
    }
}
