using TMPro;
using UnityEngine;

public class ConveyorVisual : BLBMono
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private TextMeshProUGUI textAmount;
    public void OnInit(int amount, int maxAmount,int size)
    {
        ChangeTextAmount(amount, maxAmount);
    }
    public void ChangeTextAmount(int amount,int maxAmount)
    {
        textAmount.text = amount.ToString() + "/" + maxAmount;
    }
    public void AddSlot(int amount, int maxAmount)
    {
        ChangeTextAmount(amount, maxAmount);
    }
}
