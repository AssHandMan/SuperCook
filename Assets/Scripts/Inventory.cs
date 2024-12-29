using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform tookObjectPosition;
    public GameObject tookObject;

    private GameObject typeObject;
    private bool holding, CanDrop;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeObject()
    {
        if(holding)
        {
            if (CanDrop)
            {
                tookObject.transform.parent = null;
                tookObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                tookObject = null;
                holding = false;
            }
        }
        else
        {
            tookObject = Instantiate(typeObject, tookObjectPosition.position, tookObjectPosition.rotation);
            tookObject.transform.parent = tookObjectPosition.transform;
            tookObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            holding = true;
        }
        
    }



    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BoxWithStuff>() != null && !holding)
        {
            typeObject = other.GetComponent<BoxWithStuff>().Stuff;
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("KitchenStation"))
        {
            CanDrop = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("KitchenStation"))
        {
            CanDrop = false;
        }
    }
}
