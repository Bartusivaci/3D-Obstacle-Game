using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustPower = 1000f;
    [SerializeField] float rotationPower = 100f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationPower);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationPower);
        }
    }

    void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true; // freezing rotation in order to rotate manually
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing to let physics system rotate
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }
}
