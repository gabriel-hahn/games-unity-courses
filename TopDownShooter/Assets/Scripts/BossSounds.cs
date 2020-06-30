using UnityEngine;

public class BossSounds : MonoBehaviour
{
    public AudioClip[] audioClips;
    public float timeBetweenSoundEffects;

    private AudioSource audioSource;
    private float nextSoundEffectTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time >= nextSoundEffectTime)
        {
            int randomNumber = Random.Range(0, audioClips.Length);

            audioSource.PlayOneShot(audioClips[randomNumber]);
            nextSoundEffectTime = Time.time + timeBetweenSoundEffects;
        }
    }
}
