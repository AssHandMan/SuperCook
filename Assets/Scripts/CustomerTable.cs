using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CustomerTable : MonoBehaviour
{

    public GameObject CoockObject;
    public Transform CoockPos;

    public int money;
    public Text CountMoney;
    public bool isEating;
    private TakeDropSystem inventory;

    public GameObject[] Customers;
    public Transform CustomersPos;
    public ProgressBarController ProgressBar;
    public SpriteRenderer OrderMenu;
    public Sprite[] Dishes;
    public string DishOrder;
    private string[] Recepie = { "Burger", "HamDinner" };
    private int[] DishCost = { 10, 7 };
    private int OrderNumber;

    private Custumer custumer;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("TakeSystem").GetComponent<TakeDropSystem>();
        CountMoney.text = Convert.ToString(money);
        Instantiate(Customers[UnityEngine.Random.Range(0, Customers.Length)], CustomersPos.position, CustomersPos.rotation);
    }

    
    void Update()
    {       
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = 4;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            Time.timeScale = 1;
        }

        if (CoockObject != null && !isEating) 
        {
            StartCoroutine(TimeToEat());
            isEating = true;//
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Custumer>() != null)
        {
            custumer = other.GetComponent<Custumer>();
            if (custumer.patience == 0)
            {
                MakeOrder();
            }
        }
    }
    public void MakeOrder()
    {
        OrderNumber = UnityEngine.Random.Range(0, Recepie.Length);
        DishOrder = Recepie[OrderNumber];
        OrderMenu.sprite = Dishes[OrderNumber];
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<TakeDropSystem>() != null)
        {
            if (inventory.tookObj != null && inventory.tookObj.GetComponent<IngredientStatment>().IngredientType == "Dish")
            {
                if (inventory.tookObj.GetComponent<IngredientStatment>().Name == DishOrder)
                {
                    inventory.CanDrop = true;
                }
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Dish") && inventory.tookObj == null)
        {
            if (CoockObject == null && other.GetComponent<IngredientStatment>().Name == DishOrder)
            {
                CoockObject = other.gameObject;
                CoockObject.transform.position = CoockPos.position;
            }
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TakeDropSystem>() != null)
        {
            inventory.CanDrop = false;
        }
    }

    private IEnumerator TimeToEat()
    {
        custumer.StopAllCoroutines();
        yield return new WaitForSeconds(0.5f);
        money += DishCost[OrderNumber];
        CountMoney.text = Convert.ToString(money);
        Destroy(CoockObject);
        OrderMenu.sprite = null;
        DishOrder = null;
        isEating =false;
        custumer.isFinish = true;
        custumer.transform.Rotate(0, -90, 0);
        custumer.speed = 2;
        ProgressBar.StopAllCoroutines();
        ProgressBar.image.fillAmount = 0f;
        yield return new WaitForSeconds(0.5f);
        Instantiate(Customers[UnityEngine.Random.Range(0, Customers.Length)], CustomersPos.position, CustomersPos.rotation);
    }

    public void Leave()
    {
        money -= DishCost[OrderNumber];
        CountMoney.text = Convert.ToString(money);
        OrderMenu.sprite = null;
        DishOrder = null;
        Instantiate(Customers[UnityEngine.Random.Range(0, Customers.Length)], CustomersPos.position, CustomersPos.rotation);
    }
}
