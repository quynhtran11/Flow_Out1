using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Singleton<HUD>
{

    [SerializeField] private List<UIBase> allUIs = new List<UIBase>();
    private UIBase currentUI;
    private Stack<UIBase> queueUIs = new Stack<UIBase>();
    protected override bool dondestroy => false;
    public T Get<T>() where T : UIBase
    {
        for (int i = 0; i < allUIs.Count; i++)
        {
            if (allUIs[i].GetType() == typeof(T))
            {
                return (T)allUIs[i];
            }
        }
        return null;
    }
    public void Open<T>(bool overrideCurrent = false) where T : UIBase // call when open ui 
    {

        if (overrideCurrent)
        {
            if (currentUI == null)
            {
                ActiveUI<T>();
            }
            else
            {
                DeActiveUI();
                ActiveUI<T>();
            }
        }
        else
        {
            ActiveUI<T>();
        }
    }
    public void CloseCurrent()
    {
        if (currentUI == null) return;
        currentUI.Close();
    }
    public void Close<T>(bool overrideCurrent = false) where T : UIBase // call when exit ui 
    {
        if (overrideCurrent)
        {

            if (queueUIs == null || queueUIs.Count <= 0)
            {
                if (currentUI == null) return;
                currentUI.Close();
                currentUI = null;
            }
            else
            {
                UIBase uiNew = queueUIs.Pop();
                uiNew.gameObject.SetActive(true);
                uiNew.Open(.25f);
                currentUI = uiNew;


                for (int i = 0; i < allUIs.Count; i++)
                {
                    if (allUIs[i].GetType() == typeof(T))
                    {
                        allUIs[i].Close();
                    }
                }
            }
        }
        else
        {
            if (currentUI == null) return;
            currentUI.Close();
        }
    }
    public void CloseUI<T>() where T : UIBase
    {
        var ui = Get<T>();
        if (ui == null) return;
        ui.Close();
    }
    public void OpenUI<T>() where T : UIBase
    {
        var ui = Get<T>();
        if (ui == null) return;
        ui.gameObject.SetActive(true);
        ui.Open();
    }
    private void ActiveUI<T>()
    {
        for (int i = 0; i < allUIs.Count; i++)
        {
            if (allUIs[i].GetType() == typeof(T))
            {
                allUIs[i].gameObject.SetActive(true);
                allUIs[i].Open();
                currentUI = allUIs[i];
            }
        }
    }
    private void DeActiveUI()
    {
        for (int i = 0; i < allUIs.Count; i++)
        {
            if (allUIs[i] == currentUI)
            {
                allUIs[i].Close();
                if (!queueUIs.Contains(allUIs[i]))
                {
                    queueUIs.Push(allUIs[i]);
                }
            }
        }
    }
}
