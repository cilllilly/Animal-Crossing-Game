using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float currentSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public Vector2 lastInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fbMovement = Input.GetAxis("Vertical");
        float lrMovement = Input.GetAxis("Horizontal");

        if(fbMovement != 0 || lrMovement != 0)
        {
            lastInput = new Vector2(lrMovement, fbMovement);
        }

        Vector3 totalMovement = new Vector3(lrMovement, 0, fbMovement)*currentSpeed*Time.deltaTime;
        totalMovement.Normalize();
        transform.Translate(totalMovement);
    }
}
