using System.Collections;
using UnityEngine;
/*
public class Destructible : MonoBehaviour
{
    [HideInInspector]
    public int line { get; set; }
    public float damage = 1f;
    public string victim_tag;
    public DamageType damage_type;
    public Sprite sprite_to_diffuse, sprite_to_boom;
    public bool activated = true;
    public AudioClip explode_sound, defuse_sound;

    [HideInInspector]
    public GameObject player;

    private SpriteRenderer my_sprite;
    private AudioSource audio_source;

    public virtual void Awake()
    {
        my_sprite = transform.Find("Sprite").transform.GetComponent<SpriteRenderer>();
        audio_source = transform.GetComponent<AudioSource>();

        StartCoroutine(DestroyByDistance(1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
        {
            BaseEntity e = collision.transform.GetComponent<BaseEntity>();
            if (collision.tag == victim_tag && e != null)
            {
                if (e.line == line || e.lineToMove == line)
                {
                    e.TakeDamage(damage, damage_type, null, gameObject);
                    if (e.has_player)
                    {
                        StartCoroutine(e.cam_controller.Shake(0.15f, 0.25f));
                    }
                    Action("boom");
                }
            }
        }
    }

    public void Action(string action)
    {
        if (my_sprite != null)
        {
            audio_source.pitch = Random.Range(0.85f, 1.15f);
            switch (action)
            {
                case "boom":
                    my_sprite.sprite = sprite_to_boom;
                    activated = false;
                    audio_source.clip = explode_sound;
                    break;

                case "defuse":
                    my_sprite.sprite = sprite_to_diffuse;
                    activated = false;
                    audio_source.clip = defuse_sound;
                    break;
            }
            audio_source.Play();
        }
    }

    public IEnumerator DestroyByDistance(float time)
    {
        yield return new WaitForSeconds(time);

        if (Vector2.Distance(transform.position, player.transform.position) > 6f && player.transform.position.x > transform.position.x)
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(DestroyByDistance(1f));
        }
    }
}
*/