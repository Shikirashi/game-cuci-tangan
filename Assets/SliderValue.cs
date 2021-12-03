using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour{
    private Slider slider;
    private AudioManager manager;
    void Start() {
        manager = FindObjectOfType<AudioManager>();
        slider = GetComponent<Slider>();
        manager.slider = slider;
        manager.slider.value = 1f;
        slider.onValueChanged.AddListener(delegate {
            ValueChange(slider.value);
        });
    }

    private void ValueChange(float value) {
        manager.changeVol(value);
	}
}
