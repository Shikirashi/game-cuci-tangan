using UnityEngine;

public class Enemy : CharacterStats{
    public AudioClip deathSFX;
    public EnemyDetection enemyDetection;

    public override void Die(){
        AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        Destroy(enemyDetection.ui.gameObject);
        Destroy(gameObject);
    }
}
