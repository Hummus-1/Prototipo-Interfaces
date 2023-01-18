using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyGeneration : MonoBehaviour
{
    public readonly float SPAWNTIME = 30f;
    public float spawnInterval;
    public float timer;
    public bool spawnEnabled;
    public GameObject[] slimes;
    private static readonly System.Random getRandom = new System.Random();
    private readonly Vector3[] spawnPoints = {
        new Vector3(11f,3.13f,15.2f),
        new Vector3(8.7f,2.9f,19.2f),
        new Vector3(4.6f,4.9f,21.8f),
        new Vector3(18.2f,1.37f,40f)
    };
    private Player player;
    private bool reduceTime;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Reset();
    }

    void Update () {
        timer += Time.deltaTime;
        if (timer >= spawnInterval) {
            GenerateEnemy();
        }
        if ((player.score % 5 == 0) && reduceTime && (player.score != 0)) {
            if (spawnInterval > 10f) {spawnInterval -= 1f;}
            reduceTime = false;
        } else if (player.score % 5 != 0) {
            reduceTime = true;
        }
    }

    // Update is called once per frame
    void GenerateEnemy()
    {
        if (spawnEnabled)
        {
            int direction = getRandom.Next(0,spawnPoints.Length);
            int slime = getRandom.Next(0,slimes.Length);
            Instantiate(slimes[slime], spawnPoints[direction], Quaternion.identity);
            timer = 0f;
        }
    }

    public void Reset() {
        timer = SPAWNTIME;
        spawnInterval = SPAWNTIME;
        spawnEnabled = false;
        reduceTime = true;
    }
}
