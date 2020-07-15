using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBehavior : MonoBehaviour
{

    public float speed = 2f;
    public Transform player;
    public AudioClip hitSFX;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver && Vector3.Distance(transform.position, player.position) <= 100)
        {
            transform.Rotate(Vector3.forward, 360 * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position);

            gameObject.GetComponent<Animator>().SetTrigger("EnemyDied");

            Destroy(gameObject, 1);
        }
    }
}
