using System;
using System.Linq;
using UnityEngine;


public class GoombaBehaviour : MonoBehaviour, IKillable
{
    [SerializeField] private float verticalForceOnDeath = 1f;


    private Rigidbody2D rb;
    private BasicEnemy basicEnemy;

    private SFXManager sfxManager;

    private void Awake()
    {
        basicEnemy = GetComponent<BasicEnemy>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        sfxManager = managers.GetComponentInChildren<SFXManager>();
    }

    public void KillSimple()
    {
        basicEnemy.CanMove = false;
        basicEnemy.SpriteAnimator.SetBool("IsDead", true);
        sfxManager.PlaySound(3);
        Destroy(gameObject, 0.3f);
    }

    public void KillAndFall()
    {
        // Disable all collisions
        foreach (Collider2D col in GetComponents<Collider2D>().Concat(GetComponentsInChildren<Collider2D>()))
        {
            col.isTrigger = true;
        }
        
        basicEnemy.SpriteAnimator.SetBool("IsFalling", true);
        basicEnemy.CanMove = false;
        
        // Rise up the sprite
        rb.AddForce(new Vector2(0, verticalForceOnDeath), ForceMode2D.Force);
    }
}