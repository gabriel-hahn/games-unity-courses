using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigiBody;

    // Start is called before the first frame update
    void Start()
    {
        rigiBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigiBody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigiBody.AddRelativeForce(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rigiBody.AddRelativeForce(Vector3.left);
        }
    }
}
