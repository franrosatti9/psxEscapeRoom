using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXOnInteract : InteractionFX
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
        AudioManager.instance.PlaySfx(soundToPlay);
    }
}
