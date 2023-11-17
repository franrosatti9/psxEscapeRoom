using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private float minRandomPitch;
    [SerializeField] private float maxRandomPitch;
    [SerializeField] private AudioClip explosionClip;

    private Dictionary<GlobalSfx, AudioClip> _clipsDictionary = new Dictionary<GlobalSfx, AudioClip>();
    

    public static AudioManager instance;
    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }
    void Start()
    {
        _clipsDictionary[GlobalSfx.Explosion] = explosionClip;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    public void PlaySfxRandomPitch(AudioClip clip)
    {
        //Debug.Log("sound");
        sfxAudioSource.pitch = Mathf.Lerp(minRandomPitch, maxRandomPitch,Random.value);
        sfxAudioSource.PlayOneShot(clip);
    }

    public void PlaySoundAtPosition(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos);
    }
    
    public void PlaySoundAtPosition(GlobalSfx clipKey, Vector3 pos)
    {
        _clipsDictionary.TryGetValue(clipKey, out AudioClip clip);
        AudioSource.PlayClipAtPoint(clip, pos);
    }
}

public enum GlobalSfx
{
    Explosion
}
