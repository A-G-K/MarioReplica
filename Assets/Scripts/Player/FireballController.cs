using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerController controller;

    [SerializeField] private Vector2 travelVelocity;
    [SerializeField] private AnimationClip clipInfo;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        rb.velocity = travelVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < travelVelocity.y)
            rb.velocity = travelVelocity;
    }

    public void Initialise(PlayerController _controller, int _dir)
    {
        controller = _controller;
        travelVelocity = new Vector2(travelVelocity.x * _dir, travelVelocity.y);
        transform.localScale = new Vector2(_dir, 1);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            StartCoroutine(Explode());
            col.gameObject.GetComponent<IKillable>().KillAndFall();
        }
        else if (col.otherCollider.GetType().FullName == "UnityEngine.CircleCollider2D" && col.gameObject.tag != "Player")   //Also check if col is with block
        {
            StartCoroutine(Explode());
        }
        else if (col.gameObject.tag != "Player")
        {
            rb.velocity = new Vector2 (travelVelocity.x, -travelVelocity.y);
        }
    }

    private IEnumerator Explode()
    {   
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(GetComponent<EdgeCollider2D>());

        RigidbodyConstraints2D freeze = new RigidbodyConstraints2D();
        freeze = RigidbodyConstraints2D.FreezeAll;
        rb.constraints = freeze;

        controller.ReduceCurrentFireballs();

        anim.SetTrigger("Explode");
        yield return new WaitForSeconds(clipInfo.length);

        Destroy(gameObject);
    }
}
