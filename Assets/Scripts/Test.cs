using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform areaRoot;

    private void Start()
    {
        var area = areaRoot.GetComponentInChildren<Area>();
        area.BuildBuilding("farm");
    }
}
