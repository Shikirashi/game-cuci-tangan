using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour {
    public GameObject uiPrefab;
    public Transform target;
    public float healthPercent;

    float visibleTime = 5f;
    float lastMadeVisibleTime;

    Transform ui;
    Image healthSlider;
    Transform cam;

    void Start() {
        cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>()) {
            if (c.renderMode == RenderMode.WorldSpace) {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(float maxHealth, float currentHealth) {
        Debug.Log("Enemy got hurt");
        if (ui != null) {
            ui.gameObject.SetActive(true);
            lastMadeVisibleTime = Time.time;
            healthPercent = currentHealth / (float)maxHealth;
            //healthNow = healthPercent;
            healthSlider.fillAmount = healthPercent;
            if (healthPercent <= 0) {
                Destroy(ui.gameObject);
            }

        }
    }

    void LateUpdate() {
        if (ui != null) {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if (Time.time - lastMadeVisibleTime > visibleTime) {
                ui.gameObject.SetActive(false);
            }
        }
    }
}
