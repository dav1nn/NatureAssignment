using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidController : MonoBehaviour
{
    public Slider speedSlider;
    public Slider neighbourDistanceSlider;
    public Slider separationDistanceSlider;
    public Slider obstacleAvoidanceDistanceSlider;
    private Boid[] boids;

    void Start()
    {
        boids = FindObjectsOfType<Boid>();

        if (boids.Length == 0)
        {
            Debug.LogError("No boids found in scene");
        }

        
        if (speedSlider != null)
            speedSlider.onValueChanged.AddListener(OnSpeedChanged);
        if (neighbourDistanceSlider != null)
            neighbourDistanceSlider.onValueChanged.AddListener(OnNeighbourDistanceChanged);
        if (separationDistanceSlider != null)
            separationDistanceSlider.onValueChanged.AddListener(OnSeparationDistanceChanged);
        if (obstacleAvoidanceDistanceSlider != null)
            obstacleAvoidanceDistanceSlider.onValueChanged.AddListener(OnObstacleAvoidanceDistanceChanged);

        
        if (boids.Length > 0)
        {
            speedSlider.value = boids[0].speed;
            neighbourDistanceSlider.value = boids[0].neighbourDistance;
            separationDistanceSlider.value = boids[0].separationDistance;
            obstacleAvoidanceDistanceSlider.value = boids[0].obstacleAvoidanceDistance;
        }
    }

    public void OnSpeedChanged(float value)
    {
        foreach (Boid boid in boids)
            boid.speed = value;
    }

    public void OnNeighbourDistanceChanged(float value)
    {
        foreach (Boid boid in boids)
            boid.neighbourDistance = value;
    }

    public void OnSeparationDistanceChanged(float value)
    {
        foreach (Boid boid in boids)
            boid.separationDistance = value;
    }

    public void OnObstacleAvoidanceDistanceChanged(float value)
    {
        foreach (Boid boid in boids)
            boid.obstacleAvoidanceDistance = value;
 
    }
    public void RefreshBoidList()
{
    boids = FindObjectsOfType<Boid>();
    Debug.Log("Boid list updated. Count: " + boids.Length);
}

}




