using System.Collections;
using UnityEngine;

public class ThisIsEnemy : Creature
{
    [SerializeField] private GameObject _warnObject;

    private bool _inQueue;
    public bool InQueue => _inQueue;
    private bool _isRange;
    private Creature _myEnemy;

    private float _timeToChangeLine = 2f;

    public override void Awake()
    {
        base.Awake();
        _attackLayers = LayerMask.GetMask("Player");
        _input.x = -1f;
        UpdateWarning(false);
    }

    public void Init(bool isRange, bool canChangeLine)
    {
        _isRange = isRange;
        CanChangeLine = canChangeLine;
        _input.x = -1f;
    }

    public void Init(bool isRange, bool canChangeLine, int line, Creature myEnemy)
    {
        _myEnemy = myEnemy;
        CurrentLine = line;
        LineToMove = line;
        Init(isRange, canChangeLine);
    }

    public void Init(bool isRange, bool canChangeLine, int ZOrder, int line, Creature myEnemy)
    {
        _skin.ChangeZOrder(ZOrder);
        Init(isRange, canChangeLine, line, myEnemy);
    }

    public override void Start()
    {
        base.Start();

        if (_isRange)
            CreateWeapon(PrefabManager.Instance.EnemyRangeWeapon, false);
        else
            CreateWeapon(PrefabManager.Instance.EnemyMeleeWeapon, false);

        _skin.AddSpriteToList(_weapon.transform.Find("Sprite").transform.GetComponent<SpriteRenderer>());

        StartCoroutine(AddToQueueByDistance());
        StartCoroutine(CheckEnemyDistance());
        if (CanChangeLine)
            StartCoroutine(TimerChangeLine());
        UpdateSpeed();
    }

    public void AddToQueue()
    {
        _inQueue = true;
        _input.y = 0f;
        _rb.velocity = new Vector2(0f, 0f);
        transform.position = new Vector3(-20f, 0f, 0f);
        _myShadow.transform.position = transform.position;
        _skin.SetDefaultZOrder();
        Die = true;
        _animator.SetBool("Walk", false);
        UpdateWarning(false);
        StopAllCoroutines();
    }

    public void SetFromQueue(Vector3 position, int line, int ZOrder)
    {
        CurrentLine = line;
        LineToMove = line;
        Die = false;
        DisabledInput = false;
        Health = 1f;
        _animator.SetBool("Death", Die);
        _weapon.Init(this);

        _skin.SetDefaultZOrder();
        if (_isRange)
            CreateWeapon(PrefabManager.Instance.EnemyRangeWeapon, true);
        else
            CreateWeapon(PrefabManager.Instance.EnemyMeleeWeapon, true);

        _skin.RemoveLastSpriteFromList();
        _skin.AddSpriteToList(_weapon.transform.Find("Sprite").GetComponent<SpriteRenderer>());
        _skin.UpdateDefaultValues();
        _skin.ChangeZOrder(ZOrder);

        transform.position = position;

        _inQueue = false;

        StartCoroutine(AddToQueueByDistance());
        StartCoroutine(CheckEnemyDistance());

        if (CanChangeLine)
            StartCoroutine(TimerChangeLine());

        UpdateSpeed();
    }

    public void UpdateSpeed()
    {
        if (!_inQueue)
        {
            if (!Die)
            {
                Speed = new(GameController.Instance.PlayerSpeed + GameController.Instance.EnemySpeedOffset, Speed.y);
            }
            else
            {
                Speed = new(GameController.Instance.PlayerSpeed, Speed.y);
            }

            _rb.velocity = new Vector2(_input.x * Speed.x, _rb.velocity.y);
        }
    }

    public override void DestroyMe()
    {
        _input.y = 0f;
        _animator.SetBool("Death", Die);
        UpdateWarning(false);
        UpdateSpeed();
    }

    public void UpdateWarning(bool enable)
    {
        if (_warnObject.activeSelf != enable)
        {
            _warnObject.SetActive(enable);
        }
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

    public IEnumerator CheckEnemyDistance()
    {
        while (!_inQueue)
        {
            Vector3 enPos = _myEnemy.transform.position;
            Vector3 myPos = transform.position;
            float dist = Mathf.Abs(myPos.x - enPos.x);
            float rDist = _weapon.AttackDistance + Random.Range(-0.5f, 0.5f);

            if (myPos.x > enPos.x + 0.5f && dist <= rDist)
            {
                if (_myEnemy.CurrentLine == CurrentLine || _myEnemy.LineToMove == CurrentLine)
                {
                    StartAttack();
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator TimerChangeLine()
    {
        bool canChLine = true;

        while (!_inQueue && CanChangeLine)
        {
            // If enemy can change line select line to move it
            if (canChLine)
            {
                float tc = Mathf.Clamp(Random.Range(_timeToChangeLine / 1.5f, _timeToChangeLine * 1.5f), 1f, 4f);

                // Select line to move
                int mLine = 1;
                switch (CurrentLine)
                {
                    case 1:
                        float c1 = Random.Range(0f, 100f);
                        if (c1 <= 50f)
                            mLine = 1;
                        else
                            mLine = 2;
                        break;

                    case 2:
                        float c2 = Random.Range(0f, 100f);
                        if (c2 <= 50f)
                            mLine = -1;
                        else
                            mLine = 1;
                        break;

                    case 3:
                        float c3 = Random.Range(0f, 100f);
                        if (c3 <= 50f)
                            mLine = -1;
                        else
                            mLine = -2;
                        break;
                }

                float warnC = 0.7f;
                yield return new WaitForSeconds(tc - warnC);

                if (!DisabledInput && !Die)
                    UpdateWarning(true);

                yield return new WaitForSeconds(warnC);

                UpdateWarning(false);
                ChangeLine(mLine);
                canChLine = false;
            }
            // If enemy can't change line wait several seconds
            else
            {
                yield return new WaitForSeconds(2f);
                canChLine = true;
            }
        }
    }
}
