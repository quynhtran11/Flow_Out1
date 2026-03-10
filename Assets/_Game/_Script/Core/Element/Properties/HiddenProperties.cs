using UnityEngine;
public class HiddenProperties : IProperties
{
    public void Initilize<T, R>(BaseElement<T, R> data) where T : BaseElementVisual<R>
    {
        if (data is WaterElement water)
        {
            water.StartHidden();
            var value = GameObject.Instantiate(GameData.Instance.PropetiesInfor.GetData(EPropertiesType.Hidden).prefab);
            var hidden = value.GetComponent<WaterHiddenPropertiesVisual>();
            hidden.Tf.SetParent(water.Visual.Mesh.transform);
            hidden.Tf.localPosition = Vector3.zero;
            hidden.OnInit(water);
        }
    }
}
