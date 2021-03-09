using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{

    public AudioSource BGMSource;

    public AudioClip Main;
    public AudioClip Underground;
    public AudioClip StageClear;
    public AudioClip Die;
    public AudioClip FlagPole;


    public static BackgroundMusicManager Instance = null;

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

    // Start is called before the first frame update
    void Start()
    {

//        BGMSource = gameObject.AddComponent<AudioSource>();
    }

   public void PlaySound(int ClipID)
    {

        //Stop any playing music
        BGMSource.Stop();

            switch (ClipID)
            {
                case 1:
                    BGMSource.PlayOneShot(Main);
                    break;

                case 2:
                    BGMSource.PlayOneShot(Underground);
                    break;

                case 3:
                    BGMSource.PlayOneShot(StageClear);
                    break;

                case 4:
                    BGMSource.PlayOneShot(Die);
                    break;
                case 5:
                    BGMSource.PlayOneShot(FlagPole);
                    break;

            }

        }

    }
