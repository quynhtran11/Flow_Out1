using System.Collections.Generic;
using UnityEngine;
public class ToggleColorProperties : IProperties
{
    protected List<CupElement> allCups = new List<CupElement>();
    public void Initilize<T, R>(BaseElement<T, R> data) where T : BaseElementVisual<R>
    {
    }
    public void AddCup(CupElement cup)
    {
        allCups.Add(cup);
    }
    public void ConnectCup()
    {
        for (int i = 0; i < allCups.Count; i++)
        {
            var cup = allCups[i];
            var cupOpsite = i == 0 ? allCups[1] : allCups[0];
            var value = GameObject.Instantiate(GameData.Instance.PropetiesInfor.GetData(EPropertiesType.ToggleColor).prefab);
            var toggle = value.GetComponent<CupToggleColorPropertiesVisual>();
            toggle.Tf.SetParent(cup.Tf);
            toggle.Tf.localPosition = Vector3.zero;
            toggle.OnInit(cup);
            toggle.AddOposite(cupOpsite);
        }
    }
}