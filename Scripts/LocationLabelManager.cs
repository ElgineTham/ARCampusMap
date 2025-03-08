using UnityEngine;
using TMPro;
using Virtence.VText;
using Virtence.VText.LEGACY;

public class LocationLabelManager : MonoBehaviour
{
    public Transform userTransform; // AR Camera
    public GameObject vtextPrefab; // The 3D text prefab from VText Pro
    public Transform[] locations; // List of buildings
    public string[] locationNames; // Corresponding names
    private GameObject[] labels; // Store instantiated labels

    private float detectionRadius = 25.0f;

    void Start()
    {
        labels = new GameObject[locations.Length];

        for (int i = 0; i < locations.Length; i++)
        {
            // Instantiate VText Pro 3D text at each location
            labels[i] = Instantiate(vtextPrefab, locations[i].position + new Vector3(0, 2, 0), Quaternion.identity);

            // Set the text dynamically
            VTextInterface vtext = labels[i].GetComponent<VTextInterface>();
            if (vtext != null)
            {
                vtext.RenderText = locationNames[i]; // Change text to match location
            }

            labels[i].SetActive(false); // Start hidden
        }
    }

    void Update()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            float distance = Vector3.Distance(userTransform.position, locations[i].position);

            if (distance < detectionRadius)
            {
                labels[i].SetActive(true);
            }
            else
            {
                labels[i].SetActive(false);
            }
        }
    }
}

