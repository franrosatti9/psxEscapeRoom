using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSceneChange : InteractAnim
{
    [SerializeField] private string sceneToLoad = null;

    public override bool Interact()
    {
        base.Interact();
        
        FadeController.instance.FadeOut(sceneToLoad);
        PlayInteractionEffects();
        return true;
    }
}
