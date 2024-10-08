using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materialize : MonoBehaviour
{
    public float materializeDuration = 7f; // Time it takes to fully materialize
    public float targetScale = 1f; // Final scale of the TARDIS
    public AudioClip materializeSound; // Sound played during materialization
    private AudioSource audioSource;

    private Vector3 initialScale;
    private float elapsedTime = 0f;
    private bool isMaterializing = false;

    private void Start()
    {
        initialScale = Vector3.zero; // Start with scale zero
        transform.localScale = initialScale;

        audioSource = gameObject.AddComponent<AudioSource>();
        if (materializeSound != null)
        {
            audioSource.clip = materializeSound;
        }

        StartMaterialization();
    }

    private void Update()
    {
        if (isMaterializing)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / materializeDuration;

            // Scale the TARDIS
            transform.localScale = Vector3.Lerp(initialScale, new Vector3(targetScale, targetScale, targetScale), progress);

            // Play the materialize sound once
            if (materializeSound != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

            // Complete the materialization
            if (progress >= 1f)
            {
                isMaterializing = false;
                transform.localScale = new Vector3(targetScale, targetScale, targetScale); // Ensure final scale is correct
                audioSource.Stop(); // Stop the sound
            }
        }
    }

    public void StartMaterialization()
    {
        isMaterializing = true;
        elapsedTime = 0f; // Reset the timer
    }
}
