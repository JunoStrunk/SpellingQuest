using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtGameObject : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Vector3 rot;
    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(Camera.main.transform.position, Vector3.up);
        rot = target.position - transform.position;
        rot.y = 0;
        transform.rotation = Quaternion.LookRotation(rot);
    }
}
