using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public enum MarioState {SMALL, BIG, FIRE};
    private MarioState currentMarioState = MarioState.SMALL;

    private PlayerMovement playerMovement;
    private Animator anim;
    private Rigidbody2D rb;
    private CapsuleCollider2D playerCol;

    [SerializeField] private GameObject fireballPrefab;
    //[SerializeField]
    [SerializeField] private Transform shootPosition;
    private int maxNumOfFireballs = 2;
    private int currentNumOfFireballs = 0;


    private LivesManager livesManager;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<Animator>();
        playerCol = GetComponent<CapsuleCollider2D>();


        GameObject constantManagers = GameObject.FindGameObjectWithTag("ConstantManagers");
        livesManager = constantManagers.GetComponentInChildren<LivesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is in FIRE mode and they press Q, shoot a fireball
        if (currentMarioState == MarioState.FIRE && Input.GetKeyDown(KeyCode.Q))
        {
            if (currentNumOfFireballs < maxNumOfFireballs)
            {
                anim.SetTrigger("shootFireball");
                ShootFireball();
            }
        }

        //Debug tools for testing state changing
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncreaseMarioState();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DecreaseMarioState();
        }
    }

    //Increases the cureent MarioState
    public void IncreaseMarioState()
    {
        if (currentMarioState != MarioState.FIRE)
        {
            StartCoroutine(playerMovement.TempFreezeMovement(1f));
            currentMarioState += 1;
            anim.SetInteger("playerState", (int)currentMarioState);
        }
    }

    //Decreases the cureent MarioState
    public void DecreaseMarioState()
    {
        if (currentMarioState != MarioState.SMALL)
        {
            StartCoroutine(playerMovement.TempFreezeMovement(1f));
            currentMarioState -= 1;
            anim.SetInteger("playerState", (int)currentMarioState);
        }
        else
        {
            //Lives Manager . Lose Life
            Debug.Log("LOSE");
            anim.SetTrigger("playerDie");
            livesManager.LoseLife();
        }
    }

    //Shoot a fireball in the direction the player is facing from a specified point
    private void ShootFireball()
    {
        currentNumOfFireballs++;
        GameObject fireballGO = Instantiate(fireballPrefab, shootPosition.position, new Quaternion());
        fireballGO.GetComponent<FireballController>().Initialise(this, playerMovement.isFacingRight() ? 1 : -1);   
    }

    public void ReduceCurrentFireballs() { currentNumOfFireballs--; }
}
