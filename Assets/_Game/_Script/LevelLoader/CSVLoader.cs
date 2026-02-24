using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class CSVLoader
{
    public static async Task<LevelVarient> LoadLevelVariantAsync(string resourceName)
    {
        await Task.Yield();

        TextAsset csv = Resources.Load<TextAsset>(resourceName);
        if (csv == null)
            throw new Exception($"CSV not found: {resourceName}");

        return Parse(csv.text);
    }

    private static LevelVarient Parse(string csvText)
    {
        var result = new LevelVarient
        {
            levelOrder = new List<int>(),
        };

        var lines = csvText.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            var columns = lines[i].Split(',');

            int levelID = int.Parse(columns[0].Trim());
            result.levelOrder.Add(levelID);
        }
        return result;
    }

}
