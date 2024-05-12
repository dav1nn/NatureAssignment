using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class SnailManager : MonoBehaviour
{
    public GameObject snailPrefab;
    public Transform snailParent;
    public TMP_InputField snailCountInputField;
    public List<GameObject> snails = new List<GameObject>();

    void Start()
    {
        UpdateSnailCount(0);
    }

    public void AddSnail()
    {
        int count = int.Parse(snailCountInputField.text);
        count++;
        UpdateSnailCount(count);
    }

    public void RemoveSnail()
    {
        int count = int.Parse(snailCountInputField.text);
        if (count > 0) count--;
        UpdateSnailCount(count);
    }

    private void UpdateSnailCount(int count)
    {
        snailCountInputField.text = count.ToString();

        // Adjust the number of snails in the scene
        while (snails.Count < count)
        {
            GameObject newSnail = Instantiate(snailPrefab, RandomNavMeshLocation(20f), Quaternion.identity, snailParent);
            snails.Add(newSnail);
        }
        while (snails.Count > count)
        {
            Destroy(snails[snails.Count - 1]);
            snails.RemoveAt(snails.Count - 1);
        }
    }

    public Vector3 RandomNavMeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += Vector3.zero;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
