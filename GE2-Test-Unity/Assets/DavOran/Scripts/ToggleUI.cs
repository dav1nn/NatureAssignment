using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleUI : MonoBehaviour
{
    public Canvas uiCanvas; 
    private Button toggleButton;
    private TextMeshProUGUI buttonText;
    private bool isCanvasVisible = false;

    void Start()
    {
        toggleButton = GetComponent<Button>();

        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        toggleButton.onClick.AddListener(ToggleCanvas);

        UpdateButtonText();
    }

    void ToggleCanvas()
    {
        isCanvasVisible = !isCanvasVisible;
        uiCanvas.gameObject.SetActive(isCanvasVisible);

        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        if (isCanvasVisible)
        {
            buttonText.text = "Close UI";
        }
        else
        {
            buttonText.text = "Open UI";
        }
    }
}
