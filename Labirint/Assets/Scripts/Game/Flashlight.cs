using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool flashlightEnabled;
    public GameObject flashlight;
    public GameObject spotlight;
    public float maxEnergy;
    private float currentEnergy;

    private int batteries = 1;
    private GameObject batteryPickedUp;
    private float usedEnergy;


   public void Start()
    {
        maxEnergy = 60 * batteries; 
        currentEnergy = maxEnergy;
    }

 
    public void FixedUpdate()
    {
        maxEnergy = 60 * batteries;
        currentEnergy = maxEnergy;


        //equip
        if (Input.GetKeyDown(KeyCode.F))
            flashlightEnabled =! flashlightEnabled;

        if (flashlightEnabled)
        {
            spotlight.SetActive(true);

            if (currentEnergy <= 0)
            {
                spotlight.SetActive(false);
                batteries = 0;
            }
            if (currentEnergy > 0)
            {
                spotlight.SetActive(true);
                currentEnergy -= 1f * Time.deltaTime;
                usedEnergy += 1f * Time.deltaTime;
            }

            if(usedEnergy >= 60)
            {
                batteries -= 1;
                usedEnergy = 0;
            }

        }
        else
        {
           spotlight.SetActive(false);
        }

        
    }

    
}
