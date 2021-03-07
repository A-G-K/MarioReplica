using System;
using UnityEngine;


public class KoopaBehaviour : MonoBehaviour
{
    [SerializeField] private float shellMoveSpeed = 2f;
    [SerializeField] private Collider2D mainCollider;
    [SerializeField] private Collider2D shellCollider;
    [SerializeField] private CollisionEventer topCollider;
    [SerializeField] private CollisionEventer leftTopCollider;
    [SerializeField] private CollisionEventer rightTopCollider;

    private BasicEnemyMovement basicEnemyMovement;
    private bool shouldShell;
    private bool hasSetShellMovement;

    private bool ShouldShell
    {
        get { return shouldShell; }
        set
        {
            shouldShell = value;
            basicEnemyMovement.SpriteAnimator.SetBool("ShouldShell", value);
        }
    }

    private void Awake()
    {
        basicEnemyMovement = GetComponent<BasicEnemyMovement>();
        shellCollider.gameObject.SetActive(false);
    }

    private void Start()
    {
        // We need to do this, just in case we have an animator to setup
        ShouldShell = shouldShell;

        topCollider.OnCollisionEnter2DEvent += collision2d =>
        {
            TurnIntoShell();
            basicEnemyMovement.CanMove = false;
        };

        leftTopCollider.OnCollisionEnter2DEvent += collision2d =>
        {
            if (ShouldShell && !hasSetShellMovement)
            {
                SetEnemyMovement(shellMoveSpeed, false);
                hasSetShellMovement = true;
            }
        };
        
        rightTopCollider.OnCollisionEnter2DEvent += collision2d =>
        {
            if (ShouldShell && !hasSetShellMovement)
            {
                SetEnemyMovement(shellMoveSpeed, true);
                hasSetShellMovement = true;
            }
        };
    }

    private void TurnIntoShell()
    {
        ShouldShell = true;
        mainCollider.gameObject.SetActive(false);
        shellCollider.gameObject.SetActive(true);
    }

    private void SetEnemyMovement(float speed, bool isMovingLeft)
    {
        basicEnemyMovement.CanMove = true;
        basicEnemyMovement.IsMovingLeft = isMovingLeft;
        basicEnemyMovement.moveSpeed = speed;
    }
}