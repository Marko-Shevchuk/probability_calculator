using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using WebGLSupport;


public class PipeCalculator : MonoBehaviour
{
    public TMP_InputField timeField;
    public TMP_InputField densityField;
    public TMP_InputField corrosionField;
    public TMP_InputField strength1Field;
    public TMP_InputField strength2Field;

    public TMP_Text errorText;

    public TMP_InputField outputMassField;
    public TMP_InputField outputDecreaseField;

    public Button calculateButton;
    void Start()
    {
        calculateButton.onClick.AddListener(CalculateMassDecrease);
    }

    void CalculateMassDecrease()
    {
        double time = GetValidated(timeField.text);
        double density = GetValidated(densityField.text);
        double corrosion = GetValidated(corrosionField.text);
        double strength1 = GetValidated(strength1Field.text);
        double strength2 = GetValidated(strength2Field.text);

        if (time < 0 || density < 0 || corrosion < 0 || strength1 < 0 || strength2<0)
        {
            errorText.text = "Invalid input. Please enter values over 0.";
            return;
        }
        time = time * 8765.81277;
        double massloss = corrosion * time / 1000;
        double thickloss = massloss / density / 1000;
        outputMassField.text = massloss.ToString("F6");
        outputDecreaseField.text = thickloss.ToString("F6");
        
    }
    double GetValidated(string input)
    {
        double value = 0;
        if (string.IsNullOrEmpty(input))
        {
            return -1;
        }

        if (double.TryParse(input, out value))
        {
            if ((value >= 0))
            {
                return value;
            }
        }
        errorText.text = "Input value not a number or not more than 0.";
        return -2;
    }

}
