using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    [SerializeField] private DoorControl doorControl;

    [SerializeField] private SceneController scene;

    [SerializeField] private int numEnemiesAlive;

    private bool hasBeenOpened = false;
    private bool hasBeenClosed = false;

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("Door: scene.enemiesAlive: " + scene.enemiesAlive + " numEnemiesAlive: " + numEnemiesAlive);

        if (scene.enemiesAlive == numEnemiesAlive)
        {
            hasBeenOpened = false;
            hasBeenClosed = false;
        }

        if (!hasBeenOpened && !hasBeenClosed && (scene.enemiesAlive == numEnemiesAlive))
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
