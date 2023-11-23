using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickable : MonoBehaviour
{
    public SpriteRenderer sprite_rend;
    [HideInInspector]
    public int line;

    [HideInInspector]
    public GameObject player;

    public List<AudioClip> my_sounds = new List<AudioClip>();
    [HideInInspector]
    public AudioSource audio_source;
    [HideInInspector]
    public string audio_prefix;

    [HideInInspector] public float min_pitch = 0.7f;
    [HideInInspector] public float max_pitch = 1.2f;

    private bool audio_played = false;

    private void Awake()
    {
        audio_source = transform.GetComponent<AudioSource>();
    }

    public virtual void Start()
    {
        ChangeAudio();

        StartCoroutine(DestroyByDistance());
    }

    public virtual void ChangeSprite(Sprite sprite)
    {
        sprite_rend.sprite = sprite;
    }

    public virtual void ChangeAudio()
    {
        if (my_sounds != null && audio_source != null)
        {
            if (audio_prefix != null && audio_prefix != "")
            {
                List<AudioClip> tc = new List<AudioClip>();

                foreach (AudioClip ac in my_sounds)
                {
                    string[] tn = ac.name.Split('_');
                    if (tn[0] == audio_prefix)
                    {
                        tc.Add(ac);
                    }
                }

                if (tc.Count > 0)
                {
                    int i = Random.Range(0, tc.Count);
                    audio_source.clip = tc[i];
                }
            }
            else
            {
                int i = Random.Range(0, my_sounds.Count);
                audio_source.clip = my_sounds[i];
            }

            audio_source.pitch = Random.Range(min_pitch, max_pitch);
        }
    }

    public virtual void Action(Creature entity)
    {
        if (!audio_played)
        {
            if (audio_source != null && my_sounds != null)
            {
                audio_source.Play();
                sprite_rend.sprite = null;
                audio_played = true;
            }
        }
    }

    private void Update()
    {
        if (audio_played)
        {
            if (!audio_source.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!audio_played)
        {
            if (collision != null)
            {
                Creature ent = collision.transform.GetComponent<Creature>();
                if (collision.tag == "Player" && (ent.CurrentLine == line || ent.LineToMove == line))
                {
                    Action(ent);
                }
            }
        }
    }

    public IEnumerator DestroyByDistance()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > 6f && player.transform.position.x > transform.position.x)
            {
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
