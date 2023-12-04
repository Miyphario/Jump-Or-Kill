using System.Collections;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [Header("Range")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 3f;
    [SerializeField] private float _bullRotateSpeed = 6f;
    private int _currentLine = 2;
    private bool _isBullet = false;

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

    public void SetBullet(Creature myCreature)
    {
        _isBullet = true;
        _myCreature = myCreature;
    }

    public override void Attack()
    {
        if (_isBullet) return;

        RangeWeapon bull = Instantiate(_bulletPrefab, transform.position, Quaternion.identity).transform.GetComponent<RangeWeapon>();
        bull.SetBullet(_myCreature);

        // Calculate near line
        if (_myCreature.CurrentLine != _myCreature.LineToMove)
        {
            bull._currentLine = _myCreature.YToLine(_myCreature.transform.position.y);
        }
        else
        {
            bull._currentLine = _myCreature.CurrentLine;
        }

        bull.transform.rotation = _myCreature.transform.rotation;
        Rigidbody2D bRb = bull.GetComponent<Rigidbody2D>();
        if (_isPlayer)
            bRb.velocity = new Vector2(_bulletSpeed, 0f);
        else
            bRb.velocity = new Vector2(-(_bulletSpeed + _myCreature.Speed.x + GameController.Instance.EnemySpeedOffset), 0f);

        Reload();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isBullet) return;

        if (_isPlayer)
        {
            if (collision.CompareTag("Player")) return;
        }
        else
        {
            if (collision.CompareTag("Enemy")) return;
        }

        if (!collision.TryGetComponent<Creature>(out var tar)) return;

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
