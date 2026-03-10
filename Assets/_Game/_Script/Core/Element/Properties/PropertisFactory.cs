using System.Collections.Generic;
using UnityEngine;

public static class PropertisFactory 
{
    public static List<IProperties> GetCupProperties(CupData data)
    {
        List<IProperties> allPros = new List<IProperties>();
        if (data.hiddenData.isHidden)
        {
            allPros.Add(new HiddenProperties() { });
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
        if (data.freezeData.amount>0)
        {
            allPros.Add(new FreezeProperties() { });
        }
        return allPros;
    }
}
