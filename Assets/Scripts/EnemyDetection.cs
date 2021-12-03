using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyDetection : MonoBehaviour {
    public GameObject uiPrefab;
    public Transform target;
    float visibleTime = .1f;
    float lastMadeVisibleTime;
    bool sound;

    public Transform ui;
    Transform cam;
    public AudioClip detectionSound;

    void Start() {
        sound = false;
        cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>()) {
            if (c.renderMode == RenderMode.WorldSpace) {
                ui = Instantiate(uiPrefab, c.transform).transform;
                //healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        GetComponent<EnemyMovement>().OnSeenPlayer += OnSeenPlayer;
    }

    void OnSeenPlayer(float healthy) {
        if (ui != null) {
            //Debug.Log(GetComponent<HealthUI>().healthPercent);
            ui.gameObject.SetActive(true);
            lastMadeVisibleTime = Time.time;
            //float healthPercent = currentHealth / (float)maxHealth;
            //healthSlider.fillAmount = healthPercent;
            if (healthy <= 49) {
                Destroy(ui.gameObject);
                sound = false;
            }

        }
        if(!sound){
            AudioSource.PlayClipAtPoint(detectionSound, transform.position);
            sound = true;
        }
    }

    void LateUpdate() {
        if (ui != null) {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if (Time.time - lastMadeVisibleTime > visibleTime) {
                ui.gameObject.SetActive(false);
                sound = false;
            }
        }
    }
}
