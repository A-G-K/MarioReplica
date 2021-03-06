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
            //Instantiate(mushroom, transform.position, Quaternion.identity);
            //else
            //Instantiate(flower, transform.position, Quaternion.identity);
        }

        else if (itemInBox == Item.Star)
        {
            Instantiate(star, transform.position, Quaternion.identity);
        }

        else if (itemInBox == Item.Life)
        {
            Instantiate(life, transform.position, Quaternion.identity);
        }

        Instantiate(emptyBox, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
