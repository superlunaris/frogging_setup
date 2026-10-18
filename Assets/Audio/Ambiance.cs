using UnityEngine;
using System.Collections;

public class Ambiance : MonoBehaviour
{
    public AudioClip areaMusic;

    public float fadeDuration = 2f;

    public AudioSource audioSource;
    private Coroutine musicTransition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource.isPlaying && audioSource.clip == areaMusic) return;
            
            if (musicTransition != null)
            {
                StopCoroutine(musicTransition);
            }

            musicTransition = StartCoroutine(CrossfadeMusic(areaMusic));
        }
    }

    private IEnumerator CrossfadeMusic(AudioClip newClip)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0.01f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;

            yield return null;
        }

        audioSource.volume = 0f;

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeDuration;

            yield return null;
        }

        audioSource.volume = startVolume;
        musicTransition = null;
    }
}