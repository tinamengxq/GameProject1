using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip harvestClip;
    [SerializeField] private AudioClip questCheckClip;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        if (bgmSource != null && !bgmSource.isPlaying)
        {
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlayHarvest()
    {
        if (sfxSource != null && harvestClip != null)
            sfxSource.PlayOneShot(harvestClip);
    }

    public void PlayQuestCheck()
    {
        if (sfxSource != null && questCheckClip != null)
            sfxSource.PlayOneShot(questCheckClip);
    }
}
