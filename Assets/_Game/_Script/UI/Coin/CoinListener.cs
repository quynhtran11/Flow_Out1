using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinListener : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private GameObject coinFlyPrefab;
    [SerializeField] private RectTransform canvasRoot;

    private int currentCoin;

    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<ChangeCoinEvent>(OnChangeCoin);
        currentCoin = PlayerPrefs.GetInt(Constans.KeyCurrentCoin, 0);
        ChangeCoin(currentCoin);
    }

    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<ChangeCoinEvent>(OnChangeCoin);
    }

    private void OnChangeCoin(ChangeCoinEvent param)
    {
        int newCoin = param.coin;
        SFX.Instance.PlaySound(ESoundKey.Coin);
        if (newCoin > currentCoin)
        {
            int add = newCoin - currentCoin;
            SpawnCoinFly(add, param.callBack);
        }

        currentCoin = newCoin;
        ChangeCoin(newCoin, param.oldCoin);
    }

    private Tween coinTween;

    private void ChangeCoin(int coin, int oldCoin = 0)
    {
        coinTween?.Kill();

        if (oldCoin <= 0)
        {
            textCoin.text = OnChangeCoinCurrency(coin);
            return;
        }

        int currentValue = oldCoin;
        textCoin.text = OnChangeCoinCurrency(currentValue);

        coinTween = DOVirtual.DelayedCall(1, () =>
        {
            DOTween.To(() => currentValue, x =>
            {
                currentValue = x;
                textCoin.text = OnChangeCoinCurrency(currentValue);
            },
            coin,
            0.5f
        ).SetEase(Ease.OutQuad);
        });
    }
    private string OnChangeCoinCurrency(int value)
    {
        if (value >= 1000)
        {
            return (Mathf.FloorToInt((float)value / 1000f)).ToString() + "k";
        }
        else
        {
            return value.ToString();
        }
    }

    private void SpawnCoinFly(int count, Action callBack = null)
    {
        count = Mathf.Clamp(count, 1, 10);

        float randomRange = 80f;
        List<Vector2> usedPositions = new List<Vector2>();

        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);

        for (int i = 0; i < count; i++)
        {
            int index = i;
            GameObject c = Instantiate(coinFlyPrefab, canvasRoot);
            RectTransform rect = c.GetComponent<RectTransform>();

            Vector2 spawnPos;
            int loop = 0;

            do
            {
                spawnPos = center + new Vector2(
                    UnityEngine.Random.Range(-randomRange, randomRange),
                    UnityEngine.Random.Range(-randomRange, randomRange)
                );

                loop++;
                if (loop > 20) break;
            }
            while (usedPositions.Contains(spawnPos));

            usedPositions.Add(spawnPos);
            rect.position = spawnPos;

            float delay = index * 0.05f;

            DOVirtual.DelayedCall(delay, () =>
            {
                c.GetComponent<CoinFly>().Play(
                    textCoin.rectTransform,
                    () =>
                    {
                        // sfx
                        transform.DOKill();
                        transform.localScale = Vector3.one;
                        transform.DOPunchScale(Vector3.one * .2f, .25f);
                        if (index == count - 1)
                        {
                            DOVirtual.DelayedCall(1f, () =>
                            {
                                callBack?.Invoke();
                            });
                        }
                    }
                );
            });
        }
    }
}
