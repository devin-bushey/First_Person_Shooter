using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{

    private float speed = 9.0f;
    private float xMove;
    private float yMove;

    void Update()
    {
        this.xMove = Input.GetAxis("Horizontal");
        this.yMove = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {

        Vector3 movement = new Vector3(xMove, 0, yMove);
        transform.Translate(movement * speed * Time.deltaTime, Space.Self);

    }
}
