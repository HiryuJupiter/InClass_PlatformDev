using System.Collections;
using UnityEngine;

public class TileFeedback : MonoBehaviour
{
    [SerializeField] GameObject selectionBorder;

    public void SetTileHighlight(bool isOn)
    {
        selectionBorder.SetActive(isOn);
    }
}