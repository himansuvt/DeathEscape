using UnityEngine;

public class BackgroundAudioController : MonoBehaviour
{
    public AudioClip normalBreathingClip;
    public AudioClip heavyBreathingClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Start with the normal breathing clip
        if (normalBreathingClip != null)
        {
            audioSource.clip = normalBreathingClip;
            audioSource.Play();
        }
    }

    public void SetHeavyBreathing(bool isHeavy)
    {
        if (isHeavy && heavyBreathingClip != null)
        {
            audioSource.clip = heavyBreathingClip;
            audioSource.Play();
        }
        else if (!isHeavy && normalBreathingClip != null)
        {
            audioSource.clip = normalBreathingClip;
            audioSource.Play();
        }
    }
}
