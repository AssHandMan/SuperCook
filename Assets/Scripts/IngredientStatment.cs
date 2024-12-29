using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientStatment : MonoBehaviour
{
    [SerializeField]
    private MeshFilter model;
    public GameObject[] coocked;

    public string IngredientType, Name;
    public string[] Names;
    public int statment;
    public float speedRotate;

    private void Start()
    {
    }

    private void Update()
    {
        transform.Rotate(0, speedRotate * Time.deltaTime, 0);
        if(Input.GetKeyDown(KeyCode.D)) { ChangeStatment(); }
    }

    public void ChangeStatment()
    {
        if (statment < coocked.Length)
        {
            model.mesh = coocked[statment].GetComponent<MeshFilter>().sharedMesh;
            Name = Names[statment];
            statment++;
        }
    }
}
