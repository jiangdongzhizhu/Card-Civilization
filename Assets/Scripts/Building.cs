using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Building
{
    public string ID { get; private set; }
    public Action OnBuilt { get; private set; }

    private static readonly Building[] allBuildings = new Building[]
    {
        new Building()
        {
            ID="farm",
            OnBuilt = () =>
            {
                BuildingFunctions.TerrainTypeBonus(TerrainType.Plain,1);
                BuildingFunctions.TerrainTypeBonus(TerrainType.Forest,1);
            }
        }
    };

    public static IReadOnlyDictionary<string, Building> dict
        = allBuildings.ToDictionary(x => x.ID);
}
