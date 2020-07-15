using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetWMoonBehavior : MonoBehaviour
{
    public static int planetWMoonCount = 0;
    public AudioClip pickupSFX;
    public float rotationAmount = 45f;
    public int scoreValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        planetWMoonCount += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver) planetWMoonCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);

            FindObjectOfType<LevelManager>().IncreaseScore(scoreValue);

            transform.DetachChildren();

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!LevelManager.isGameOver)
        {
            planetWMoonCount--;
        }
    }
}
