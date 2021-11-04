using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    [SerializeField] AudioSource soundAus;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }

    public void PlayTennisSound()
    {
        AudioClip tennisSFX = Resources.Load<AudioClip>("SoundFX/SF-tennis1");
        soundAus.PlayOneShot(tennisSFX);
    }
    
    public void PlayGameEndSound()
    {
        AudioClip gameEndSFX = Resources.Load<AudioClip>("SoundFX/endgameSound");
        soundAus.PlayOneShot(gameEndSFX);
    }
}
