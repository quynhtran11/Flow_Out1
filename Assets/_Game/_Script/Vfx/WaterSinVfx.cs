using DG.Tweening;
using System.Collections;
using UnityEngine;

public class WaterSinVfx : MonoBehaviour
{
    [SerializeField] private SpriteRenderer skin;

    [SerializeField] private Sprite[] waterSins;
    [SerializeField] private Vector3 sizeEnd;
    [SerializeField] private Vector3 sizeStart;
    [SerializeField] private float posY;
    private bool isLoop = false;
    public void OnInit(Color c,float t)
    {
        isLoop = true;
        skin.color = c;
        skin.enabled = false;
        skin.transform.DOKill();
        float sizeX = Mathf.Lerp(sizeStart.x, sizeEnd.x, t);
        float sizeY = Mathf.Lerp(sizeStart.y, sizeEnd.y, t);
        float sizeZ = Mathf.Lerp(sizeStart.z, sizeEnd.z, t);
        if (t >= 1)
        {
            transform.DOLocalMoveY(posY, GameData.Instance.GetSpeedWaterFill());
            sizeY = 2;
        }
        skin.transform.DOScale(new Vector3(sizeX, sizeY, sizeZ), GameData.Instance.GetSpeedWaterFill());
        StopAllCoroutines();

        StartCoroutine(WaterLoop());
    }
    IEnumerator WaterLoop()
    {
        float timeDelay = GameData.Instance.GetSpeedWaterFill() * .4f;
        yield return null;
        yield return new WaitForSeconds(timeDelay);
        skin.enabled = true;
        float t = 0;
        float maxTime = 0.1f;
        int index = 0;
        skin.sprite = waterSins[index];
        while (isLoop)
        {
            t += Time.deltaTime;
            if (t > maxTime)
            {
                index++;
                t = 0;
                if (index >= waterSins.Length)
                {
                    index = 0;
                }
                skin.sprite = waterSins[index];
            }
            yield return null;
        }
    }
}
