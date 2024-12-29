using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooker : MonoBehaviour
{
    public string IngredientType;
    public bool isBlockMovement;
    public Transform CoockPos;
    private TakeDropSystem inventory;
    public GameObject CoockObject;
    public float CookDuration;
    public ProgressBarController ProgressBar;

    private int IngredientStatment;
    private GameObject plr;
    private TakeDropSystem hand;
    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("TakeSystem").GetComponent<TakeDropSystem>();
        ProgressBar.duration = CookDuration;
    }

    
    void Update()
    {
        if(CoockObject == null & ProgressBar.image.fillAmount > 0)
        {
            ProgressBar.StopAllCoroutines();
            ProgressBar.image.fillAmount = 0;
            ProgressBar.inProgress = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<TakeDropSystem>() != null)
        {
            hand = other.gameObject.GetComponent<TakeDropSystem>();

            if (hand.tookObj != null && CoockObject == null)
            {
                if (hand.tookObj.GetComponent<IngredientStatment>().IngredientType == IngredientType)
                {
                    hand.CanDrop = true;
                    hand.CanTake = false;
                }
            }
            else
            {
                if (CoockObject != null & plr.GetComponent<PlayerMovement>().CanMove == true)
                {
                    hand.CanTake = true;
                    hand.CanDrop = false;
                    hand.typeObj = CoockObject;
                    hand.TakenFromCooker = true;
                }
            }

        }

        if (other.gameObject.layer == LayerMask.NameToLayer("FoodIngredient") && inventory.tookObj == null && CoockObject == null)
        {
            if (isBlockMovement)
            {
                plr.GetComponent<PlayerMovement>().CanMove = false;
            }
            CoockObject = other.gameObject;
            CoockObject.transform.position = CoockPos.position;

            if(CoockObject.GetComponent<IngredientStatment>().statment < CoockObject.GetComponent<IngredientStatment>().coocked.Length)
            {
                StartCoroutine(ToCook());
            }
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TakeDropSystem>() != null)
        {
            hand.CanDrop = false;
            if (hand.typeObj == CoockObject)
            {
                hand.CanTake = false;
                hand.typeObj = null;
            }
        }
    }

    private IEnumerator ToCook()
    {
        ProgressBar.StartCoroutine(ProgressBar.ChangeProgressBar());
        yield return new WaitForSeconds(CookDuration);
        CoockObject.GetComponent<IngredientStatment>().ChangeStatment();
        
        if(CoockObject.GetComponent<IngredientStatment>().statment < CoockObject.GetComponent<IngredientStatment>().coocked.Length)
        {
            ProgressBar.StartCoroutine(ProgressBar.ChangeProgressBar());
            yield return new WaitForSeconds(CookDuration);
            CoockObject.GetComponent<IngredientStatment>().ChangeStatment();
        }
        else
        {
            CoockObject.GetComponent<IngredientStatment>().IngredientType = "Cutting";
            plr.GetComponent<PlayerMovement>().CanMove = true;
        }
    }
}
