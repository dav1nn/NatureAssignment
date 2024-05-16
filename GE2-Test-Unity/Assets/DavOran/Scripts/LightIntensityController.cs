using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightIntensityController : MonoBehaviour
{
    public Light directionalLight;
    public Slider intensitySlider;
    public Light[] additionalLights;

    public Material lightSkybox;
    public Material darkSkybox;

    void Start()
    {
        if (intensitySlider != null && directionalLight != null)
        {
            intensitySlider.value = directionalLight.intensity;
            intensitySlider.onValueChanged.AddListener(UpdateLightAndSkybox);
        }

        UpdateAdditionalLights(intensitySlider.value);
        UpdateSkybox(intensitySlider.value);
    }

    public void UpdateLightAndSkybox(float value)
    {
        if (directionalLight != null)
        {
            directionalLight.intensity = value;
        }

        UpdateAdditionalLights(value);
        UpdateSkybox(value);
    }

    private void UpdateAdditionalLights(float sliderValue)
    {
        bool turnOn = sliderValue < 0.5f;

        foreach (Light light in additionalLights)
        {
            light.enabled = turnOn;
        }
    }

    private void UpdateSkybox(float sliderValue)
    {
        if (sliderValue < 0.5f)
        {
            RenderSettings.skybox = darkSkybox;
        }
        else
        {
            RenderSettings.skybox = lightSkybox;
        }

        DynamicGI.UpdateEnvironment();
    }
}
