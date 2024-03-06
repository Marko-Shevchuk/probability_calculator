using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Windows;

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

    public TMP_InputField probA;
    public TMP_InputField probB;
    public TMP_InputField numAN;
    public TMP_InputField numBM;
    public TMP_InputField outputANtimes;
    public TMP_InputField outputANot;
    public TMP_InputField outputBMtimes;
    public TMP_InputField outputBNot;
    public TMP_InputField outputneitherAB;
    public TMP_InputField outputbothAB;
    public TMP_InputField outputANtimesBNot;
    public TMP_InputField outputBMtimesANot;
    public TMP_InputField outputABNot;
    public TMP_InputField outputBANot;
    public Button calculateButton2;
    public TMP_Text errorText2;

    
    void Start()
    {
        calculateButton.onClick.AddListener(CalculateProbability);
        calculateButton2.onClick.AddListener(CalculateProbability2);

    }
   
    void CalculateProbability2()
    {
        // Retrieve input values
        float a = GetValidatedProbability(probA.text);
        float b = GetValidatedProbability(probB.text);

        int n;
        int m;
        if (a == -1 | b == -1)
        {
            errorText2.text = "Failed. A or B must be within [0,1] ";
            outputANtimes.text = "";
            outputANot.text = "";
            outputBMtimes.text = "";
            outputBNot.text = "";
            outputneitherAB.text = "";
            outputbothAB.text = "";
            outputANtimesBNot.text = "";
            outputBMtimesANot.text = "";
            outputABNot.text = "";
            outputBANot.text = "";
        }
        else if (int.TryParse(numAN.text, out n) & (n > 0) & int.TryParse(numBM.text, out m) & (m > 0))
        {
            // Calculate probabilities
            float ANtimes = Mathf.Pow(a, n);
            float ANot = Mathf.Pow(1 - a, n);
            float BMtimes = Mathf.Pow(b, m);
            float BNot = Mathf.Pow(1 - b, m);
            float neitherAB = Mathf.Pow(1 - a, n) * Mathf.Pow(1 - b, m);
            float bothAB = (1 - Mathf.Pow(1 - a, n)) * (1 - Mathf.Pow(1 - b, m));
            float ANtimesBNot = Mathf.Pow(a, n) * Mathf.Pow(1 - b, m);
            float BMtimesANot = Mathf.Pow(1 - a, n) * Mathf.Pow(b, m);
            float ABNot = (1 - Mathf.Pow(1 - a, n)) * Mathf.Pow(1 - b, m);
            float BANot = Mathf.Pow(1 - a, n) * (1 - Mathf.Pow(1 - b, m));

            // Display results
            outputANtimes.text = ANtimes.ToString();
            outputANot.text = ANot.ToString();
            outputBMtimes.text = BMtimes.ToString();
            outputBNot.text = BNot.ToString();
            outputneitherAB.text = neitherAB.ToString();
            outputbothAB.text = bothAB.ToString();
            outputANtimesBNot.text = ANtimesBNot.ToString();
            outputBMtimesANot.text = BMtimesANot.ToString();
            outputABNot.text = ABNot.ToString();
            outputBANot.text = BANot.ToString();
        }
        else
        {
            errorText2.text = "Failed. Number of repetitions must be integer.";
            outputANtimes.text = "";
            outputANot.text = "";
            outputBMtimes.text = "";
            outputBNot.text = "";
            outputneitherAB.text = "";
            outputbothAB.text = "";
            outputANtimesBNot.text = "";
            outputBMtimesANot.text = "";
            outputABNot.text = "";
            outputBANot.text = "";
        }


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
        if (a < 0 | b < 0)
        {

            errorText.text = "Failed to calculate based on given data.";
            outputA.text = "";
            outputB.text = "";
            outputNotA.text = "";
            outputNotB.text = "";
            outputAandB.text = "";
            outputAorB.text = "";
            outputAxorB.text = "";
            outputAnorB.text = "";
        }
        else
        {
            errorText.text = "";
            notA = 1 - a;
            notB = 1 - b;
            aAndB = a * b;
            aOrB = a + b - a * b;
            aXorB = a + b - 2 * a * b;
            aNorB = 1 - aOrB;
            outputA.text = a.ToString();
            outputB.text = b.ToString();
            outputNotA.text = notA.ToString();
            outputNotB.text = notB.ToString();
            outputAandB.text = aAndB.ToString();
            outputAorB.text = aOrB.ToString();
            outputAxorB.text = aXorB.ToString();
            outputAnorB.text = aNorB.ToString();
        }

       
    }

    float GetValidatedProbability(string input)
    {
        float value = 0;
        if (input.Equals(""))
        {
            return -1;
        }
        
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
    float GetValidatedProbability2(string input)
    {
        float value = 0;
        

        if (float.TryParse(input, out value))
        {
            if ((value >= 0) & (value <= 1))
            {
                return value;
            }
        }
        errorText2.text = "Input probability not valid.";
        return -1;
    }
}
