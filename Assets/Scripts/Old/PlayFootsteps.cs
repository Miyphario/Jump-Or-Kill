using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{
    /*
    public BaseEntity my_parent;
    public GameSounds sound_manager;

    private AudioSource audio_source;
    private bool disabled;

    private void Awake()
    {
        my_parent = transform.parent.transform.GetComponent<BaseEntity>();
        audio_source = transform.GetComponent<AudioSource>();

        if (my_parent != null)
        {
            if (!my_parent.has_player)
            {
                audio_source.volume = 0.5f;
            }
        }
        else
        {
            disabled = true;
        }
    }

    private void Start()
    {
        if (!disabled)
            sound_manager = my_parent.sound_manager;
    }

    public void PlayFootstep()
    {
        if (!disabled)
        {
            int i = Random.Range(0, sound_manager.footstep_sounds.Count);
            audio_source.clip = sound_manager.footstep_sounds[i];
            audio_source.pitch = Random.Range(0.85f, 1.15f);
            audio_source.Play();
        }
    }
    */
}
