using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : UIBase
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI textProgress;
    public void Loading(AsyncOperation asyn)
    {
        StopAllCoroutines();
        StartCoroutine(LoadingProgress(asyn));
    }
    public IEnumerator LoadingProgress(AsyncOperation async)
    {
        yield return null;

        mainFrame.transform.localScale = Vector3.one;
        progressBar.value = 0;
        textProgress.text = "0%";

        float time = 0f;
        float minTime = 2f;

        async.allowSceneActivation = false;

        while (true)
        {
            time += Time.deltaTime;

            float loadProgress = async.progress / 0.9f;

            float timeProgress = Mathf.Clamp01(time / minTime);

            float displayProgress = Mathf.Min(loadProgress, timeProgress);

            progressBar.value = displayProgress;
            textProgress.text = $"{(int)(displayProgress * 100f)}%";

            if (time >= minTime && async.progress >= 0.9f)
            {
                break;
            }

            yield return null;
        }
        Close();
        async.allowSceneActivation = true;

    }
}
