using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Custumer : MonoBehaviour
{
    public float speed, patience;
    public CustomerTable customerTable;
    private float DeadX = 6f;
    public bool isFinish;

    void Start()
    {  
    }


    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(transform.position.x > DeadX)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CustomerTable>() != null)
        {
            if (!isFinish)
            {
                speed = 0;
                transform.Rotate(0, 90, 0);
                customerTable = other.GetComponent<CustomerTable>();
                StartCoroutine(ToLeave());
            }
        }
    }
    private IEnumerator ToLeave()
    {
        patience = UnityEngine.Random.Range(125, 160);
        customerTable.ProgressBar.duration = patience;
        customerTable.ProgressBar.StartCoroutine(customerTable.ProgressBar.ChangeProgressBar());
        yield return new WaitForSeconds(patience);
        isFinish = true;
        transform.Rotate(0, -90, 0);
        speed = 2;
        customerTable.Leave();
    }
}
