using UnityEngine;

public class EnvironmentScroller : MonoBehaviour
{
    private BoxCollider2D _collision;
    private Rigidbody2D _rb;

    private float _width;

    private void Start()
    {
        _collision = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();

        _width = _collision.size.x;
        _collision.enabled = false;

        UpdateSpeed();
    }

    private void Update()
    {
        if (transform.position.x < -_width)
        {
            Vector2 resetPos = new Vector2(_width * transform.localScale.x, 0f);
            transform.position = (Vector2)transform.position + resetPos;
        }
    }

    public void UpdateSpeed()
    {
        _rb.velocity = new Vector2(-GameController.Instance.PlayerSpeed, 0);
    }
}
