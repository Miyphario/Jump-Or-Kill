using UnityEngine;

public class ThisIsPlayer : Creature
{
    private int _kills;
    public int Kills => _kills;
    private float _timerToChangeLine;

    public override void Awake()
    {
        base.Awake();
        _attackLayers = LayerMask.GetMask("Enemy");
        Health = 3;
        CreateWeapon(GameController.Instance.LoadCurrentWeapon(), false);
    }

    public override void Start()
    {
        base.Start();

        _skin.AddSpriteToList(_weapon.transform.Find("Sprite").GetComponent<SpriteRenderer>());
        _skin.ChangeSortingLayer("Player");
    }

    public override void Update()
    {
        base.Update();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        float inY = -Input.GetAxisRaw("Vertical");
        if (_timerToChangeLine <= 0f)
        {
            if (inY != 0f)
            {
                ChangeLine((int)inY);
                _timerToChangeLine = 0.3f;
            }
        }

        if (_timerToChangeLine > 0f)
        {
            _timerToChangeLine -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            StartAttack();
        }
#endif
    }

    public override bool TakeDamage(float damage, GameObject attacker)
    {
        bool t = base.TakeDamage(damage, attacker);
        GameHUD.Instance.UpdateHearts(true);
        return t;
    }

    public override void DestroyMe()
    {
        GameController.GameOver();
    }

    public void AddKill()
    {
        _kills++;
    }
}
