using System.Collections.Generic;
using UnityEngine;

public static class PropertisFactory
{
    private static Dictionary<int, ToggleColorProperties> toggleGroups = new();

    public static List<IProperties> GetCupProperties(CupData data, CupElement cupElement)
    {
        List<IProperties> allPros = new List<IProperties>();
        if (data.hiddenData.isHidden)
        {
            allPros.Add(new HiddenProperties() { });
        }
        if (data.toggleColorData.isToggle)
        {
            if (!toggleGroups.TryGetValue(data.toggleColorData.idGroup, out var prop))
            {
                prop = new ToggleColorProperties();
                toggleGroups[data.toggleColorData.idGroup] = prop;
                prop.AddCup(cupElement);
            }
            else
            {
                toggleGroups[data.toggleColorData.idGroup].AddCup(cupElement);
                prop.ConnectCup();
            }
            allPros.Add(prop);
        }
        return allPros;
    }
    public static List<IProperties> GetWaterProperties(WaterData data)
    {
        List<IProperties> allPros = new List<IProperties>();
        if (data.hiddenData.isHidden)
        {
            allPros.Add(new HiddenProperties() { });
        }
        if (data.freezeData.amount > 0)
        {
            allPros.Add(new FreezeProperties() { });
        }
        return allPros;
    }
    public static void OnInit()
    {
        toggleGroups = new Dictionary<int, ToggleColorProperties>();
    }
}
