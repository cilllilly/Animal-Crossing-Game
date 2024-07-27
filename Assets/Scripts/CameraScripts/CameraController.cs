using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        FollowTarget(); 
    }

    private void FollowTarget()
    {
        transform.position = target.position + offset;
    }
}
