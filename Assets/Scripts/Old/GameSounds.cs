using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ActionSound
{
    quit,
    to_menu,
    restart
}

public class GameSounds : MonoBehaviour
{
    public List<AudioClip> blop_sounds = new List<AudioClip>();
    [HideInInspector]
    public AudioSource audio_source;

    public List<AudioClip> footstep_sounds = new List<AudioClip>();
    public AudioClip land_sound;
    public AudioClip line_change_sound;
    public AudioClip slash_sound;

    private ActionSound cur_action;
    private bool sound_played = false;

    private void Awake()
    {
        audio_source = transform.GetComponent<AudioSource>();
        audio_source.ignoreListenerPause = true;
    }

    public void PlaySound(AudioClip sound, float pitch)
    {
        audio_source.clip = sound;
        audio_source.pitch = pitch;
        audio_source.Play();
    }

    public void PlayBlop()
    {
        audio_source.clip = blop_sounds[Random.Range(0, blop_sounds.Count)];
        audio_source.pitch = Random.Range(0.8f, 1f);
        audio_source.Play();
    }

    private void Update()
    {
        if (sound_played)
        {
            if (!audio_source.isPlaying)
            {
                switch (cur_action)
                {
                    case ActionSound.quit:
                        Application.Quit();
                        break;

                    case ActionSound.restart:
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        break;

                    case ActionSound.to_menu:
                        SceneManager.LoadScene("Menu");
                        break;
                }

                sound_played = false;
            }
        }
    }

    public void ActionWithSound(ActionSound action)
    {
        cur_action = action;
        sound_played = true;
    }
}
