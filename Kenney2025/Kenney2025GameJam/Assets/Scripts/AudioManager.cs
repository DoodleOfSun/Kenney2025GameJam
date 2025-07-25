using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


//audioSource.clip = myAudioClip; // 원하는 클립을 연결
//audioSource.Play();             // 연결된 클립을 재생

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource BGMSource;
    public AudioSource SFXSource1;
    private AudioSource SFXSource2;
    private AudioSource SFXSource3;
    private AudioSource SFXSource4;
    private AudioSource SFXSource5;
    public Slider BGMSlider;
    public Slider SFXSlider;

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
        SFXSource1 = GameObject.Find("SFXSource1").GetComponent<AudioSource>();
        SFXSource2 = GameObject.Find("SFXSource2").GetComponent<AudioSource>();
        SFXSource3 = GameObject.Find("SFXSource3").GetComponent<AudioSource>();
        SFXSource4 = GameObject.Find("SFXSource4").GetComponent<AudioSource>();
        SFXSource5 = GameObject.Find("SFXSource5").GetComponent<AudioSource>();

        BGMSlider = GameObject.Find("Slider_BGM").GetComponent<Slider>();
        SFXSlider = GameObject.Find("Slider_SFX").GetComponent<Slider>();

        BGMSource.Play();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject bgmObj = GameObject.Find("Slider_BGM");
        if (bgmObj != null && bgmObj.GetComponent<Slider>() != null)
        {

            BGMSlider = bgmObj.GetComponent<Slider>();
            BGMSource.volume = BGMSlider.value;
        }

        GameObject sfxObj = GameObject.Find("Slider_SFX");
        if (sfxObj != null && sfxObj.GetComponent<Slider>() != null)
        {
            SFXSlider = sfxObj.GetComponent<Slider>();
            SFXSource1.volume = SFXSlider.value;
            SFXSource2.volume = SFXSlider.value;
            SFXSource3.volume = SFXSlider.value;
            SFXSource4.volume = SFXSlider.value;
            SFXSource5.volume = SFXSlider.value;
        }
    }

    public void BGM()
    {
        BGMSource.clip = bgmClip;
        BGMSource.Play();
    }

    public void ClickSound()
    {
        SFXSource1.clip = SFXClipList[0];
        SFXSource1.Play();
    }

    public void ShotgunFire()
    {
        SFXSource2.clip = SFXClipList[1];
        SFXSource2.Play();
    }

    public void ShotgunPumping()
    {
        SFXSource3.clip = SFXClipList[2];
        SFXSource3.Play();
    }

    public IEnumerator ShotgunShallDrop()
    {
        SFXSource4.clip = SFXClipList[3];
        SFXSource4.Play();
        yield return new WaitForSeconds(2f);
        SFXSource4.Stop();
    }

    public void MineExplosion()
    {
        SFXSource5.clip = SFXClipList[4];
        SFXSource5.Play();
    }

    public void AllSourceStop()
    {
        BGMSource.Stop();
        SFXSource1.Stop();
        SFXSource2.Stop();
        SFXSource3.Stop();
        SFXSource4.Stop();
        SFXSource5.Stop();

    }
}