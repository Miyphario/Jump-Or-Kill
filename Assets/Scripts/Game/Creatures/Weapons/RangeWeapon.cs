using System.Collections;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private bool _isBullet = false;
    [SerializeField] private float _bulletSpeed = 3f;
    [SerializeField] private float _bullRotateSpeed = 6f;
    [SerializeField] private int _currentLine = 2;

    private bool _isPlayer = false;

    private void Start()
    {
        if (_isBullet)
        {
            StartCoroutine(DestroyByDistance());
        }

        if (_myCreature.transform.TryGetComponent<ThisIsPlayer>(out var _))
            _isPlayer = true;
    }

    public override void Attack()
    {
        if (!_canAttack) return;
        if (_isBullet) return;

        RangeWeapon bull = Instantiate(_bullet, transform.position, Quaternion.identity).transform.GetComponent<RangeWeapon>();
        bull._myCreature = _myCreature;

        // Calculate near line
        if (_myCreature.CurrentLine != _myCreature.LineToMove)
        {
            bull._currentLine = _myCreature.YToLine(_myCreature.transform.position.y);
        }
        else
        {
            bull._currentLine = _myCreature.CurrentLine;
        }

        bull._isBullet = true;
        bull.transform.rotation = _myCreature.transform.rotation;
        Rigidbody2D bRb = bull.transform.GetComponent<Rigidbody2D>();
        if (_isPlayer)
            bRb.velocity = new Vector2(_bulletSpeed, 0f);
        else
            bRb.velocity = new Vector2(-(_bulletSpeed + _myCreature.Speed.x / 10f), 0f);

        Reload();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isBullet) return;

        Creature tar;

        tar = collision.transform.GetComponent<Creature>();
        if (tar == null || tar.Die) return;

        if (tar.CurrentLine == _currentLine || tar.LineToMove == _currentLine)
        {
            bool tarKilled = tar.TakeDamage(_damage, _myCreature.gameObject);
            if (_myCreature is ThisIsPlayer pl && tarKilled) pl.AddKill();

            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (_isBullet)
        {
            transform.Rotate(new Vector3(0f, 0f, -_bullRotateSpeed));
        }
    }

    public IEnumerator DestroyByDistance()
    {
        while(_isBullet)
        {
            yield return new WaitForSeconds(2f);
            Vector2 plPos = GameController.Instance.Player.transform.position;
            if (transform.position.x < plPos.x - 5f || transform.position.x > plPos.x + 20f)
            {
                Destroy(gameObject);
            }
        }    
    }
}
