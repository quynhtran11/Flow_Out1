using UnityEngine;

public class FreezeProperties : IProperties
{
    public void Initilize<T, R>(BaseElement<T, R> data) where T : BaseElementVisual<R>
    {
        if (data is WaterElement water)
        {
            water.StartFreeze();
            var value = GameObject.Instantiate(GameData.Instance.PropetiesInfor.GetData(EPropertiesType.Freeze).prefab);
            var hidden = value.GetComponent<WaterFreezePropertiesVisual>();
            hidden.Tf.SetParent(water.Visual.Mesh.transform);
            hidden.Tf.localPosition = Vector3.zero;
            hidden.OnInit(water);
        }
    }
}