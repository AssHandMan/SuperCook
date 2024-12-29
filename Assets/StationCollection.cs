using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationCollection : MonoBehaviour
{
    public int[] StationsList = new int[64];
    public float[] RotationList;
    void Start()
    {
        RotationList = new float[64];
        SceneData data = SaveSystem.LoadCells();
        StationsList = data.pos;
        RotationList = data.rot;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            SaveSystem.SaveCells(this);
        }
    }

    public void ChangeCell(int number, int type, float rot)
    {
        StationsList[number] = type;
        RotationList[number] = rot;
        SaveSystem.SaveCells(this);
    }
}
