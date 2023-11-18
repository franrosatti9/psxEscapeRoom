using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAtPosOnInteract : InteractionFX
{
    [SerializeField] private AudioClip soundToPlay;
    [SerializeField] private Transform positionToPlay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteract()
    {
        AudioManager.instance.PlaySoundAtPosition(soundToPlay, positionToPlay.transform.position);
    }
}
