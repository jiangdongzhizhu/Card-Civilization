using System;
using System.Collections.Generic;
using System.Linq;

public class Building
{
    public string ID { get; private set; }
    public Action OnBuilt { get; private set; }

    private static readonly Building[] allBuildings = new Building[]
    {
        new Building()
        {
            ID = "farm",
            OnBuilt = () =>
            {
                BuildingFunctions.TerrainTypeBonus(TerrainType.Plain, 1);
                BuildingFunctions.TerrainTypeBonus(TerrainType.Forest, 1);
            }
        },
        new Building()
        {
            ID = "mine",
            OnBuilt = () =>
            {
                BuildingFunctions.TerrainTypeBonus(TerrainType.Desert, 1);
                BuildingFunctions.TerrainTypeBonus(TerrainType.Mountain, 1);
            }
        },
        new Building()
        {
            ID = "seaport",
            OnBuilt = () =>
            {
                BuildingFunctions.TerrainTypeBonus(TerrainType.Waters, 2);
            }
        },
        new Building()
        {
            ID = "market",
            OnBuilt = () =>
            {
                BuildingFunctions.ScoreBonus(4, 3);
            }
        },
        new Building()
        {
            ID = "library",
            OnBuilt = () =>
            {
                BuildingFunctions.Support(2, areaInfo =>
                {
                    areaInfo.AddScore(areaInfo.Score);
                });
            }
        },
    };

    public static IReadOnlyDictionary<string, Building> dict
        = allBuildings.ToDictionary(x => x.ID);
}
