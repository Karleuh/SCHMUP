using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlidersManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider energySlider;


    public void updateHealthSlider(float health, float maxHealth) {
        healthSlider.value = health/maxHealth;
    }

    public void updateEnergySlider(float energy, float maxEnergy){
        energySlider.value = energy/maxEnergy;
    }


}
