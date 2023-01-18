using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamController : MonoBehaviour
{
    public int sampleWindow;
    public float sensitivity;
    public float threshold;
    public float loudness;

    private string deviceName;
    private AudioClip capturedClip;
    private ParticleSystem.EmissionModule bubbleEmission;

    // Start is called before the first frame update
    void Start()
    {
        sampleWindow = 64;
        sensitivity = 150f;
        threshold = 0.1f;
        loudness = 0f;

        deviceName = "";
        capturedClip = RecordFromMicrophone();
        bubbleEmission = GetComponent<ParticleSystem>().emission;
        bubbleEmission.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActive())
        {
            Debug.Log("Failed to record from microphone!");
            return;
        }

        loudness = LoudnessFromMicrophone() * sensitivity;
        if (loudness < threshold)
        {
            loudness = 0f;
        }
        // Debug.Log($"Loudness: {loudness}");
        
        bubbleEmission.rateOverTime = loudness;    
    }

    public AudioClip RecordFromMicrophone()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.Log("Cannot find any microphone plugged in!");
            return null;
        }
        // You can also leave it empty to use default device.
        // deviceName = Microphone.devices[0];
        // Debug.Log("Using input device: " + deviceName);
        return Microphone.Start(deviceName, true, 20, AudioSettings.outputSampleRate);
    }

    public bool IsActive()
    {
        return capturedClip != null && Microphone.IsRecording(deviceName);
    }

    public float LoudnessFromMicrophone()
    {
        int position = Microphone.GetPosition(deviceName);
        return LoudnessFromAudioClip(position, capturedClip);
    }

    public float LoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        // Collect a certain amount of data before given position.
        int startPosition = clipPosition - sampleWindow;
        // Taking a sample from the beginning can result in a negative position.
        if (startPosition < 0)
        {
            return 0f;
        }
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        // Compute loudness as mean absolute value (-1 < data < 1).
        float totalLoudness = 0f;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness / sampleWindow;
    }
}
