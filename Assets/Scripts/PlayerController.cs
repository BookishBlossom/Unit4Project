using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables
    private Rigidbody2D playerRb;
    public float speed;
    public float jumpForce;
    public bool isOnGround = true;
    public bool hasPowerup;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move
        float forwardInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(Vector2.right * speed * forwardInput);

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
            {
                Rigidbody2D enemyRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

                Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode2D.Impulse);
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

}
