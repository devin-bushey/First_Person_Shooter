using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject spawnPt;
    [SerializeField] private SceneController scene;
    [SerializeField] private int numEnemies;
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //scene.SpawnEnemies(spawnPt, numEnemies);
        }
    }
}
