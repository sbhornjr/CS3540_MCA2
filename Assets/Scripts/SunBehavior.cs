using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBehavior : MonoBehaviour
{

    public float speed = 2f;
    public float jumpAmount = 3f;
    public AudioClip jumpSFX;

    Rigidbody rb;
    float sinceLastTeleported = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        sinceLastTeleported += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!LevelManager.isGameOver)
        {
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");

            Vector3 forceVector = new Vector3(moveH, 0.0f, moveV);

            rb.AddForce(forceVector * speed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (transform.position.y <= 0.75) {
                    rb.AddForce(0, jumpAmount, 0, ForceMode.Impulse);
                    AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position);
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<LevelManager>().LevelLost();
            Destroy(gameObject);
        }
    }

    public void Teleport(GameObject wormhole)
    {
        if (sinceLastTeleported >= 2)
        {
            transform.position = wormhole.transform.position;
            sinceLastTeleported = 0;
        }
    }
}
