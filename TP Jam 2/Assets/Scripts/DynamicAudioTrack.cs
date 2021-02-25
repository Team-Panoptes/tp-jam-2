using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DynamicAudioTrack : MonoBehaviour
{

    public AnimationCurve volumeOverAltitude;
    public float minVolume;
    public float maxVolume;

    public float maxAltitude;
    public AudioMixer audioMixer;
    public string mixerParameterName;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float altitude = Mathf.Clamp01(player.position.y / maxAltitude);
        float factor = volumeOverAltitude.Evaluate(altitude);
        float volume = factor * (maxVolume - minVolume) + minVolume;
        float currentVolume;
        audioMixer.GetFloat(mixerParameterName, out currentVolume);
        audioMixer.SetFloat(mixerParameterName, Mathf.Lerp(currentVolume, volume, 0.25f * Time.deltaTime));
    }
}
