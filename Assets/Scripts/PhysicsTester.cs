using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTester : MonoBehaviour
{

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.AddForce(1, 0, 0, ForceMode.Force);
        rb.AddTorque(0, 0, 20);
    }

}
