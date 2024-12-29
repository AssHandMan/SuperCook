using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDropSystem : MonoBehaviour
{
    //can take = 1 can drop = 0
    public bool CanTake, CanDrop, TakenFromCooker;
    public GameObject tookObj, typeObj;
    public Transform ObjectPosition;
    private Cooker cooker;
    private BuildingWorktable buildTable;
    public void TakeObject()
    {
        if(CanTake)
        {
            if (tookObj == null)
            {
                tookObj = Instantiate(typeObj, ObjectPosition.transform.position, ObjectPosition.transform.rotation);
                tookObj.transform.parent = ObjectPosition.transform;
                tookObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

                if (TakenFromCooker)
                {
                    if (cooker != null)
                    {
                        cooker.StopAllCoroutines();
                        cooker.ProgressBar.image.fillAmount = 0;
                        cooker.ProgressBar.StopAllCoroutines();
                        cooker.ProgressBar.inProgress = false;
                        Destroy(cooker.CoockObject);
                    }
                    else if (buildTable != null)
                    {
                        Destroy(buildTable.TookIngredients[buildTable.lustObjIndex]);
                        buildTable.TookIngredients.RemoveAt(buildTable.lustObjIndex);
                        buildTable.Ingredients.RemoveAt(buildTable.lustObjIndex);
                        if (buildTable.lustObjIndex >= 0)
                        {
                            buildTable.lustObjIndex--;
                        }
                        if (buildTable.lustObjIndex >= 0) { buildTable.LustObject = buildTable.TookIngredients[buildTable.lustObjIndex]; }
                        buildTable.lustPosY -= 0.3f;
                    }
                    TakenFromCooker = false;
                }
            }
        }
        else if(CanDrop)
        {
            if (tookObj != null)
            {
                typeObj = null;
                tookObj.transform.parent = null;
                tookObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                tookObj = null;
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Cooker>() != null)
        {
            cooker = other.GetComponent<Cooker>();
        }
        if (other.GetComponent<BuildingWorktable>() != null)
        {
            buildTable = other.GetComponent<BuildingWorktable>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Cooker>() != null)
        {
            cooker = null;
        }
        if (other.GetComponent<BuildingWorktable>() != null)
        {
            buildTable = null;
        }
    }
}
