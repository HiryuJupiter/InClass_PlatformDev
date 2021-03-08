using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Image energyBar;

    public void SetEnergy (float perc)
    {
        energyBar.fillAmount = perc;
    }

    private void Awake()
    {
        Instance = this;
    }
}