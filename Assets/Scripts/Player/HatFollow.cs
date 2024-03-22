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

    void FixedUpdate()
    {
        targetPos = new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);
        Debug.Log(targetPos);
        targetPos *= thrust;
        rb.AddForce(Vector3.MoveTowards(transform.position, targetPos, 0.01f));
    }
}
