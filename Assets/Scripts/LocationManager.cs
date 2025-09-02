using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocationManager : MonoBehaviour
{
    [Header("Location Settings")]
    public List<string> locations = new List<string>() 
    { 
        "New York - United States",
        "Paris - France",
        "Tokyo - Japan",
        "London - United Kingdom",
        "Dubai - UAE",
        "Istanbul - Turkey",
        "Sydney - Australia",
        "Toronto - Canada"
    };

    [Header("UI Reference")]
    public TMP_Text locationTMP;

    [Header("Change Interval")]
    public float changeInterval = 5f; // kaç saniyede bir değişsin

    private float timer = 0f;

    void Start()
    {
        ShowRandomLocation();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            ShowRandomLocation();
            timer = 0f;
        }
    }

    void ShowRandomLocation()
    {
        if (locations.Count == 0 || locationTMP == null) return;

        int randomIndex = Random.Range(0, locations.Count);
        locationTMP.text = locations[randomIndex];
    }
}
