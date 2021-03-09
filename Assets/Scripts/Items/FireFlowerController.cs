using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerController : MonoBehaviour
{

    private PlayerController pController;
    // Start is called before the first frame update
    void Start()
    {
        CollidersOn(false);
        StartCoroutine(MoveItem());
    }

    private IEnumerator MoveItem()
    {
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(0.01f);
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.0092f);
        }
        CollidersOn(true);
    }

    private void CollidersOn(bool col)
    {
        Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = col;
        }
    }
}
