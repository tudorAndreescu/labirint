using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool flashlightEnabled;
    public GameObject flashlight;
    public GameObject spotlight;
    public float maxEnergy;
    public static float currentEnergy;

    private GameObject batteryPickedUp;
    private float usedEnergy;


   public void Start()
    {
        maxEnergy = 60 * StaticValues.GetNumberOfBatteries(); 
        currentEnergy = 60;
    }

 
    public void Update()
    {
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

        
    }

    
}
