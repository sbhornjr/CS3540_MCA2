using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehavior : MonoBehaviour
{

    public static int planetCount = 0;
    public AudioClip pickupSFX;
    public float rotationAmount = 45f;
    public int scoreValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        planetCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver) planetCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);

            FindObjectOfType<LevelManager>().IncreaseScore(scoreValue);

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!LevelManager.isGameOver)
        {
            planetCount--;

            if (planetCount <= 0)
            {
                FindObjectOfType<LevelManager>().LevelWon();
            }
        }
    }
}
