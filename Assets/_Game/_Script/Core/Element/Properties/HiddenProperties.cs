using UnityEngine;
public class HiddenProperties : IProperties
{
    public void Initilize<T, R>(BaseElement<T, R> data) where T : BaseElementVisual<R>
    {
        if (data is WaterElement water)
        {
            water.StartHidden();
            var value = GameObject.Instantiate(GameData.Instance.PropetiesInfor.GetData(EPropertiesType.HiddenWater).prefab);
            var hidden = value.GetComponent<WaterHiddenPropertiesVisual>();
            hidden.Tf.SetParent(water.Visual.Mesh.transform);
            hidden.Tf.localPosition = Vector3.zero;
            hidden.OnInit(water);
        }
        if(data is CupElement cup)
        {
            cup.StartHidden();
            var value = GameObject.Instantiate(GameData.Instance.PropetiesInfor.GetData(EPropertiesType.HiddenCup).prefab);
            var hidden = value.GetComponent<CupHiddenPropertiesVisual>();
            hidden.Tf.SetParent(cup.Visual.Skin.transform);
            hidden.Tf.localPosition = Vector3.zero;
            hidden.OnInit(cup);
        }
    }
}
