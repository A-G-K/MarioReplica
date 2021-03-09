using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    UIManager uiManager;
    [SerializeField] FlagController flagController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        uiManager = managers.GetComponent<UIManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        flagController.movingPlayer = false;
        //uiManager.end();
    }
}
