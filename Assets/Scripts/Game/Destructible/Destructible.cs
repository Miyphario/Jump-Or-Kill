using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;
    private int _currentLine;
    protected bool _activated = true;

    private float _speedX;
    private float _inputX;
    protected Creature _myEnemy;
    private bool _inQueue;
    public bool InQueue => _inQueue;

    private SkinFunctions _skin;
    private Rigidbody2D _rb;

    protected SpriteRenderer _mySprite;
    protected AudioSource _audioSource;

    protected virtual void Awake()
    {
        _mySprite = transform.Find("Sprite").transform.GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _skin = _mySprite.transform.GetComponent<SkinFunctions>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(int line, Creature myEnemy)
    {
        _currentLine = line;
        _myEnemy = myEnemy;
    }

    protected virtual void Start()
    {
        StartCoroutine(AddToQueueByDistance());
        UpdateSpeed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_activated)
        {
            Creature e = collision.GetComponent<Creature>();
            if (e == _myEnemy)
            {
                if (e.CurrentLine == _currentLine || e.LineToMove == _currentLine)
                {
                    e.TakeDamage(_damage, gameObject);
                    Action("boom", e);
                }
            }
        }
    }

    public virtual void Action(string action, Creature creature)
    {
        
    }

    public void UpdateSpeed()
    {
        if (!_inQueue)
        {
            _speedX = -GameController.Instance.PlayerSpeed;
            _rb.velocity = new Vector2(_speedX, _rb.velocity.y);
        }
    }

    private void AddToQueue()
    {
        _inQueue = true;
        _inputX = 0f;
        _rb.velocity = new Vector2(0f, 0f);
        transform.position = new Vector3(-20f, 0f, 0f);
        _mySprite.sortingOrder = -1;
        StopAllCoroutines();
    }

    public virtual void SetFromQueue(Vector3 position, int line)
    {
        _currentLine = line;
        transform.position = position;
        _activated = true;

        _inQueue = false;

        StartCoroutine(AddToQueueByDistance());
        UpdateSpeed();
    }

    private IEnumerator AddToQueueByDistance()
    {
        while (!_inQueue)
        {
            Vector2 plPos = GameController.Instance.Player.transform.position;
            if (transform.position.x < plPos.x - 5f)
            {
                AddToQueue();
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
