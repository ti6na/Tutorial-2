using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public Animator anim;
    public float speed;

    public Text winText;
    public Text countText;
    public Text livesText;

    private int count;
    private int lives;

    private bool facingRight = true;
    AudioSource winSound;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        winSound = GetComponent<AudioSource>();
        lives = 3;
        count = 0;

        winText.text = "";
        SetText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement, verMovement) * speed);
        SetText();

    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5f && !facingRight)
            {

                Flip();
                facingRight = true;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0.5f && facingRight)
            {

                Flip();
                facingRight = false;
            }
        }
        anim.SetFloat("MoveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        anim.SetFloat("MoveY", Mathf.Abs(Input.GetAxisRaw("Vertical")));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);
            count++;
            SetText();
        }

        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            lives--;
            SetText();
        }

        if (count == 4)
        {
            lives = 3;
            transform.position = new Vector2(48, 10);
            count++;
        }

        //level 2 complete: WIN
        if (count >= 8)
        {
            SetText();
            Destroy(this);
            winText.text = "You win!\n Game created by Tiana George";
            winSound.Play();
        }

        //out of lives: LOSE
        if (lives <= 0)
        {
            SetText();
            Destroy(this);
            winText.text = "You LOSE :(\nGame created by Tiana";
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    private void SetText()
    {
        countText.text = "SCORE " + count.ToString();
        livesText.text = "LIVES " + lives.ToString();
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
