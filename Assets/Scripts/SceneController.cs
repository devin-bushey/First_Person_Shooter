using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;
    private GameObject enemy;
    private Vector3 spawnPoint = new Vector3(0, 0, 5);
    private int numEnemies = 3;
    private GameObject[] arrEnemies; 

    private void Start()
    {
        arrEnemies = new GameObject[numEnemies];
    }

    void Update()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            if (arrEnemies[i] == null)
            {
                enemy = Instantiate(enemyPrefab) as GameObject;
                enemy.transform.position = spawnPoint;
                float angle = Random.Range(0, 360);
                enemy.transform.Rotate(0, angle, 0);
                arrEnemies[i] = enemy;
            }
        }
        
    }
}
