using UnityEngine;

public class ConveyorBowElement : BLBMono
{
    public void OnUpdate(Vector2 start, Vector2 end)
    {
        Vector2 calculatorSpeed = Vector2.left * GameData.Instance.GetSpeedConveyor() * Time.deltaTime;
        Debug.LogError("value_" + GameData.Instance.GetSpeedConveyor());
        Tf.Translate(calculatorSpeed);
        if (Tf.position.x >= end.x)
        {
            Tf.position = new Vector3(start.x, Tf.position.y, Tf.position.z);
        }
    }
}
