using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    [SerializeField] private GameObject fade;
    
        [Tooltip("Este string es el nombre del trigger que tendrá.")]
        [SerializeField] private string TriggerStartFade;
    
        [SerializeField] private UnityEngine.UI.Image fadeImage;
        [SerializeField] private float fadeInLength = 1f;
        [SerializeField] private float fadeInDelay = 0f;
        [SerializeField] private float fadeOutLength = 1f;
        [SerializeField] private float fadeOutDelay = 0f;
        [SerializeField] private AnimationCurve curve;

        public static FadeController instance;
    
        private void Awake()
        {
           instance = this;
           fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        }
    
        private void Start()
        {
            FadeIn();
        }
    
        public void FadeIn()
        {
            StartCoroutine(Fade(0f, fadeInLength));
        }
        
        public void FadeOut()
        {
            StartCoroutine(Fade(1f, fadeOutLength));
            //fade.GetComponent<Animator>().SetTrigger(TriggerStartFade);
        }
        public void FadeOut(string sceneToLoad)
        {
            StartCoroutine(Fade(1f, fadeOutLength, sceneToLoad));
            //fade.GetComponent<Animator>().SetTrigger(Trigger);
        }
        public void FadeOut(float timeToWait)
        {
            Invoke(nameof(FadeOut), timeToWait);
        }
        
        public IEnumerator Fade(float endValue,  float duration, string sceneToLoad = null)
        {
            
            float elapsedTime = 0;
            float startValue = fadeImage.color.a;
    
            if (endValue == 0f && fadeInDelay > 0) yield return new WaitForSeconds(fadeInDelay);
            else if (endValue == 1f && fadeOutDelay > 0) yield return new WaitForSeconds(fadeOutDelay);
            
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, endValue,  curve.Evaluate(elapsedTime/duration));
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
                yield return null;
            }
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, endValue);

            if (sceneToLoad != null) SceneManager.LoadScene(sceneToLoad);

        }
}
