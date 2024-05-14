using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidController : MonoBehaviour
{
    public Slider speedSlider;
    public Slider neighborDistanceSlider;
    public Slider separationDistanceSlider;
    private Boid[] boids;

    void Start()
    {
        boids = FindObjectsOfType<Boid>();
        speedSlider.onValueChanged.AddListener(OnSpeedChanged);
        neighborDistanceSlider.onValueChanged.AddListener(OnNeighborDistanceChanged);
        separationDistanceSlider.onValueChanged.AddListener(OnSeparationDistanceChanged);
    }

    void OnSpeedChanged(float value)
    {
        foreach (Boid boid in boids)
        {
            boid.speed = value;
        }
    }

    void OnNeighborDistanceChanged(float value)
    {
        foreach (Boid boid in boids)
        {
            boid.neighborDistance = value;
        }
    }

    void OnSeparationDistanceChanged(float value)
    {
        foreach (Boid boid in boids)
        {
            boid.separationDistance = value;
        }
    }
}

