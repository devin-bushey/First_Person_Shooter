using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    [SerializeField] private DoorControl doorControl;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();

            //Debug.Log("Does player have a key?: " + player.hasKey);

            if (player.hasKey)
            {
                doorControl.Operate();
                player.hasKey = false;
            }
        }

    }
}
