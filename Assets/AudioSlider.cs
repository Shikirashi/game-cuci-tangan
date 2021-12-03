using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour{

    public Slider slider;

    private AudioManager audio;
    void Start(){
        audio = FindObjectOfType<AudioManager>();
        audio.slider = slider;
        audio.slider.value = 1f;
    }

}
