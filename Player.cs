using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float score;
    public HealthBar healthBar;

    void Start() {
        maxHealth = 100f;
        score = 0f;
        health = maxHealth;
    }

    void Update() {
        
    }

    public void newElimination() {
        score += 1f;
        GameObject.FindWithTag("Score").GetComponent<TMP_Text>().text = $"{score}";
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Enemy") {
            health -= 0.01f;
            healthBar.UpdateHealthBar();
            if (health <= 0) {
                endGame();
            }
        }
    }

    private void endGame() {
        GameObject.FindWithTag("Score").GetComponent<TMP_Text>().text = "Game Over";
        // pause enemy generator
        GameObject.FindWithTag("Terrain").GetComponent<EnemyGeneration>().Reset();
        // delete all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
        // enable start canvas
        GameObject.FindWithTag("Start Canvas").GetComponent<Canvas>().enabled = true;
        // reset score
        score = 0f;
        // reset health
        health = maxHealth;
        healthBar.UpdateHealthBar();
    }
}
