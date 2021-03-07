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

    // Start is called before the first frame update
    void Start()
    {
        GameObject constantManagers = GameObject.FindGameObjectWithTag("ConstantManagers");
        coinsManager = constantManagers.GetComponentInChildren<CoinsManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Mario")
        {
            ContactPoint2D point = collision.GetContact(0);
            if(point.otherCollider == bottomCollider)
            {
                HitBlock();
            }
        }
    }

    private void HitBlock()
    {
        if(itemInBox == Item.Coin)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            coinsManager.AddCoin();
        }

        else if (itemInBox == Item.Powerup)
        {
            //Check state of mario
            //if state = small
            //createdObject = Instantiate(mushroom, transform.position, Quaternion.identity);
            //StartCoroutine(MoveItem());
            //else
            //Instantiate(flower, transform.position, Quaternion.identity);
            //StartCoroutine(MoveItem());
        }

        else if (itemInBox == Item.Star)
        {
            createdObject = Instantiate(star, transform.position, Quaternion.identity);
            StartCoroutine(MoveItem());
        }

        else if (itemInBox == Item.Life)
        {
            createdObject = Instantiate(life, transform.position, Quaternion.identity);
            StartCoroutine(MoveItem());
        }

        Instantiate(emptyBox, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator MoveItem()
    {
        MoveMushrooms(false);

        for(int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.1f);
            createdObject.transform.position = new Vector2(createdObject.transform.position.x, createdObject.transform.position.y + 0.11f);
        }

        MoveMushrooms(true);
    }

    private void MoveMushrooms(bool move)
    {
        if (createdObject.gameObject == mushroom)
        {
            //MushroomController controller = createdObject.GetComponent<LifeMushroomController>();
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
        else if (createdObject.gameObject == life)
        {
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
}
