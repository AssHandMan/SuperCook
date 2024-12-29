using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public int[] pos;
    public float[] rot;
    public SceneData(StationCollection place)
    {
        pos = new int[64];
        rot = new float[64];

        pos = place.StationsList;
        rot = place.RotationList;
    }
}
