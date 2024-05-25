using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BinomialProbabilityCalculator : MonoBehaviour
{
    public TMP_InputField pInputField;
    public TMP_InputField qInputField;
    public TMP_InputField nInputField;
    public TMP_InputField kInputField; 
    public TMP_InputField k1InputField; 
    public TMP_InputField k2InputField; 

    public TMP_InputField outputSingleKField; 
    public TMP_InputField outputK1ToK2Field;
    public TMP_InputField outputExclusiveK1ToK2Field;

    public Button calculateButton;
    public TMP_Text errorText;

    public TMP_InputField outputExactlyK1Field; 
    public TMP_InputField outputExactlyK2Field; 
    public TMP_InputField outputMoreThanK1Field; 
    public TMP_InputField outputLessThanK2Field; 
    public TMP_InputField outputMoreEqualThanK1Field;
    public TMP_InputField outputLessEqualThanK2Field;

    void Start()
    {
        calculateButton.onClick.AddListener(CalculateBinomialProbabilities);
    }

    void CalculateBinomialProbabilities()
    {
        double p = GetValidatedProbability(pInputField.text);
        double q = GetValidatedProbability(qInputField.text);
        int n = GetValidatedInteger(nInputField.text);
        int k = GetValidatedInteger(kInputField.text);
        int k1 = GetValidatedInteger(k1InputField.text);
        int k2 = GetValidatedInteger(k2InputField.text);

        if (p == -2 || q == -2 || n == -2 || k == -2 || k1 == -2 || k2 == -2)
        {
            errorText.text = "Invalid input. Please enter valid probabilities and integers.";
            return;
        }
        if (p != -1) q = 1 - p;
        if (q != -1) p = 1 - q;

        // Calculate for single k value
        double singleKProbability = CalculateBinomialProbability(n, k, p, q);
        outputSingleKField.text = singleKProbability.ToString("F6");

        // Calculate for range between k1 and k2 using a loop
        double k1ToK2Probability = 0;
        for (int i = k1; i <= k2; i++)
        {
            k1ToK2Probability += CalculateBinomialProbability(n, i, p, q);
        }
        outputK1ToK2Field.text = k1ToK2Probability.ToString("F6");


        double exactlyK1Probability = CalculateBinomialProbability(n, k1, p, q);
        outputExactlyK1Field.text = exactlyK1Probability.ToString("F6");

        double exactlyK2Probability = CalculateBinomialProbability(n, k2, p, q);
        outputExactlyK2Field.text = exactlyK2Probability.ToString("F6");


        double exclusiveK1ToK2Probability;
        exclusiveK1ToK2Probability = k1ToK2Probability - exactlyK1Probability - exactlyK2Probability;
        outputExclusiveK1ToK2Field.text = exclusiveK1ToK2Probability.ToString("F6");


        double moreThanK1Probability = 0;
        double moreEqualThanK1Probability = 0;
        for (int i = k1 + 1; i <= n; i++)
        {
            moreThanK1Probability += CalculateBinomialProbability(n, i, p, q);
        }
        outputMoreThanK1Field.text = moreThanK1Probability.ToString("F6");
        moreEqualThanK1Probability = moreThanK1Probability + exactlyK1Probability;
        outputMoreEqualThanK1Field.text = moreEqualThanK1Probability.ToString("F6");


        double lessThanK2Probability = 0;
        double lessEqualThanK2Probability = 0;
        for (int i = 0; i < k2; i++)
        {
            lessThanK2Probability += CalculateBinomialProbability(n, i, p, q);
        }
        outputLessThanK2Field.text = lessThanK2Probability.ToString("F6");
        lessEqualThanK2Probability = lessThanK2Probability + exactlyK2Probability;
        outputLessEqualThanK2Field.text = lessEqualThanK2Probability.ToString("F6");
    }

    double CalculateBinomialProbability(int n, int k, double p, double q)
    {
        double binomialCoefficient = CalculateBinomialCoefficient(n, k);
        double probability = binomialCoefficient * Math.Pow(p, k) * Math.Pow(q, n - k);
        return probability;
    }

    double CalculateBinomialCoefficient(int n, int k)
    {
        double result = 1;
        for (int i = 1; i <= k; i++)
        {
            result *= (n - i + 1) / (double)i;
        }
        return result;
    }

    double GetValidatedProbability(string input)
    {
        double value = 0;
        if (string.IsNullOrEmpty(input))
        {
            return -1;
        }

        if (double.TryParse(input, out value))
        {
            if ((value >= 0) && (value <= 1))
            {
                return value;
            }
        }
        errorText.text = "Input value not a number or not within [0,1].";
        return -2;
    }

    int GetValidatedInteger(string input)
    {
        int value = 0;
        if (string.IsNullOrEmpty(input))
        {
            return -1;
        }

        if (int.TryParse(input, out value))
        {
            return value;
        }
        errorText.text = "Input value not an integer.";
        return -2;
    }
}