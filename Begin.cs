using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Begin : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas startCanvas;
    Canvas playerCanvas;
    TMP_Text loudnessMessage;
    TMP_Text microphoneMessage;
    TMP_Text message;
    Image leftAdvise;
    Image rightAdvise;
    StreamController streamController;
    private float setupBound = 5f;
    private float startBound = 40f;
    private bool setup = true;
    private float maxSetupDB = 0f;
    private float timer = 0f;

    void Start()
    {
        // find the canvas with tag "Start Canvas"
        startCanvas = GameObject.FindWithTag("Start Canvas").GetComponent<Canvas>();
        playerCanvas = GameObject.FindWithTag("Player Canvas").GetComponent<Canvas>();
        // set the canvas to be active
        startCanvas.enabled = true;
        // find the canvas child with name "loudness"
        loudnessMessage = startCanvas.transform.Find("loudness").gameObject.GetComponent<TMP_Text>();
        microphoneMessage = startCanvas.transform.Find("microphone").gameObject.GetComponent<TMP_Text>();
        message = startCanvas.transform.Find("message").gameObject.GetComponent<TMP_Text>();
        streamController = GameObject.FindWithTag("Stream").GetComponent<StreamController>();

        leftAdvise = playerCanvas.transform.Find("LeftAdvise").gameObject.GetComponent<Image>();
        leftAdvise.enabled = false;
        rightAdvise = playerCanvas.transform.Find("RightAdvise").gameObject.GetComponent<Image>();
        rightAdvise.enabled = false;
        playerCanvas.enabled = false;
    }

    void Update()
    {
        if (startCanvas.enabled) {
            loudnessMessage.text = $"{Mathf.Round(streamController.loudness)}db";
            bool active = streamController.IsActive();
            microphoneMessage.text = "Microphone is " + (active ? "enabled" : "disabled");
            if (setup) {
                if (maxSetupDB > 0) {
                    timer += Time.deltaTime;
                }
                if (streamController.loudness > setupBound && streamController.loudness > maxSetupDB) {
                    maxSetupDB = streamController.loudness;
                }
                if (timer > 5f) {
                    setup = false;
                    streamController.sensitivity = 5000 / maxSetupDB;
                    message.text = $"Supera los {startBound}db para comenzar";
                }
            }
            else {
                if (active && streamController.loudness > startBound) {
                    StartGame();
                }
            }
        } else {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                if (enemy != null && enemy.GetComponent<SlimeDetector>().slimeBehind) {
                    StartCoroutine(AdviceTurn());
                    break;
                }
            }
        }
    }

    private void StartGame()
    {
        playerCanvas.enabled = true;
        playerCanvas.transform.Find("Score").gameObject.GetComponent<TMP_Text>().text = "0";
        startCanvas.enabled = false;
        var generator = GameObject.FindWithTag("Terrain").GetComponent<EnemyGeneration>();
        generator.spawnEnabled = true;
    }

    private IEnumerator AdviceTurn()
    {
        if (!leftAdvise.enabled && !rightAdvise.enabled) {
            leftAdvise.enabled = true;
            rightAdvise.enabled = true;
            yield return new WaitForSeconds(2);
            leftAdvise.enabled = false;
            rightAdvise.enabled = false;
        }
    }
}
