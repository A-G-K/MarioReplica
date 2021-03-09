using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public bool movingPlayer = false;
    UIManager uiManager;
    BackgroundMusicManager bgmManager;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        uiManager = managers.GetComponentInChildren<UIManager>();
        bgmManager = managers.GetComponentInChildren<BackgroundMusicManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movingPlayer == true)
        {
            rigidBody.velocity = new Vector2(2.0f, rigidBody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("FlagpoleHit");
            PlayerMovement playerMovement = collision.gameObject.GetComponentInParent<PlayerMovement>();
            rigidBody = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            uiManager.HitFlagPole();
            bgmManager.PlaySound(5);
            StartCoroutine(MovingDownwards());
            playerMovement.canMove = false;
        }
    }

    private IEnumerator MovingDownwards()
    {
        rigidBody.velocity = new Vector2(0f, -2f);
        yield return new WaitForSeconds(2f);
        movingPlayer = true;
        bgmManager.PlaySound(3);
    }
}
