using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioSource backgroundMusicSource;
    private AudioSource clickSoundSource;
    private Slider bgmSliderForSetting;
    private Slider sfxSliderForSetting;
    private float bgmVolume = 1.0f;
    public float sfxVolume = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMusicSource = GameObject.Find("BGMSource").GetComponent<AudioSource>();
        clickSoundSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();

        bgmSliderForSetting = GameObject.Find("Slider_BGM").GetComponent <Slider>();
        sfxSliderForSetting = GameObject.Find("Slider_SFX").GetComponent<Slider>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMusicSource.volume = bgmSliderForSetting.value;
        clickSoundSource.volume = sfxSliderForSetting.value;
    }
}