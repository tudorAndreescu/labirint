using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{

    public Slider difficultySlider;
    public TextMeshProUGUI difficultyDescription;

    public void Start()
    {
        difficultySlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        StaticValues.difficulty = (int)difficultySlider.value;

        difficultyDescription.SetText(StaticValues.GetDifficultyDescription());
    }
}
