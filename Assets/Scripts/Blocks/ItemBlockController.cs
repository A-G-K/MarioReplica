using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlockController : MonoBehaviour
{
    CoinsManager coinsManager;
    [SerializeField] Collider2D bottomCollider;
    private enum Item { Coin, Powerup, Star, Life }
    [SerializeField] private Item itemInBox;
    
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject mushroom;
    [SerializeField] private GameObject flower;
    [SerializeField] private GameObject star;
    [SerializeField] private GameObject life;

    [SerializeField] private GameObject emptyBox;

    private GameObject createdObject;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject constantManagers = GameObject.FindGameObjectWithTag("ConstantManagers");
        //coinsManager = constantManagers.GetComponentInChildren<CoinsManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ContactPoint2D point = collision.GetContact(0);
            if(point.otherCollider == bottomCollider)
            {
                playerController = collision.gameObject.GetComponent<PlayerController>();
                HitBlock();
            }
        }
    }

    private void HitBlock()
    {
        if(itemInBox == Item.Coin)
        {
            Debug.Log("coin spawn");
            Instantiate(coin, new Vector2(transform.position.x, transform.position.y + 0.2f), Quaternion.identity);
            coinsManager.AddCoin();
        }

        else if (itemInBox == Item.Powerup)
        {
            if(playerController.GetMarioState() == PlayerController.MarioState.SMALL)
            {
                createdObject = Instantiate(mushroom, transform.position, Quaternion.identity);
                //StartCoroutine(MoveItem());
            } 
            else
            {
                createdObject = Instantiate(flower, transform.position, Quaternion.identity);
                //StartCoroutine(MoveItem());
            }
        }

        else if (itemInBox == Item.Star)
        {
            createdObject = Instantiate(star, transform.position, Quaternion.identity);
            //StartCoroutine(MoveItem());
        }

        else if (itemInBox == Item.Life)
        {
            createdObject = Instantiate(life, transform.position, Quaternion.identity);
            //StartCoroutine(MoveItem());
        }

        Instantiate(emptyBox, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    /*
    private void MoveMushrooms(bool move)
    {
        if (createdObject.GetComponent<RedMushroomController>())
        {
            RedMushroomController controller = createdObject.GetComponent<RedMushroomController>();
            //controller.canMove = move;

            Rigidbody2D rigidBody = createdObject.GetComponent<Rigidbody2D>();
            if (move)
            {
                rigidBody.gravityScale = 1;
            }
            else
            {
                rigidBody.gravityScale = 0;
            }
        }
        else if (createdObject.GetComponent<LifeMushroomController>())
        {
            Debug.Log("created life");
            LifeMushroomController controller = createdObject.GetComponent<LifeMushroomController>();
            controller.canMove = move;

            Rigidbody2D rigidBody = createdObject.GetComponent<Rigidbody2D>();
            if (move)
            {
                rigidBody.gravityScale = 1;
            }
            else
            {
                rigidBody.gravityScale = 0;
            }
        }
    }

    private void CollidersOn(bool col)
    {
        Collider2D[] colliders = createdObject.GetComponents<Collider2D>();
        foreach(Collider2D collider in colliders)
        {
            collider.enabled = col;
        }
    }*/
}
