using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInteract : MonoBehaviour
{
    public Camera mainCam;
    public LayerMask interactLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray.origin, ray.direction *10, out hitInfo, Mathf.Infinity, interactLayer, QueryTriggerInteraction.Ignore))
            {
                IInteractable interactableHit = hitInfo.collider.GetComponent<IInteractable>();
                interactableHit.Interact();
            }
        }
    }
}
