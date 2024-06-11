using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlottyFace : MonoBehaviour
{
    [SerializeField] private Image slotImage; // L'immagine UI da animare
    [SerializeField] private List<Sprite> frames; // Lista di sprite per l'animazione
    [SerializeField] private float frameInterval = 0.1f; // Intervallo tra i frame in secondi

    private int currentFrameIndex = 0;
    private bool isAnimating = false;

    private void Start()
    {
        if (frames == null || frames.Count == 0)
        {
            Debug.LogError("No frames assigned for the animation.");
            return;
        }

        // Inizia l'animazione
        StartAnimation();
    }

    private void StartAnimation()
    {
        if (isAnimating) return;
        isAnimating = true;
        StartCoroutine(AnimateFrames());
    }

    private IEnumerator AnimateFrames()
    {
        while (isAnimating)
        {
            // Imposta l'immagine corrente
            slotImage.sprite = frames[currentFrameIndex];

            // Incrementa l'indice del frame corrente
            currentFrameIndex = (currentFrameIndex + 1) % frames.Count;

            // Attende il tempo impostato prima di passare al frame successivo
            yield return new WaitForSecondsRealtime(frameInterval);
        }
    }

    public void StopAnimation()
    {
        isAnimating = false;
        StopCoroutine(AnimateFrames());
    }

    public void ResumeAnimation()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            StartCoroutine(AnimateFrames());
        }
    }

    public void SetFrameInterval(float interval)
    {
        frameInterval = interval;
    }
}
