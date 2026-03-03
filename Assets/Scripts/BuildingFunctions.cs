using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingFunctions
{
    public static void TerrainTypeBonus(TerrainType terrainType, int score, int range = 1)
    {
        int totalScore = 0;
        foreach (var item in SurroundingArea(range))
        {
            var info = item.value;
            if (info.TerrainType == terrainType)
            {
                totalScore += score;
            }
        }
        SelfAddScore(totalScore);
    }

    private static IEnumerable<HexGridElement<AreaInfo>> SurroundingArea(int range)
    {
        Vector2Int buildingPosition = GlobalManager.Inst.BuildingPosition;
        return GlobalManager.Inst.HexGrid.Area(buildingPosition, range);
    }

    private static void SelfAddScore(int score)
    {
        GlobalManager.BuildingAreaInfo.AddScore(score);
    }
}
