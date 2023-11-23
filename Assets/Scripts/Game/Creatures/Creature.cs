using UnityEngine;

public class Creature : MonoBehaviour
{
    public float MaxHealth { get; protected set; } = 10f;
    public float Health { get; protected set; } = 1f;

    public bool Die { get; protected set; }
    public bool God { get; protected set; }

    [SerializeField] private LayerMask _attackLayers;
    public LayerMask AttackLayers => _attackLayers;

    public Vector2 MaxSpeed { get; protected set; } = new(3f, 3f);
    public Vector2 Speed { get; protected set; } = new(0f, 7f);
    public float JumpHeight { get; protected set; } = 15f;

    protected Vector2 _input;
    public bool DisabledInput { get; protected set; }
    public bool InAir { get; protected set; }

    public int CurrentLine { get; protected set; } = 2;
    public int LineToMove { get; protected set; } = 2;
    public bool CanChangeLine { get; protected set; } = true;

    protected SkinFunctions _skin;
    public SkinFunctions Skin => _skin;
    protected Weapon _weapon;

    [SerializeField] private Transform _floor;
    protected Rigidbody2D _rb;
    protected Animator _animator;
    protected GameObject _myShadow;

    public virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        UpdateSkin();
    }

    public virtual void Start()
    {
        CreateShadow();
    }

    // Needs if player button down or enemy start attack
    public void StartAttack()
    {
        if (DisabledInput || Die || InAir || !_weapon.CanAttack) return;

        _weapon.StartAttack();
        _animator.SetTrigger("Attack");
    }

    private void UpdateSkin()
    {
        _skin = transform.GetChild(0).transform.GetComponent<SkinFunctions>();
        _skin.Init(this);
        _animator = _skin.transform.GetComponent<Animator>();
    }
    private void CreateShadow()
    {
        _myShadow = Instantiate(PrefabManager.Instance.CreatureShadow, new Vector3(transform.position.x, transform.position.y - 0.85f, transform.position.z), Quaternion.identity);
    }

    public void Attack()
    {
        if (DisabledInput || Die || _weapon == null) return;
        _weapon.Attack();
    }

    public void CreateWeapon(GameObject weaponToCreate, bool update)
    {
        if (weaponToCreate == null) return;

        if (update)
        {
            if (_weapon != null)
            {
                Destroy(_weapon.gameObject);
            }
        }

        Weapon tWp = weaponToCreate.transform.GetComponent<Weapon>();

        // Get hand transform
        Transform hand = tWp.RightArm ? _skin.RightArm : _skin.LeftArm;
        GameObject wpOb = Instantiate(weaponToCreate, hand);

        _weapon = wpOb.GetComponent<Weapon>();
        _weapon.Init(this);
    }

    // Take damage return true if creature die
    public virtual bool TakeDamage(float damage, GameObject attacker)
    {
        if (God || Die) return false;

        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);

        // Create hit particles
        Instantiate(PrefabManager.Instance.HitParticles, transform.position, Quaternion.Euler(-90f, 0f, 0f));

        if (Health <= 0)
        {
            Die = true;
            DestroyMe();
            return true;
        }

        return false;
    }

    public virtual void DestroyMe()
    {
        /*
        Destroy(myShadow);
        Destroy(gameObject);
        */
    }

    public virtual void Jump()
    {
        if (InAir || CurrentLine != LineToMove) return;

        _animator.SetBool("Walk", false);
        _animator.SetTrigger("Jump");
        _rb.gravityScale = 1f;
        InAir = true;
        //prevJumpPos = CalcLineToY(currentLine);
        _floor.parent = null;
        _rb.velocity = new(_rb.velocity.x, Vector2.up.y * JumpHeight);
    }

    public virtual void ChangeLine(int line)
    {
        // Variable "line" is how many line do you want to add or remove

        if (DisabledInput || Die | InAir || CurrentLine != LineToMove) return;
        if (line == 0) return;

        AudioClip clip = AudioManager.Instance.ChangeLine;
        float pitch = Random.Range(0.9f, 1.1f);
        float volume = 0.3f;

        // Move down
        if (line > 0)
        {
            if (CurrentLine + line > 3) return;

            LineToMove = CurrentLine + line;
            _input.y = -1f;
        }

        // Move up
        else if (line < 0)
        {
            if (CurrentLine + line < 1) return;

            LineToMove = CurrentLine + line;
            _input.y = 1f;
        }

        _skin.AudioPlay(clip, pitch, volume);
    }

    public virtual void Update()
    {
        if (!DisabledInput && !Die)
        {
            if (!InAir)
            {
                _animator.SetBool("Walk", true);

                if (CurrentLine != LineToMove)
                {
                    float posY = LineToY(LineToMove);
                    _rb.position = new(_rb.position.x, Mathf.MoveTowards(_rb.position.y, posY, Speed.y * Time.deltaTime));
                    if (Mathf.Abs(_rb.position.y - posY) <= 0.1f)
                    {
                        _input.y = 0f;
                        _rb.position = new(_rb.position.x, posY);
                        CurrentLine = LineToMove;
                    }
                }

                //if (CurrentLine != LineToMove)
                //{
                //    float posY = CalcLineToY(LineToMove);

                //    // If distance to lineToMove <= value
                //    if (Mathf.Abs(_rb.position.y - posY) <= 0.1f)
                //    {
                //        // Stopping
                //        inputY = 0f;
                //        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
                //        _rb.position = new Vector3(_rb.position.x, posY);
                //        currentLine = lineToMove;
                //    }
                //}
            }
            else
            {
                float posY = LineToY(CurrentLine);
                _floor.position = new(transform.position.x, posY);

                if (_rb.velocity.y <= 0f)
                {
                    // Check distance to last line position and if it <= value creature is grounded
                    _animator.SetBool("Fall", true);
                }
            }
        }

        // Shadow position
        Vector3 shPos = new(transform.position.x, transform.position.y - 0.85f, transform.position.z);
        if (InAir)
        {
            shPos.y = LineToY(CurrentLine) - 0.85f;
        }

        _myShadow.transform.position = shPos;
    }

    public virtual void FixedUpdate()
    {
        if (DisabledInput || Die) return;

        _rb.velocity = new(_input.x * Speed.x * Time.fixedDeltaTime, _rb.velocity.y);

        //if (CurrentLine == LineToMove) return;

        //float posY = LineToY(LineToMove);
        //if (Mathf.Abs(_rb.position.y - posY) > 0.05f)
        //{
        //    _rb.position = new(_rb.position.x, Mathf.MoveTowards(_rb.position.y, posY, Speed.y * Time.fixedDeltaTime));
        //}
        //else
        //{
        //    _input.y = 0f;
        //    _rb.position = new(_rb.position.x, posY);
        //    CurrentLine = LineToMove;
        //}

        //rb.velocity = new Vector2(rb.velocity.x + inputX * speedX * Time.deltaTime, rb.velocity.y);

        //if (inputY != 0f && !isJump)
        //{
        //    _rb.position = new Vector3(_rb.position.x, _rb.position.y + inputY * speedY * Time.deltaTime);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collider tag: {collision.collider.tag}");

        if (collision.collider.CompareTag("Floor"))
        {
            if (InAir)
            {
                float posY = LineToY(CurrentLine);
                _rb.velocity = new Vector2(_rb.velocity.x, 0f);
                _rb.position = new Vector3(_rb.position.x, posY);

                _animator.SetBool("Fall", false);
                _animator.SetTrigger("Grounded");

                _rb.gravityScale = 0f;
                _floor.parent = transform;
                _floor.localPosition = new();

                InAir = false;

                AudioClip clip = AudioManager.Instance.CreatureOnGrounded;
                float pitch = Random.Range(0.85f, 1.15f);
                float vol = 0.6f;
                _skin.AudioPlay(clip, pitch, vol);
            }
        }
    }

    public float LineToY(int line)
    {
        return line switch
        {
            1 => GameController.Instance.RoadYs[0],
            2 => GameController.Instance.RoadYs[1],
            3 => GameController.Instance.RoadYs[2],
            _ => 0f,
        };
    }

    public int YToLine(float posY)
    {
        float diff = float.MaxValue;
        int curLine = 0;

        int i = 1;
        foreach (float p in GameController.Instance.RoadYs)
        {
            float dist = Mathf.Abs(p - posY);
            if (dist < diff)
            {
                curLine = i;
                diff = dist;
            }

            i++;
        }

        return curLine;
    }

    public void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        if (_weapon != null && _skin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_skin.AttackPoint.position, _weapon.AttackRange);
        }
    }
}