using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //rotation cam x = 42.13

    private GameObject plr;
    private Vector3 BuildModePos = new Vector3 (-0.5f, 14, -4f);
    private Vector3 lustPos;
    private Camera mainCamera;
    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GetComponent<Camera>();
        transform.position = new Vector3(plr.transform.position.x, transform.position.y, transform.position.z);

    }

    void Update()
    {
        if (plr != null)
        {
            if (plr.transform.position.x > -3 && plr.transform.position.x < 3f)
            {
                transform.position = new Vector3(plr.transform.position.x, transform.position.y, transform.position.z);
            }
        }

    }

    public void ChangeView(bool LookingForPlayer)
    {
        if (!LookingForPlayer)
        {
            lustPos = transform.position;
            plr = null;
            transform.position = BuildModePos;
            transform.rotation =Quaternion.Euler(90,0,0);
            mainCamera.orthographic = true;
        }
        else
        {
            plr = GameObject.FindGameObjectWithTag("Player");
            transform.position = lustPos;
            transform.rotation = Quaternion.Euler(42.13f, 0, 0);
            mainCamera.orthographic = false;
        }
    }
}
