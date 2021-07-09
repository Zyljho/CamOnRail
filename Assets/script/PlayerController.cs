using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;

    float speed = 4.0f;
    float angularSpeed = 2.0f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 velocity = rb.velocity;

        velocity.x = transform.forward.x * Input.GetAxis("Vertical") * speed;
        velocity.z = transform.forward.z * Input.GetAxis("Vertical") * speed;

        rb.velocity = velocity;

        rb.angularVelocity = Vector3.up * Input.GetAxis("Horizontal") * angularSpeed;
    }
}
