using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    public float speed = 5;
    private float xMove;
    private float yMove;

    private Rigidbody rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        this.xMove = Input.GetAxis("Horizontal");
        this.yMove = Input.GetAxis("Vertical");

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 movement = new Vector3(xMove, 0, yMove);
        //transform.Translate(movement * speed * Time.deltaTime);

        //rbody.AddForce(movement * speed);
        rbody.velocity = movement * speed * 100 * Time.deltaTime; // x100 to convert to N

    }

}
