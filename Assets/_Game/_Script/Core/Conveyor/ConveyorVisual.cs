using TMPro;
using UnityEngine;

public class ConveyorVisual : BLBMono
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private TextMeshProUGUI textAmount;
    public void OnInit(int amount, int maxAmount,int size)
    {
        Tf.transform.position = new Vector3((size / 2) - .5f, 0, 0);
        ChangeTextAmount(amount, maxAmount);
    }
    public void ChangeTextAmount(int amount,int maxAmount)
    {
        textAmount.text = amount.ToString() + "/" + maxAmount;
    }
}
