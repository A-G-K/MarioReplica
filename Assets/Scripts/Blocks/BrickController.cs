using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] Collider2D bottomCollider;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        uiManager = managers.GetComponentInChildren<UIManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ContactPoint2D point = collision.GetContact(0);
            if (point.otherCollider == bottomCollider)
            {
                playerController = collision.gameObject.GetComponent<PlayerController>();
                if(playerController.GetMarioState() == PlayerController.MarioState.BIG || playerController.GetMarioState() == PlayerController.MarioState.FIRE)
                {
                    uiManager.score += 100;
                    Destroy(gameObject);
                }
            }
        }
    }
}
