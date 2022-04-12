using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private bool doorIsOpen = false;
    private Vector3 homePos;
    [SerializeField] private bool isNorthSouth;
    private Vector3 closeOffset = new Vector3(5, 0, 0);
    private float moveTime = 2.0f;

    public void Start()
    {
        homePos = transform.position;
    }
    public void Operate()
    {
        if (isNorthSouth)
        {
            closeOffset = new Vector3(0, 0, 5);
        }

        if (doorIsOpen) 
        { 
            iTween.MoveTo(this.gameObject, homePos, moveTime); 
        }
        else
        {
            iTween.MoveTo(this.gameObject, homePos + closeOffset, moveTime);
        }
        doorIsOpen = !doorIsOpen;
    }

}
