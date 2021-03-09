using System;
using UnityEngine;


public class EnemyCleanup : MonoBehaviour
{
    [SerializeField] private float belowY = -50f;
    
    private void FixedUpdate()
    {
        if (transform.position.y < belowY)
        {
            Destroy(this);
        }
    }
}