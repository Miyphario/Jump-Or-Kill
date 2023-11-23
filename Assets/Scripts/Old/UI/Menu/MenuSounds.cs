using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    public List<AudioClip> blop_sounds;
    public List<AudioClip> select_sounds;

    private AudioSource audio_source;

    private void Awake()
    {
        audio_source = transform.GetComponent<AudioSource>();
    }

    public void PlayBlop()
    {
        audio_source.clip = blop_sounds[Random.Range(0, blop_sounds.Count)];
        audio_source.pitch = Random.Range(0.8f, 1f);
        audio_source.Play();
    }

    public void PlaySelect()
    {
        audio_source.clip = select_sounds[Random.Range(0, blop_sounds.Count)];
        audio_source.pitch = Random.Range(0.85f, 1.05f);
        audio_source.Play();
    }
}
