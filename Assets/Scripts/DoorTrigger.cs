using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    [SerializeField] private DoorControl doorControl;

    [SerializeField] private SceneController scene;

    [SerializeField] private int numEnemiesAliveInRoom;

    private bool hasBeenOpened = false;
    private bool hasBeenClosed = false;

    void OnTriggerEnter(Collider other)
    {

        if (scene.enemiesAlive != numEnemiesAliveInRoom)
        {
            Debug.Log("Door Locked!: " + (scene.enemiesAlive - numEnemiesAliveInRoom) + " enemie(s) left");
        }
        
        if (scene.enemiesAlive == numEnemiesAliveInRoom)
        {
            hasBeenOpened = false;
            hasBeenClosed = false;
        }

        if (!hasBeenOpened && !hasBeenClosed && (scene.enemiesAlive == numEnemiesAliveInRoom))
        {
            if (other.tag == "Player")
            {
                doorControl.Operate();
                hasBeenOpened = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {

        if (hasBeenOpened && !hasBeenClosed)
        {
            if (other.tag == "Player")
            {
                doorControl.Operate();
                hasBeenClosed = true;
            }
        }
        
    }
}
