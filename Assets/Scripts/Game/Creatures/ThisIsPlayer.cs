using UnityEngine;

public class ThisIsPlayer : Creature
{
    private int _kills;
    public int Kills => _kills;

    public override void Awake()
    {
        base.Awake();
        Health = 3;
        CreateWeapon(GameController.Instance.LoadCurrentWeapon(), false);
    }

    public override void Start()
    {
        base.Start();

        _skin.AddSpriteToList(_weapon.transform.Find("Sprite").transform.GetComponent<SpriteRenderer>());
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
        ChangeLine((int)inY);

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
