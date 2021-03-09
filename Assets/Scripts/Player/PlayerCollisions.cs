using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private Animator pAnimator;
    private LivesManager livesManager;
    private BackgroundMusicManager bgmManager;
    // Start is called before the first frame update
    void Start()
    {
        pAnimator = GetComponentInChildren<Animator>();
        GameObject constantManagers = GameObject.FindGameObjectWithTag("ConstantManagers");
        livesManager = constantManagers.GetComponentInChildren<LivesManager>();

        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        bgmManager = managers.GetComponentInChildren<BackgroundMusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillZone")
        {
            pAnimator.SetTrigger("playerDie");
            livesManager.LoseLife();
            bgmManager.PlaySound(4);
        }
    }
}
