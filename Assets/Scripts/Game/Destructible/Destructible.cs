using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private int _currentLine;
    [SerializeField] private float _damage = 1f;
    [SerializeField] protected bool _activated = true;

    [SerializeField] private float _speedX;
    [SerializeField] private float _inputX;
    [SerializeField] protected Creature _myEnemy;
    [SerializeField] private bool _inQueue;
    public bool InQueue => _inQueue;

    [SerializeField] private SkinFunctions _skin;
    private Rigidbody2D _rb;

    protected SpriteRenderer _mySprite;
    protected AudioSource _audioSource;

    public virtual void Awake()
    {
        _mySprite = transform.Find("Sprite").transform.GetComponent<SpriteRenderer>();
        _audioSource = transform.GetComponent<AudioSource>();

        _skin = _mySprite.transform.GetComponent<SkinFunctions>();
        _skin.AddSpriteToList(_mySprite);

        _rb = transform.GetComponent<Rigidbody2D>();
    }

    public void Init(int ZOrder)
    {
        _skin.ChangeZOrder(ZOrder);
    }

    public void Init(int ZOrder, int line, Creature myEnemy)
    {
        _currentLine = line;
        _myEnemy = myEnemy;
        Init(ZOrder);
    }

    public virtual void Start()
    {
        StartCoroutine(AddToQueueByDistance());
        UpdateSpeed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_activated)
        {
            Creature e = collision.transform.GetComponent<Creature>();
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

    public void AddToQueue()
    {
        _inQueue = true;
        _inputX = 0f;
        _rb.velocity = new Vector2(0f, 0f);
        transform.position = new Vector3(-20f, 0f, 0f);
        _mySprite.sortingOrder = -1;
        _skin.SetDefaultZOrder();
        StopAllCoroutines();
    }

    public virtual void SetFromQueue(Vector3 position, int line)
    {
        _currentLine = line;
        transform.position = position;
        _activated = true;

        //if (_skin._ZOrder > -1)
        //{
        //    _skin.ChangeZOrder(_skin._ZOrder);
        //}

        _inQueue = false;

        StartCoroutine(AddToQueueByDistance());
        UpdateSpeed();
    }

    public IEnumerator AddToQueueByDistance()
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
