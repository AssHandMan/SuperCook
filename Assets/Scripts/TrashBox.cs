using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBox : MonoBehaviour
{
    private TakeDropSystem inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("TakeSystem").GetComponent<TakeDropSystem>();
    }

    public void OnTriggerEnter(Collider other)
    {
        inventory.CanDrop = true;
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("FoodIngredient") && inventory.tookObj == null)
        {
            Destroy(other.gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        inventory.CanDrop = false;
    }
}
