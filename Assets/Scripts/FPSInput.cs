using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{

    private CharacterController charController;

    private float speed = 9.0f;
    private float gravity = -9.81f;

    private float pushForce = 5.0f;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal"); 
        float deltaZ = Input.GetAxis("Vertical"); 

        Vector3 movement = new Vector3(deltaX, 0, deltaZ); 

        // Clamp magnitude for diagonal movement
        movement = Vector3.ClampMagnitude (movement, 1.0f);
                                                                                                                                                 
        // determine how far to move on the XZ plane
        movement *= speed;

        movement.y = gravity;

        // Movement code Frame Rate Independent
        movement *= Time.deltaTime;
                                                                                                                                                 
        // Convert local to global coordinates
        movement = transform.TransformDirection (movement);
        charController.Move (movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody; 
        // does it have a rigidbody and is Physics enabled?
        if (body != null && !body.isKinematic) { 
            body.velocity = hit.moveDirection * pushForce; 
        } 
    }

}
