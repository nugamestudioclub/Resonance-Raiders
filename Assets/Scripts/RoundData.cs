using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[System.Serializable]
public struct RoundData
{
    public PathCreator pathCreator;
    public int enemyCount;

    public RoundData(PathCreator pathCreator, int enemyCount)
    {
        this.pathCreator = pathCreator;
        this.enemyCount = enemyCount;
    }
}
