using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource SFXSource;

    public AudioClip JumpSmall;
    public AudioClip JumpSuper;
    public AudioClip Stomp;
    public AudioClip Die;
    public AudioClip Bump;
    public AudioClip Break;
    public AudioClip Coin;
    public AudioClip PowerUpAppears;
    public AudioClip PowerUp;
    public AudioClip OneUp;
    public AudioClip Fireball;
    public AudioClip Flagpole;
    public AudioClip PipeShrink;
 

    public static SFXManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }




    public void PlaySound(int ClipID)
    {

        switch (ClipID)
        {
            case 1:
                SFXSource.PlayOneShot(JumpSmall);
                break;

            case 2:
                SFXSource.PlayOneShot(JumpSuper);
                break;

            case 3:
                SFXSource.PlayOneShot(Stomp);
                break;

            case 4:
                SFXSource.PlayOneShot(Die);
                break;

            case 5:
                SFXSource.PlayOneShot(Bump);
                break;

            case 6:
                SFXSource.PlayOneShot(Break);
                break;

            case 7:
                SFXSource.PlayOneShot(Coin);
                break;

            case 8:
                SFXSource.PlayOneShot(PowerUpAppears);
                break;

            case 9:
                SFXSource.PlayOneShot(PowerUp);
                break;

            case 10:
                SFXSource.PlayOneShot(OneUp);
                break;

            case 11:
                SFXSource.PlayOneShot(Fireball);
                break;

            case 12:
                SFXSource.PlayOneShot(Flagpole);
                break;

            case 13:
                SFXSource.PlayOneShot(PipeShrink);
                break;
        }


    }



}
