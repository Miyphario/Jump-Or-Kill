using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] protected float _damage = 1f;
    [SerializeField] protected bool _damageToFriend;
    [SerializeField] protected float _reloadSpeed = 1f;
    [SerializeField] protected float _attackRange = 0.7f;
    public float AttackRange => _attackRange;
    [SerializeField] protected bool _canAttack = true;
    public bool CanAttack => _canAttack;
    [SerializeField] protected float _attackDistance = 2.5f;
    public float AttackDistance => _attackDistance;
    protected Creature _myCreature;
    [SerializeField] protected Sprite _skin;

    [SerializeField] protected bool _rightArm;
    public bool RightArm => _rightArm;

    [Header("Shop")]
    [SerializeField] protected string _weaponName;
    [SerializeField] protected string _description;
    [SerializeField] protected int _price;
    [SerializeField] protected int _currency;
    [SerializeField] protected bool _purchased;
    [SerializeField] protected bool _selected;

    private Coroutine _reloadRoutine;

    public void Init(Creature creature)
    {
        _myCreature = creature;
        _canAttack = true;
    }

    public void StartAttack()
    {
        _canAttack = false;
    }

    public abstract void Attack();

    protected void Reload()
    {
        if (_reloadRoutine != null) StopCoroutine(_reloadRoutine);
        _reloadRoutine = StartCoroutine(ReloadIE());
    }

    private IEnumerator ReloadIE()
    {
        yield return new WaitForSeconds(_reloadSpeed);
        _canAttack = true;
    }
}