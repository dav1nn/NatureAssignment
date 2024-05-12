using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SnailManager : MonoBehaviour
{
    public GameObject snailPrefab;
    public TMP_InputField snailInputField;
    public Button addButton;
    public Button removeButton;

    void Start()
    {
        addButton.onClick.AddListener(() => UpdateSnailCount(1));
        removeButton.onClick.AddListener(() => UpdateSnailCount(-1));
        snailInputField.onValueChanged.AddListener(delegate { ValidateInput(); });
        UpdateSnailDisplay();
    }

    void UpdateSnailCount(int increment)
    {
        int currentCount = GetCurrentSnailCount();
        int newCount = Mathf.Max(0, currentCount + increment);  
        if (newCount != currentCount)
        {
            SetSnailCount(newCount);
        }
        UpdateSnailDisplay();
    }

    void SetSnailCount(int count)
    {
        GameObject[] snails = GameObject.FindGameObjectsWithTag("Snail");
        int currentCount = snails.Length;

        if (count > currentCount)
        {
            for (int i = currentCount; i < count; i++)
            {
                Instantiate(snailPrefab, new Vector3(Random.Range(-5, 5), 0.25f, Random.Range(-5, 5)), Quaternion.identity);
            }
        }
        else if (count < currentCount)
        {
            for (int i = count; i < currentCount; i++)
            {
                if (i < snails.Length) 
                {
                    Destroy(snails[i]);
                }
            }
        }
    }

    void ValidateInput()
    {
        if (int.TryParse(snailInputField.text, out int number))
        {
            SetSnailCount(number);
            UpdateSnailDisplay();
        }
    }

    void UpdateSnailDisplay()
    {
        snailInputField.text = GetCurrentSnailCount().ToString();
    }

    int GetCurrentSnailCount()
    {
        return GameObject.FindGameObjectsWithTag("Snail").Length;
    }
}
