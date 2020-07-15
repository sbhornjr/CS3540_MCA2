using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBehavior : MonoBehaviour
{

    public float speed = 100f;
    public AudioClip pickupSFX;
    public int scoreValue = 2;

    [SerializeField]
    private Transform planet;

    // Start is called before the first frame update
    void Start()
    {
        PlanetWMoonBehavior.planetWMoonCount += 1;
        planet = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (planet != null) transform.RotateAround(planet.position, Vector3.up, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (planet == null && other.gameObject.CompareTag("Player"))
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
            PlanetWMoonBehavior.planetWMoonCount--;

            if (PlanetWMoonBehavior.planetWMoonCount <= 0)
            {
                FindObjectOfType<LevelManager>().LevelWon();
            }
        }
    }
}
