using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


//audioSource.clip = myAudioClip; // 원하는 클립을 연결
//audioSource.Play();             // 연결된 클립을 재생

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource BGMSource;
    private AudioSource SFXSource;
    private Slider BGMSlider;
    private Slider SFXSlider;
    public float bgmVolume = 1.0f;
    public float sfxVolume = 1.0f;

    public AudioClip bgmClip;
    public List<AudioClip> SFXClipList = new List<AudioClip>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }


        BGMSource = GameObject.Find("BGMSource").GetComponent<AudioSource>();
        SFXSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();

        BGMSlider = GameObject.Find("Slider_BGM").GetComponent <Slider>();
        SFXSlider = GameObject.Find("Slider_SFX").GetComponent<Slider>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        BGMSource.volume = BGMSlider.value;
        SFXSource.volume = SFXSlider.value;
    }

    public void BGM()
    {
        BGMSource.clip = bgmClip;
        BGMSource.Play();
    }

    public void ClickSound()
    {
        SFXSource.clip = SFXClipList[0];
        SFXSource.Play();
    }

    public void ShotgunFire()
    {
        SFXSource.clip = SFXClipList[1];
        SFXSource.Play();
    }

    public void ShotgunPumping()
    {
        SFXSource.clip = SFXClipList[2];
        SFXSource.Play();
    }

    public void ShotgunShallDrop()
    {
        SFXSource.clip = SFXClipList[3];
        SFXSource.Play();
    }
}