using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BirdManager : MonoBehaviour
{
    public GameObject birdPrefab;
    public Transform[] landingSpots;
    private List<GameObject> birds = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5, 5), Random.Range(1, 10), Random.Range(-5, 5));
            GameObject bird = Instantiate(birdPrefab, randomPosition, Quaternion.identity);
            birds.Add(bird);
        }

        StartCoroutine(BirdBehavior());
    }

    IEnumerator BirdBehavior()
    {
        while (true)
        {
            foreach (GameObject bird in birds)
            {
                if (Random.value < 0.1f)  
                {
                    Vector3 landingSpot = landingSpots[Random.Range(0, landingSpots.Length)].position;
                    bird.transform.position = landingSpot;
                    bird.GetComponent<Boid>().enabled = false;
                    bird.GetComponent<Rigidbody>().isKinematic = true;

                    yield return new WaitForSeconds(Random.Range(2, 5));  

                    bird.GetComponent<Boid>().enabled = true;
                    bird.GetComponent<Rigidbody>().isKinematic = false;
                }
            }

            yield return new WaitForSeconds(1f);  
        }
    }
}
