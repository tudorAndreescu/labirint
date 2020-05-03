using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Flashlight : MonoBehaviour
{
    public GameObject[] batteries;
    public GameObject spotlight;
    public TextMeshProUGUI energyTimerText;
    public GameObject energyTimer;

    private bool flashlightEnabled;
    private float maxEnergy;
    private int numberOfBatteriesToDisplay = 0;
    private int maxNumberOfBatteries = 0;

    public static float currentEnergy;


   public void Start()
    {
        //for(int i = 0; i < batteries.Length; i++) batteries[i].enabled = false;
        maxEnergy = 60 * StaticValues.GetNumberOfBatteries(); 
        currentEnergy = 60;
        maxNumberOfBatteries = StaticValues.GetNumberOfBatteries();
        if (maxNumberOfBatteries == 0) maxNumberOfBatteries = 1;
        UpdateBatteries();
    }

 
    public void Update()
    {
        numberOfBatteriesToDisplay = (int)currentEnergy/60 + 1;
        UpdateBatteries();

        //equip
        if (Input.GetKeyDown(KeyCode.F))
            flashlightEnabled =! flashlightEnabled;

        if (flashlightEnabled)
        {
            spotlight.SetActive(true);

            if (currentEnergy <= 0)
            {
                spotlight.SetActive(false);
            }
            if (currentEnergy > 0)
            {
                spotlight.SetActive(true);
                currentEnergy -= 1f * Time.deltaTime;
            }

        }
        else
        {
           spotlight.SetActive(false);
        }

        if (currentEnergy <= 15) {
            energyTimer.SetActive(true);
            energyTimerText.SetText($"{(int)currentEnergy}");
        } else energyTimer.SetActive(false);

        
    }


    private void UpdateBatteries() {
        for(int i = 0; i < maxNumberOfBatteries; i++) {
            if (i < numberOfBatteriesToDisplay) batteries[i].SetActive(true);
            else batteries[i].SetActive(false);
        }
    } 

}
