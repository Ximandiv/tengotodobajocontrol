using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Image sliderHandleImage; 
    [SerializeField] private Sprite mutedHandleSprite; 
    [SerializeField] private Sprite defaultHandleSprite; 
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer myMixer;
    public Image Vol;
    public Sprite noVol;
    public Sprite fullVol;
    public Sprite twentyVol;
    public Sprite fortyVol;
    public Sprite sixtyVol;
    public Sprite eightyVol;


    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            loadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }

    private void Update()
    {
        changeImage();
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVolume", volume);

    }

    private void loadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("masterVolume");

        SetMusicVolume();
    }

    public void changeImage()
    {
        if (musicSlider.value <= 0.05f) // Muted
        {
            Vol.overrideSprite = noVol;
            sliderHandleImage.sprite = mutedHandleSprite; // Change to muted handle
        }
        else 
        {
            if (musicSlider.value <= 0.2f) 
                Vol.overrideSprite = twentyVol;
            else if (musicSlider.value <= 0.4f) 
                Vol.overrideSprite = fortyVol;
            else if (musicSlider.value <= 0.6f) 
                Vol.overrideSprite = sixtyVol;
            else if (musicSlider.value <= 0.8f) 
                Vol.overrideSprite = eightyVol;
            else 
                Vol.overrideSprite = fullVol;

            
            if (sliderHandleImage.sprite != defaultHandleSprite)
                sliderHandleImage.sprite = defaultHandleSprite;
        }
    }
}
