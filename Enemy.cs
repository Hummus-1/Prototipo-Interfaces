using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;
    public float maxHealth;
    public float health;
    public float speed;
    public Transform target;
    public float range;
    public SlimeHealthBar slimeHealthBar;
    void Start()
    {
        maxHealth = 100f;
        health = maxHealth;
        range = 5f;
        speed = 0.005f;
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist >= range){
            transform.LookAt(target.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);      
        }
    }

    private void OnParticleCollision(GameObject other) {
        if(other.name == "Bubbles") {
            health -= 5f;
            slimeHealthBar.UpdateHealthBar();
            if (health <= 0) {
                target.GetComponent<Player>().newElimination();
                explosion = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Terrain") {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Terrain") {
            // switch to 'non-kinematic'
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero; // or another initial value
        }
    }
}
