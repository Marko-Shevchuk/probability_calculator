using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calculator1 : MonoBehaviour
{
    public TMP_InputField inputA;
    public TMP_InputField inputB;
    public TMP_InputField inputNotA;
    public TMP_InputField inputNotB;
    public TMP_InputField inputAandB;
    public TMP_InputField inputAorB;
    public TMP_InputField inputAxorB;
    public TMP_InputField inputAnorB;
    public TMP_InputField outputA;
    public TMP_InputField outputB;
    public TMP_InputField outputNotA;
    public TMP_InputField outputNotB;
    public TMP_InputField outputAandB;
    public TMP_InputField outputAorB;
    public TMP_InputField outputAxorB;
    public TMP_InputField outputAnorB;
    public Button calculateButton;
    public TMP_Text errorText;
    void Start()
    {
        calculateButton.onClick.AddListener(CalculateProbability);
    }

    void CalculateProbability()
    {
        // Retrieve input values
        float a = GetValidatedProbability(inputA.text);
        float b = GetValidatedProbability(inputB.text);
        float notA = GetValidatedProbability(inputNotA.text);
        float notB = GetValidatedProbability(inputNotB.text);
        float aAndB = GetValidatedProbability(inputAandB.text);
        float aOrB = GetValidatedProbability(inputAorB.text);
        float aXorB = GetValidatedProbability(inputAxorB.text);
        float aNorB = GetValidatedProbability(inputAnorB.text);
        
        if (a == -1 | b == -1)
        {
            if (a == -1 & notA != -1) a = 1 - notA;
            if (b == -1 & notB != -1) b = 1 - notB;
            if (aOrB == -1 & aNorB != -1) aOrB = 1 - aNorB;
        }
        if (a == -1 | b == -1)
        {
           
            if (a != -1 && aAndB != -1)
            {
                b = aAndB / a;
            }
            if (b != -1 && aAndB != -1)
            {
                a = aAndB / b;
            }
            if (a != -1 && aOrB != -1)
            {
                b = (aOrB - a)/(1 - a);
            }
            if (b != -1 && aOrB != -1)
            {
                a = (aOrB - b) / (1 - b);
            }
        }
        if (a == -1 | b == -1)
        {
            outputA.text = "Failed to calculate based on given data.";
        }
        else
        {
            notA = 1 - a;
            notB = 1 - b;
            aAndB = a * b;
            aOrB = a + b - a * b;
            aXorB = a + b - 2 * a * b;
            aNorB = 1 - aOrB;
        }

        outputA.text = a.ToString();
        outputB.text = b.ToString();
        outputNotA.text = notA.ToString();
        outputNotB.text = notB.ToString();
        outputAandB.text = aAndB.ToString();
        outputAorB.text = aOrB.ToString();
        outputAxorB.text = aXorB.ToString();
        outputAnorB.text = aNorB.ToString();
    }

    float GetValidatedProbability(string input)
    {
        float value = 0;
        if (float.TryParse(input, out value))
        {
            if ((value >= 0) & (value <= 1))
            {
                return value;
            }
        }
        errorText.text = "Input value not a number or not within [0,1].";
        return -1;
    }
}
