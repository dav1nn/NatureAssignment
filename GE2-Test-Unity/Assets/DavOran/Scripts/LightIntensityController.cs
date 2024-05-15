using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightIntensityController : MonoBehaviour
{
    public Light directionalLight; 
     public Slider intensitySlider;  
      public Light[] additionalLights; 

    void Start()
    {
        
        if (intensitySlider != null && directionalLight != null)
        {
            intensitySlider.value = directionalLight.intensity;
            intensitySlider.onValueChanged.AddListener(UpdateLightSettings);
        }

        UpdateAdditionalLights(intensitySlider.value);
    }

    public void UpdateLightSettings(float value)
    {
        if (directionalLight != null)
        {
            directionalLight.intensity = value;
        }

        UpdateAdditionalLights(value);
    }

    private void UpdateAdditionalLights(float sliderValue)
    {
        bool turnOn = sliderValue < 0.5f;

        foreach (Light light in additionalLights)
        {
            light.enabled = turnOn;
        }
    }
}


