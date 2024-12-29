using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWithStuff : MonoBehaviour
{
    public GameObject Stuff;

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TakeDropSystem>() != null)
        {
            TakeDropSystem hand = other.GetComponent<TakeDropSystem>();
            if (hand.tookObj == null) { hand.typeObj = Stuff; }
            hand.CanTake = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TakeDropSystem>() != null)
        {
            TakeDropSystem hand = other.GetComponent<TakeDropSystem>();
            if (hand.typeObj == Stuff)
            {
                hand.typeObj = null;
                hand.CanTake = false;
            }
        }
    }
}
