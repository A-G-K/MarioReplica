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

    [SerializeField] GameObject smallCollider;
    [SerializeField] GameObject bigCollider;


    private SFXManager sfxManager;
    private BackgroundMusicManager bgmManager;
    private LivesManager livesManager;
    private UIManager uiManager;

    private bool canBeHit = true;
    public float hitTimer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<Animator>();
        playerCol = GetComponent<CapsuleCollider2D>();


        GameObject constantManagers = GameObject.FindGameObjectWithTag("ConstantManagers");
        livesManager = constantManagers.GetComponentInChildren<LivesManager>();
        GameObject managers = GameObject.FindGameObjectWithTag("Managers");

        uiManager = managers.GetComponentInChildren<UIManager>();
        sfxManager = managers.GetComponentInChildren<SFXManager>();
        bgmManager = managers.GetComponentInChildren<BackgroundMusicManager>();
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

        //jump sounds
        if ((currentMarioState == MarioState.SMALL) && Input.GetKeyDown(KeyCode.Space))

        {
            sfxManager.PlaySound(1);
        }
        else if ((currentMarioState == MarioState.BIG || currentMarioState == MarioState.FIRE) && Input.GetKeyDown(KeyCode.Space))
        {
            sfxManager.PlaySound(2);
        }
    }



    //Increases the cureent MarioState
    public void IncreaseMarioState()
    {
        if (currentMarioState != MarioState.FIRE)
        {
            sfxManager.PlaySound(9);
            StartCoroutine(playerMovement.TempFreezeMovement(1f));
            currentMarioState += 1;
            anim.SetInteger("playerState", (int)currentMarioState);
        }

        UpdateMarioCollider();
    }

    //Decreases the cureent MarioState
    public void DecreaseMarioState()
    {
        if (currentMarioState != MarioState.SMALL)
        {
            sfxManager.PlaySound(13);
            StartCoroutine(playerMovement.TempFreezeMovement(1f));
            currentMarioState -= 1;
            anim.SetInteger("playerState", (int)currentMarioState);
        }
        else
        {
            //Lives Manager . Lose Life
            Debug.Log("LOSE");
            anim.SetTrigger("playerDie");
            bgmManager.PlaySound(4);
            livesManager.LoseLife();
        }

        UpdateMarioCollider();
    }

    private void UpdateMarioCollider()
    {
        if (currentMarioState == MarioState.SMALL)
        {
            smallCollider.SetActive(true);
            bigCollider.SetActive(false);
        }
        else
        {
            smallCollider.SetActive(false);
            bigCollider.SetActive(true);
        }
    }

    //Shoot a fireball in the direction the player is facing from a specified point
    private void ShootFireball()
    {
        currentNumOfFireballs++;
        GameObject fireballGO = Instantiate(fireballPrefab, shootPosition.position, new Quaternion());
        fireballGO.GetComponent<FireballController>().Initialise(this, playerMovement.isFacingRight() ? 1 : -1);
        sfxManager.PlaySound(11);
    }

    public void ReduceCurrentFireballs() { currentNumOfFireballs--; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mushroom")
        {
            IncreaseMarioState();
            uiManager.score += 1000;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "FireFlower")
        {
            IncreaseMarioState();
            IncreaseMarioState();
            uiManager.score += 2000;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "LeftTopEnemy")
        {
            Debug.Log("jumped on enemy");
            uiManager.score += 100;
            collision.gameObject.GetComponent<IKillable>().KillAndFall();
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (canBeHit)
            {
                DecreaseMarioState();
                canBeHit = false;
                StartCoroutine(HitTimer());
            }
        }
    }

    public MarioState GetMarioState()
    {
        return currentMarioState;
    }

    IEnumerator HitTimer()
    {
        yield return new WaitForSeconds(hitTimer);
        canBeHit = true;
    }
}
