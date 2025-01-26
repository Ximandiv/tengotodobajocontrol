using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer myMixer;
    public Image fullVol;
    public Sprite noVol;

    private void Start()
    {
        SetMusicVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);

    }

    public void changeImage()
    {
        if (musicSlider.value == 0.0001) 
        {
            fullVol.overrideSprite = noVol;
        }
            
            
    }
    
}
