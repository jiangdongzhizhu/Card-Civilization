using System;
using System.Collections.Generic;

public enum TerrainType
{
    None,
    Plain,
    Forest,
    Mountain,
    Desert,
    Waters
}

public class AreaInfo
{
    private TerrainType terrainType;
    private List<AreaBuff> buffs = new List<AreaBuff>();

    public TerrainType TerrainType
    {
        get => terrainType;
        set
        {
            terrainType = value;
            AfterTerrainTypeChanged?.Invoke();
        }
    }
    public string BuildingID { get; private set; }
    public int Score { get; private set; }

    public event Action AfterTerrainTypeChanged;
    public event Action AfterBuildingChanged;

    public void AddScore(int score)
    {
        Score += score;
    }

    public void AddBuff(AreaBuff areaBuff)
    {
        buffs.Add(areaBuff);
    }

    public void ClearBuffs()
    {
        int i = 0;
        while (i < buffs.Count)
        {
            if (buffs[i].IsRemoved)
            {
                buffs.RemoveAt(i);
                continue;
            }
            i++;
        }
    }

    public void BuildBuilding(string buildingID)
    {
        BuildingID = buildingID;
        Building.dict[buildingID].OnBuilt?.Invoke();

        foreach (AreaBuff buff in buffs)
        {
            buff.OnBuildingChanged?.Invoke(buff);
        }
        ClearBuffs();

        AfterBuildingChanged?.Invoke();
    }
}
