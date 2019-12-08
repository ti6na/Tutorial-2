using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text endText;

    public Text livesText;

    private int scoreValue = 0;

    private int lives;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        lives = 3;
        SetScoreText();
        SetLivesText();
        endText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (Input.GetKey("escape"))
            Application.Quit();
        if (scoreValue < 4)
        {
            endText.text = "";
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreText();
            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.tag == ("Enemy"))
        {
            lives = lives - 1;
            SetLivesText();
            
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

    void SetScoreText()
    {
        score.text = scoreValue.ToString();
        if (scoreValue >= 8)
        {
            endText.text = "You win! Game Created by Tiana George";
        }

        if (scoreValue == 4)
        {
            transform.position = new Vector2(58f, -5f);
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives < 1)
        {
            Destroy(this);
            endText.text = "you lost.";
        }
    }
}
