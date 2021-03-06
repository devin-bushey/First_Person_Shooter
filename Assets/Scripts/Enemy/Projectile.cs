using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private GameObject impactPrefab;

    private void OnCollisionEnter(Collision collision)
    {

        PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            //Debug.Log("Player Hit");
            player.Hit();
        }

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.ReactToHit();
        }

        GameObject impac = Instantiate(impactPrefab, transform.position, Quaternion.identity);
        Destroy(impac, 2);
        Destroy(gameObject);
    }

}
