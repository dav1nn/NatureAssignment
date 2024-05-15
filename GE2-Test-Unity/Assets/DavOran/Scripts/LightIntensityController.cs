using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightIntensityController : MonoBehaviour
{
    public Light directionalLight; 
    public Slider intensitySlider; 

    void Start()
    {
        
        if (intensitySlider != null && directionalLight != null)
        {
            intensitySlider.value = directionalLight.intensity;
            intensitySlider.onValueChanged.AddListener(UpdateLightIntensity);
        }
    }

    
    public void UpdateLightIntensity(float value)
    {
        if (directionalLight != null)
        {
            directionalLight.intensity = value;
        }
    }
}

