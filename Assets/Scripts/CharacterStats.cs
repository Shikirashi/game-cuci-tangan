using UnityEngine;

public class CharacterStats : MonoBehaviour{
    public float maxHealth = 100f;
    public float CurrentHealth;  //{ get; private set; }
    public static int healthNowTxt;

    public event System.Action<float, float> OnHealthChanged;

    void Awake() {
        CurrentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage) {
        damage = Mathf.Clamp(damage, 0f, float.MaxValue);

        CurrentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage!");
        Debug.Log(OnHealthChanged);
        if (OnHealthChanged != null) {
            OnHealthChanged(maxHealth, CurrentHealth);
        }
        if (CurrentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        Debug.Log(transform.name + " died.");
    }
}
