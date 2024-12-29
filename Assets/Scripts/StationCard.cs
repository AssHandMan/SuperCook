using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationCard : MonoBehaviour
{
    public GameObject stationRef;
    public BuildingMod buildingMod;
    public int stationType;
    public bool wasTaken;

    private void Update()
    {
        if(buildingMod.ChosenStation != stationRef)
        {
            wasTaken = false;
        }
    }

    public void TakeCard()
    {
        if(!wasTaken)
        {
            buildingMod.ChosenStation = stationRef;
            buildingMod.StationType = stationType;
            wasTaken = true;
        }
        else
        {
            buildingMod.ChosenStation = null;
            buildingMod.StationType = 0;
            wasTaken = false;
        }
    }
}
