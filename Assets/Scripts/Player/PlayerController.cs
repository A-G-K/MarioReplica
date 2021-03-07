using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public enum MarioState {SMALL, BIG, FIRE};
    private MarioState currentMarioState = MarioState.SMALL;

    [SerializeField] private GameObject fireballPrefab;
    [SerializeField]
    [SerializeField] private Transform shootPosition;
    private int maxNumOfFireballs = 2;
    private int currentNumOfFireballs = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMarioState == MarioState.FIRE && Input.GetKeyDown(KeyCode.LeftShift))
        {
            ShootFireball();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncreaseMarioState();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DecreaseMarioState();
        }
    }

    public void IncreaseMarioState()
    {
        if (currentMarioState != MarioState.FIRE)
        {
            currentMarioState += 1;
            Debug.Log("CMS: " + currentMarioState);
        }
    }

    public void DecreaseMarioState()
    {
        if (currentMarioState != MarioState.SMALL)
        {
            currentMarioState -= 1;
            Debug.Log("CMS: " + currentMarioState);
        }
        else
        {
            //Lives Manager . Lose Life
        }
    }

    private void ShootFireball()
    {
        GameObject fireballGO = Instantiate(fireballPrefab, shootPosition.position, new Quaternion());
        //fireballGO.GetComponent<FireballController>().SetXVelocityDir();
    }
}
