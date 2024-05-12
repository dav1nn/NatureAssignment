using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SnailManager : MonoBehaviour
{
    public GameObject snailPrefab;
    public TMP_InputField snailInputField;
    public Button addButton;
    public Button removeButton;

    private List<GameObject> snails = new List<GameObject>();

    void Start()
    {
        addButton.onClick.AddListener(() => ModifySnailCount(1));
        removeButton.onClick.AddListener(() => ModifySnailCount(-1));
        snailInputField.onEndEdit.AddListener(delegate { ValidateInput(); });

        InitializeSnails();
    }

    void InitializeSnails()
    {
        ModifySnailCount(1);
    }

    void ModifySnailCount(int increment)
    {
        int newCount = Mathf.Max(0, snails.Count + increment);
        UpdateSnailCount(newCount);
        UpdateSnailCountDisplay();
    }

    void ValidateInput()
    {
        if (int.TryParse(snailInputField.text, out int inputCount))
        {
            inputCount = Mathf.Max(0, inputCount);  
            UpdateSnailCount(inputCount);
        }
        UpdateSnailCountDisplay();
    }

    void UpdateSnailCount(int newCount)
    {
        int currentCount = snails.Count;

        if (newCount > currentCount)
        {
            for (int i = currentCount; i < newCount; i++)
            {
                GameObject snail = Instantiate(snailPrefab, GetRandomPosition(), Quaternion.identity);
                snails.Add(snail);
            }
        }
        else if (newCount < currentCount)
        {
            for (int i = currentCount - 1; i >= newCount; i--)
            {
                if (snails.Count > 0 && i < snails.Count)
                {
                    GameObject snailToRemove = snails[i];
                    snails.RemoveAt(i);
                    Destroy(snailToRemove);
                }
            }
        }
    }

    void UpdateSnailCountDisplay()
    {
        snailInputField.text = snails.Count.ToString();
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-5, 5), 0.25f, Random.Range(-5, 5));
    }
}
