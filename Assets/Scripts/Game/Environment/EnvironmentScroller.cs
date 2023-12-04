using UnityEngine;

public class EnvironmentScroller : MonoBehaviour
{
    private float _width;
    private Rigidbody2D _rb;
    private BoxCollider2D _collision;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collision = GetComponent<BoxCollider2D>();

        _width = _collision.size.x;
        _collision.enabled = false;
    }

    public void Init()
    {
        GameController.Instance.OnSpeedUpdated += HandleUpdateSpeed;
    }

    private void Update()
    {
        if (transform.position.x < -_width)
        {
            Vector2 resetPos = new(_width * transform.localScale.x, 0f);
            transform.position = (Vector2)transform.position + resetPos;
        }
    }

    private void HandleUpdateSpeed(float speed)
    {
        _rb.velocity = new Vector2(-speed, 0);
    }
}
