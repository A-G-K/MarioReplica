using System;
using System.Linq;
using UnityEngine;


public class KoopaBehaviour : MonoBehaviour, IKillable
{
    [SerializeField] private float shellMoveSpeed = 2f;
    [SerializeField] private Collider2D mainCollider;
    [SerializeField] private CollisionEventer shellCollider;
    [SerializeField] private CollisionEventer topCollider;
    [SerializeField] private CollisionEventer leftTopCollider;
    [SerializeField] private CollisionEventer rightTopCollider;
    [SerializeField] private float verticalForceOnDeath = 50f;

    private BasicEnemy basicEnemy;
    private bool isShell;
    private bool hasSetShellMovement;
    private Rigidbody2D rb;

    private bool IsShell
    {
        get { return isShell; }
        set
        {
            isShell = value;
            basicEnemy.SpriteAnimator.SetBool("ShouldShell", value);
        }
    }


    private SFXManager sfxManager;


    private void Awake()
    {
        basicEnemy = GetComponent<BasicEnemy>();
        shellCollider.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        sfxManager = managers.GetComponentInChildren<SFXManager>();


        // We need to do this, just in case we have an animator to setup
        IsShell = isShell;

        topCollider.OnCollisionEnter2DEvent += collision2d =>
        {
            TurnIntoShell();
            basicEnemy.CanMove = false;

        };

        leftTopCollider.OnCollisionEnter2DEvent += collision2d =>
        {
            if (IsShell && !hasSetShellMovement)
            {
                SetEnemyMovement(shellMoveSpeed, false);
                hasSetShellMovement = true;
            }
        };
        
        rightTopCollider.OnCollisionEnter2DEvent += collision2d =>
        {
            if (IsShell && !hasSetShellMovement)
            {
                SetEnemyMovement(shellMoveSpeed, true);
                hasSetShellMovement = true;
            }
        };

        shellCollider.OnCollisionEnter2DEvent += collision2d =>
        {
            if (IsShell)
            {
                IKillable killable = collision2d.gameObject.GetComponent<IKillable>();

                if (killable != null)
                {
                    killable.KillAndFall();
                }
            }
        };
    }

    private void TurnIntoShell()
    {
        sfxManager.PlaySound(3);
        IsShell = true;
        mainCollider.gameObject.SetActive(false);
        shellCollider.gameObject.SetActive(true);
    }

    private void SetEnemyMovement(float speed, bool isMovingLeft)
    {
        basicEnemy.CanMove = true;
        basicEnemy.IsMovingLeft = isMovingLeft;
        basicEnemy.moveSpeed = speed;
    }

    public void KillSimple()
    {
        // There is no simple killed animation implemented so divert to KillAndFall
        KillAndFall();
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