using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Pickable : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    protected int _currentLine = 2;
    private bool _inQueue;
    public bool InQueue => _inQueue;

    private Rigidbody2D _rb;
    private AudioSource _audioSource;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(AddToQueueByDistance());
    }

    public void Init(int line)
    {
        _currentLine = line;
        SetSprite();
        SetFromQueue();
    }

    public void UpdateSpeed()
    {
        if (!_inQueue)
        {
            _rb.velocity = new Vector2(-GameController.Instance.PlayerSpeed, 0);
        }
    }

    protected virtual void SetSprite() { }

    protected virtual void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    protected void AddToQueue()
    {
        _inQueue = true;
        _rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(-20f, 0f);
        StopAllCoroutines();
    }

    public virtual void SetFromQueue(Vector3 position, int line)
    {
        _currentLine = line;
        transform.position = position;
        SetFromQueue();
    }

    private void SetFromQueue()
    {
        _inQueue = false;
        UpdateSpeed();
        StartCoroutine(AddToQueueByDistance());
    }

    protected void AudioPlay(AudioClip clip, float pitch, float volume)
    {
        _audioSource.clip = clip;
        _audioSource.pitch = pitch;
        _audioSource.volume = volume;
        _audioSource.Play();
    }

    private IEnumerator AddToQueueByDistance()
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
