using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachTurretController : MonoBehaviour
{
    [SerializeField] Gun gunL;
    [SerializeField] Gun gunR;
    [SerializeField] float energyRefillSpeed = 1f;
    [SerializeField, Range(0, 1f)] private float energyCostPerBullet = 0.1f;

    float energy = 1f;

    bool shootLeft;
    UIManager uiM;

    private void Start()
    {
        uiM = UIManager.Instance;
    }

    private void Update()
    {
        EnergyRefill();
        ShootDetection();
    }

    public void Shoot()
    {
        GetGun.Shoot();
    }

    private void ShootDetection ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TrySpendEnergy())
            {
                shootLeft = !shootLeft;
                Shoot();
            }
        }
    }

    #region Energy
    private void EnergyRefill()
    {
        ChangeEnergyAmount(Time.deltaTime * energyRefillSpeed);
    }

    private bool TrySpendEnergy()
    {
        if (energy > energyCostPerBullet)
        {
            ChangeEnergyAmount(-energyCostPerBullet);
            return true;
        }
        return false;
    }

    private void ChangeEnergyAmount(float change)
    {
        energy += change;
        energy = Mathf.Clamp(energy, 0f, 1f);
        uiM.SetEnergy(energy);
    }
    #endregion

    private Gun GetGun => shootLeft ? gunL : gunR;
}
