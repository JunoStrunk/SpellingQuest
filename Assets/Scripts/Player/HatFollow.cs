using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float thrust = 1f;
    [SerializeField]
    float hatBobFactor = 0.2f;
    [SerializeField]
    float targetHeight = 3.5f;

    private Rigidbody rb;
    Vector3 targetPos;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    // void OnDrawGizmos()
    // {
    //     targetPos = new Vector3(target.position.x, target.position.y + targetHeight + hatBobFactor * Mathf.Sin(Time.time), target.position.z - 0.5f);
    //     Gizmos.DrawLine(transform.position, targetPos);
    // }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(target.forward, target.up);
        targetPos = new Vector3(target.position.x, target.position.y + targetHeight + hatBobFactor * Mathf.Sin(Time.time), target.position.z);
        targetPos -= transform.position;
        targetPos *= thrust;
        rb.AddForce(targetPos);
    }
}
