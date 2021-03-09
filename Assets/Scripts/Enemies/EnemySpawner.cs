using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    public bool goomba = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        Debug.Log("spawning enemy");
        if (goomba)
        {
            Instantiate(enemies[0], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(enemies[1], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
