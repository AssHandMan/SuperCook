using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceForStation : MonoBehaviour
{
    public int number, stationType;
    public StationCollection stations;
    public BuildingMod buildingMod;
    public Canvas canvas;
    public GameObject choosenStation, station, zone;

    private bool PlayerIn;
    void Start()
    {
        number = (int)transform.position.x + 4 + 8 * (2 - (int)transform.position.z);
        stationType = stations.StationsList[number];
        if (stationType != 0)
        {
            station = Instantiate(buildingMod.Stations[stationType], transform.position, transform.rotation);
            station.transform.SetParent(canvas.transform, true);
            station.transform.rotation = Quaternion.Euler(0, stations.RotationList[number], 0);
        }
    }

    void Update()
    {
        if (buildingMod.isBuildingMode)
        {
            if (!zone.activeSelf && station == null && !PlayerIn)
            {
                zone.SetActive(true);
            }
        }
        else
        {
            if (zone.activeSelf)
            {
                zone.SetActive(false);
            }
        }
    }
    private void OnMouseDown()
    {
        if (buildingMod.isBuildingMode)
        {
            if(station != null)
            {
                station.transform.Rotate(0, 90f, 0);
                stations.ChangeCell(number, stations.StationsList[number], station.transform.eulerAngles.y);
            }
            
            if (buildingMod.ChosenStation != null)
            {
                if (buildingMod.ChosenStation.name == "Delete")
                {
                    Destroy(station);
                    stationType = 0;
                    stations.ChangeCell(number, stationType, 0f);
                }
                else if(station == null)
                {
                    station = Instantiate(buildingMod.ChosenStation, transform.position, transform.rotation);
                    station.transform.SetParent(canvas.transform, true);
                    zone.SetActive(false);
                    stations.ChangeCell(number, buildingMod.StationType, 0f);
                }

            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            PlayerIn = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag =="Player")
        {
            PlayerIn = false;
        }
    }
}
