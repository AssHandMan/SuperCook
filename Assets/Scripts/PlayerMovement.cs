using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public FloatingJoystick joystick;
    private Rigidbody rb;
    public bool CanMove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CanMove = true;
        LoadPlayer();
    }

    void Update()
    {
        if (CanMove)
        {
            rb.velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);

            Vector3 movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            movementDirection.Normalize();
            if (movementDirection != Vector3.zero) { transform.forward = movementDirection; }
        }
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Vector3 pos;
        pos.x = data.pos[0];
        pos.y = data.pos[1];
        pos.z = data.pos[2];
        transform.position = pos;
    }


    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveSystem.SavePlayer(this);
        }
    }
}
