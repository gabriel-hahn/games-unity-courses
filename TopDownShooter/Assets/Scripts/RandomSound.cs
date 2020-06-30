using UnityEngine;

public class RandomSound : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    private void Start()
    {
        int randomNumber = Random.Range(0, audioClips.Length);

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClips[randomNumber]);
    }
}
