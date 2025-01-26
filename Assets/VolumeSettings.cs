using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer myMixer;
    public Image fullVol;
    public Sprite noVol;
    public Sprite normalVol;

    private void Start()
    {
        SetMusicVolume();
    }

    private void Update()
    {
        changeImage();
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);

    }

    public void changeImage()
    {
        if (musicSlider.value == 0.0001f)
        {
            fullVol.overrideSprite = noVol;
        }
        else if (musicSlider.value > 0.0001f)
        {
            fullVol.overrideSprite = normalVol;
        }
    }
    
}
