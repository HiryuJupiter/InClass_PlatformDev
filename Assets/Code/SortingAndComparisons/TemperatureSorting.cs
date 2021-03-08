using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureSorting : MonoBehaviour
{
    [SerializeField]
    private int temperatureCount = 11;

    private List<Temperature> temperatures = new List<Temperature>();

    void Start()
    {
        GenerateTemperature();
        DisplayTemperature();

        temperatures.BubbleSort();

        DisplayTemperature();
    }

    private void GenerateTemperature ()
    {
        for (int i = 0; i < temperatureCount; i++)
        {
            temperatures.Add(new Temperature(Random.Range(-100.0f, 100.0f)));
        }
    }

    void DisplayTemperature()
    {
        string formatted = "";
        foreach(Temperature temp in temperatures)
        {
            formatted += temp.ToString() + ", ";
        }

        Debug.Log(formatted);
    }

}