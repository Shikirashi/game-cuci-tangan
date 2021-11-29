using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats {
    float selfStab = 10f;
    public PlayerHealthUI health;
    public PlayerMovement playerMovement;
    public MouseLook mouseLook;
    public GameObject washHands;
    public GameObject camer;
    public GameObject washHandsText;
    public Transform handWasher;

    private float regenDelay = 0f;
    private float regenAmount = 1f;
    private GameObject handWashing;

    PlayerManager PlayerManager;

    private void Start() {
        health.SetMaxHealth(maxHealth);
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChangedd;
        washHandsText.SetActive(false);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.T)) {
            Debug.Log("You stabbed yourself");
            TakeDamage(selfStab);
        }
        if(CurrentHealth < 100f && Time.time >= regenDelay && Input.GetKey(KeyCode.F)) {
            //GameObject handWashing;
            CurrentHealth += regenAmount * 10f * Time.deltaTime;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, 100f);
            health.SetHealth(CurrentHealth);
            if(handWashing == null){
                handWashing = Instantiate(washHands, handWasher.transform.position, Quaternion.LookRotation(handWasher.transform.forward)) as GameObject;
                handWashing.transform.parent = camer.transform;
                Destroy(handWashing, 1f);
            }
            washHandsText.SetActive(true);
        }
        else{
            washHandsText.SetActive(false);
        }
    }
    
    void OnHealthChangedd(float maxHealth, float currentHealth) {
        Debug.Log("Player got hurt");
        regenDelay = Time.time + 5f;
    }

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
        health.SetHealth(CurrentHealth);
    }

    public override void Die() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //PlayerManager.KillPlayer();
        //playerMovement.enabled = false;
        //mouseLook.enabled = false;
        //Time.timeScale = 0.1f;
        //Destroy(gameObject);
    }
}
