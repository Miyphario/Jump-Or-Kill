using System.Collections;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    protected int _currentLine = 2;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private bool _inQueue;
    public bool InQueue => _inQueue;

    private Rigidbody2D _rb;
    private AudioSource _audioSource;

    public virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        StartCoroutine(AddToQueueByDistance());

        _audioSource = GetComponent<AudioSource>();
    }

    public void Init(int line)
    {
        _currentLine = line;
        SetSprite();
        UpdateSpeed();
    }

    public void UpdateSpeed()
    {
        if (!_inQueue)
        {
            _rb.velocity = new Vector2(-GameController.Instance.PlayerSpeed, 0);
        }
    }

    public virtual void SetSprite() { }

    public virtual void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public void AddToQueue()
    {
        _inQueue = true;
        _rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(-20f, 0f);
        StopAllCoroutines();
    }

    public virtual void SetFromQueue(Vector3 position, int line)
    {
        _inQueue = false;
        _currentLine = line;
        transform.position = position;
        UpdateSpeed();
        StartCoroutine(AddToQueueByDistance());
    }

    public void AudioPlay(AudioClip clip, float pitch, float volume)
    {
        _audioSource.clip = clip;
        _audioSource.pitch = pitch;
        _audioSource.volume = volume;
        _audioSource.Play();
    }

    public IEnumerator AddToQueueByDistance()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Vector2 plPos = GameController.Instance.Player.transform.position;
            if (transform.position.x < plPos.x - 5f)
            {
                AddToQueue();
            }
        }
    }
}
