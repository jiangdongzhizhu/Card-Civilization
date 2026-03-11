using System;
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

    public static void ScoreBonus(int scoreThreshold, int score, int range = 1)
    {
        int totalScore = 0;
        foreach (var item in SurroundingArea(range, false))
        {
            var info = item.value;
            if (info.Score >= scoreThreshold)
            {
                totalScore += score;
            }
        }
        SelfAddScore(totalScore);
    }

    public static void Support(int times, Action<AreaInfo> effect, int range = 1)
    {
        List<AreaBuff> areaBuffs = new List<AreaBuff>();
        foreach (AreaInfo areaInfo in SurroundingArea(range, false))
        {
            AreaBuff areaBuff = new AreaBuff(areaInfo, TriggerEffect);
            areaBuffs.Add(areaBuff);
        }

        void TriggerEffect(AreaBuff areaBuff)
        {
            effect?.Invoke(areaBuff.AreaInfo);

            times--;
            if (times <= 0)
            {
                Unregister();
            }
        }

        void Unregister()
        {
            foreach (var areaBuff in areaBuffs)
            {
                areaBuff.RemoveSelf();
            }
            areaBuffs = null;
        }
    }

    private static IEnumerable<HexGridElement<AreaInfo>> SurroundingArea(int range, bool includingCenter = true)
    {
        Vector2Int buildingPosition = GlobalManager.Inst.TargetPosition;
        return GlobalManager.Inst.HexGrid.Area(buildingPosition, range, includingCenter);
    }

    private static void SelfAddScore(int score)
    {
        GlobalManager.TargetAreaInfo.AddScore(score);
    }
}
