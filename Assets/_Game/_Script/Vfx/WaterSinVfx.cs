using System.Collections;
using UnityEngine;

public class WaterSinVfx : MonoBehaviour
{
    [SerializeField] private SpriteRenderer skin;

    [SerializeField] private Sprite[] waterSins;
    private bool isLoop = false;
    public void OnInit(Color c)
    {
        isLoop = true;
        skin.color = c * .8f;
        StopAllCoroutines();
        StartCoroutine(WaterLoop());
    }
    IEnumerator WaterLoop()
    {
        yield return null;
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
