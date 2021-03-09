using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawns : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (true)
        {
            Debug.Log("triggerSpawning");
            collision.gameObject.GetComponent<EnemySpawner>().SpawnEnemy();
        }
    }
}
