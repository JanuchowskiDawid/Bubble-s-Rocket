using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    Rigidbody rigidbdy;
    [SerializeField] float acceleration = 100f;
    [SerializeField] float rotationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbdy = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
        Rotate();
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotation(-rotationSpeed);
        }
    }

    private void Rotation(float rotationThisFrame)
    {
        transform.Rotate(0, 0, rotationThisFrame * Time.deltaTime);
    }

    private void Fly()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbdy.AddRelativeForce(0, acceleration*Time.deltaTime, 0);
        }
    }
}
