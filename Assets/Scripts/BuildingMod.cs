using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMod : MonoBehaviour
{
    public CameraController cam;
    public GameObject JoysticButton, TakeButton, BuildButton, CookerPanelCollection, ChosenStation;
    public int StationType;
    public bool isBuildingMode;

    public GameObject[] Stations;
    void Start()
    {

    }


    void Update()
    {

    }

    public void StartBuildMode()
    {
        if (!isBuildingMode)
        {
            Time.timeScale = 0f;
            JoysticButton.SetActive(false);
            TakeButton.SetActive(false);
            CookerPanelCollection.SetActive(true);
            BuildButton.SetActive(false);
            cam.ChangeView(false);
            isBuildingMode = true;
        }
        else
        {
            Time.timeScale = 1f;
            JoysticButton.SetActive(true);
            TakeButton.SetActive(true);
            CookerPanelCollection.SetActive(false);
            BuildButton.SetActive(true);
            cam.ChangeView(true);
            ChosenStation = null;
            StationType = 0;
            isBuildingMode =false;
        }
    }
}
