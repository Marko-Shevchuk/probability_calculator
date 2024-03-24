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

    public TMP_InputField outputProbabilityField;

    public Button calculateButton;
    public TMP_Text errorText;

    void Start()
    {
        calculateButton.onClick.AddListener(CalculateBinomialProbability);
    }

    void CalculateBinomialProbability()
    {
        double p = GetValidatedProbability(pInputField.text);
        double q = GetValidatedProbability(qInputField.text);
        int n = GetValidatedInteger(nInputField.text);
        int k = GetValidatedInteger(kInputField.text);

        if (p == -2 || q == -2 || n == -2 || k == -2)
        {
            errorText.text = "Invalid input. Please enter valid probabilities and integers.";
            return;
        }
        if (p != -1) q = 1 - p;
        if (q != -1) p = 1 - q;
        double probability = CalculateBinomialProbability(n, k, p, q);
        outputProbabilityField.text = probability.ToString("F6"); //6 decimal places
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