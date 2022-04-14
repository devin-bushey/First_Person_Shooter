using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithKey : Enemy
{
    [SerializeField] GameObject keyModel;

    void DropKey()
    {
        Vector3 position = transform.position; //position of enemy
        GameObject key = Instantiate(keyModel, position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity); // key drop
        key.SetActive(true);
    }

}
