using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BuildingWorktable : MonoBehaviour
{
    public Transform CoockPos;
    private TakeDropSystem inventory;
    public GameObject CoockObject, Burger, HamDinner;

    private TakeDropSystem hand;

    public float lustPosY;

    private List<string> BurgerRecepie = new List<string>() { "CuttingBread", "FriedKotleta", "CuttingOnion", "CuttingTomato", "CuttingCheese", "CuttingLettuce" };
    private List<string> HamDinnerRecepie = new List<string>() { "CuttingCarrot", "FriedHam", "CuttingLettuce", "CuttingPotato" };
    public List<string> Ingredients;

    public List<GameObject> TookIngredients;
    public GameObject LustObject;
    public int lustObjIndex;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("TakeSystem").GetComponent<TakeDropSystem>();
    }


    void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<TakeDropSystem>() != null)
        {
            hand = other.gameObject.GetComponent<TakeDropSystem>();
            if (hand.tookObj != null && hand.tookObj.GetComponent<IngredientStatment>().IngredientType != "Dish")
            {
                hand.CanDrop = true;
                hand.CanTake = false;
            }
            else
            {
                if (CoockObject != null)
                {
                    hand.CanTake = true;
                    hand.CanDrop = false;
                    hand.typeObj = LustObject;
                    hand.TakenFromCooker = true;
                }
            }

        }

        if (other.gameObject.layer == LayerMask.NameToLayer("FoodIngredient") && inventory.tookObj == null)
        {
            LustObject = other.gameObject;
            if (CoockObject == null)
            {
                CoockObject = other.gameObject;
                CoockObject.transform.position = CoockPos.position;
                lustPosY = CoockObject.transform.localPosition.y;
            }
            else
            {
                if (other.gameObject.transform.parent == null)
                {
                    other.gameObject.transform.position = new Vector3(CoockObject.transform.position.x, lustPosY + 0.3f, CoockObject.transform.position.z);
                    lustPosY += 0.3f;
                }
            }
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            Ingredients.Add(other.GetComponent<IngredientStatment>().Name);
            TookIngredients.Add(other.gameObject);
            lustObjIndex++;

            List<string> newData = Ingredients.Distinct().ToList();
            

            if (newData.OrderBy(x => x).SequenceEqual(BurgerRecepie.OrderBy(x => x)))
            {
                MakeDish(Burger);
            }
            else if(newData.OrderBy(x => x).SequenceEqual(HamDinnerRecepie.OrderBy(x => x)))
            {
                MakeDish(HamDinner);
            }
        }

    }

    private void AnimCreatDish()
    {

    }

    private void MakeDish(GameObject dish)
    {
        LustObject = Instantiate(dish, CoockPos.position, CoockPos.rotation);
        CoockObject = LustObject;
        Ingredients = new List<string> { dish.GetComponent<IngredientStatment>().IngredientType };
        StartCoroutine(DestroyAllIngredients());
    }

    private IEnumerator DestroyAllIngredients()
    {
        while(lustObjIndex >= 0)
        {
            Destroy(TookIngredients[lustObjIndex]);
            TookIngredients.RemoveAt(lustObjIndex);
            lustObjIndex--;
            yield return null;
        }
        lustObjIndex = 0;
        TookIngredients.Add(LustObject);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TakeDropSystem>() != null)
        {
            hand.CanDrop = false;
            hand.CanTake = false;
            hand.typeObj = null;
        }
    }
}
