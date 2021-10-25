using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerscript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public GameObject winTextObject;
    public GameObject player;
    public GameObject endTextObject;
    public Text score;
    public Text health;
    public AudioClip music;
    public AudioClip victory;
    public AudioSource musicSource;
    private int scoreValue = 0;
    private int healthValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        SetCountText();
        SetHealthText();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Coins: " + scoreValue.ToString();
        winTextObject.SetActive(false);
        endTextObject.SetActive(false);
        musicSource.clip = music;
        musicSource.Play();
        musicSource.loop = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (healthValue <= 0)
        {
            player.gameObject.SetActive(false);
            endTextObject.SetActive(true);
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector2(76.5f, 0.5f);
            rd2d = GetComponent<Rigidbody2D>();
            healthValue = 3;
        }
        if (scoreValue >= 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = victory;
            musicSource.Play();
            musicSource.loop = true;
        }
    }
    void SetCountText()
    {
        score.text = "Coins: " + scoreValue.ToString();
    }

        void SetHealthText()
    {
        health.text = "Health: " + healthValue.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "coin")
        {
            scoreValue += 1;
            score.text = "Coins: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "enemy")
        {
            healthValue -= 1;
            health.text = "Health: " + healthValue.ToString();
            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors. You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}

