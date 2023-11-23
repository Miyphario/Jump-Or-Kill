using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class MenuEnemy : MonoBehaviour
{
    public bool has_skin;

    public float jumpStr = 10f;
    private float prevJumpPos;

    public bool to_game = false;
    public GameObject play_button;

    public float speedX = 5f;
    private bool isJump = false;
    private float moveX;
    private float moveY;

    public MenuHUD my_hud;

    public GameObject my_skin;

    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        my_hud = GameObject.Find("MenuHUD").GetComponent<MenuHUD>();

        if (!has_skin)
        {
            rb = transform.GetComponent<Rigidbody2D>();

            SkinsList sk = my_hud.GetSkins();

            if (sk != null)
            {
                GameObject ls = sk.GetSelectedSkin();
                if (ls != null)
                {
                    ChangeSkin(ls);
                }
                else
                {
                    my_skin = transform.GetChild(0).gameObject;
                    anim = my_skin.transform.GetComponent<Animator>();
                }
            }
            else
            {
                my_skin = transform.GetChild(0).gameObject;
                anim = my_skin.transform.GetComponent<Animator>();
            }

            my_hud.DestroySkinsList();
        }
        else
        {
            my_skin = transform.GetChild(0).gameObject;
            anim = my_skin.transform.GetComponent<Animator>();

            MakeMaskable();
        }
    }

    public void MakeMaskable()
    {
        ChangeZOrder o = my_skin.transform.GetComponent<ChangeZOrder>();
        o.make_maskable = true;
        o.mask_outside = false;
        o.UpdateOrder();
    }

    public void Jump()
    {
        if (!has_skin)
        {
            if (!isJump)
            {
                anim.SetTrigger("Jump");
                prevJumpPos = transform.position.y;
                to_game = true;
                rb.gravityScale = 1;
                rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpStr);
                isJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!has_skin)
        {
            rb.velocity = new Vector2(rb.velocity.x + moveX * speedX * Time.deltaTime, rb.velocity.y);
        }
    }

    private void Update()
    {
        if (!has_skin)
        {
            if (to_game)
            {
                if (rb.velocity.y < 0)
                {
                    anim.SetBool("Fall", true);
                }
                else
                {
                    anim.SetBool("Fall", false);
                }

                if (Mathf.Abs(transform.position.x - play_button.transform.position.x) > 0.25f)
                {
                    moveX = play_button.transform.position.x > transform.position.x ? 1f : -1f;
                }
                else
                {
                    moveX = 0f;
                }
            }
        }
    }

    public void ChangeSkin(GameObject skin)
    {
        if (skin != null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            GameObject s = Instantiate(skin, transform.position, Quaternion.identity, transform);
            my_skin = s;

            anim = my_skin.transform.GetComponent<Animator>();

            if (has_skin)
            {
                MakeMaskable();
                AudioSource d = my_skin.transform.GetComponent<AudioSource>();
                Destroy(d);
            }
        }
    }
}
*/