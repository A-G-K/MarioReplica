using System;
using UnityEngine;


public class KoopaBehaviour : MonoBehaviour
{
    [SerializeField] private float shellMoveSpeed = 2f;
    [SerializeField] private Collider2D mainCollider;
    [SerializeField] private CollisionEventer shellCollider;
    [SerializeField] private CollisionEventer topCollider;
    [SerializeField] private CollisionEventer leftTopCollider;
    [SerializeField] private CollisionEventer rightTopCollider;

    private BasicEnemy basicEnemy;
    private bool isShell;
    private bool hasSetShellMovement;

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
}