using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Calculator2 : MonoBehaviour
{
    public TMP_InputField A;
    
    public TMP_InputField ABi1;
    public TMP_InputField ABi2;
    public TMP_InputField ABi3;
    public TMP_InputField ABi4;
    public TMP_InputField ABi5;
    public TMP_InputField ABi6;
    public TMP_InputField ABi7;
    public TMP_InputField ABi8;
    
    public TMP_InputField Bi1;
    public TMP_InputField Bi2;
    public TMP_InputField Bi3;
    public TMP_InputField Bi4;
    public TMP_InputField Bi5;
    public TMP_InputField Bi6;
    public TMP_InputField Bi7;
    public TMP_InputField Bi8;
    public TMP_InputField outputA;
    public TMP_InputField outputABi1;
    public TMP_InputField outputABi2;
    public TMP_InputField outputABi3;
    public TMP_InputField outputABi4;
    public TMP_InputField outputABi5;
    public TMP_InputField outputABi6;
    public TMP_InputField outputABi7;
    public TMP_InputField outputABi8;
    public TMP_InputField outputBi1;
    public TMP_InputField outputBi2;
    public TMP_InputField outputBi3;
    public TMP_InputField outputBi4;
    public TMP_InputField outputBi5;
    public TMP_InputField outputBi6;
    public TMP_InputField outputBi7;
    public TMP_InputField outputBi8;
    public Button calculateButton;
    public TMP_Text errorText;



    void Start()
    {
        calculateButton.onClick.AddListener(CalculateFullProbability);


    }

    void CalculateFullProbability()
    {
        // Initialize variables to hold the probabilities
        float P_A = GetValidatedProbability(A.text);
        float[] P_Bi = new float[8];
        float[] P_A_given_Bi = new float[8];

        // Check if any input is invalid
        bool invalidInput = false;

        // Get the probabilities for each Bi
        for (int i = 0; i < 8; i++)
        {
            P_Bi[i] = GetValidatedProbability(GetBiInputField(i).text);
            P_A_given_Bi[i] = GetValidatedProbability(GetABiInputField(i).text);

            if (P_Bi[i] == -2 || P_A_given_Bi[i] == -2)
            {
                invalidInput = true;
            }
        }

        // If any input is invalid, set error text and return
        if (invalidInput || P_A == -2)
        {
            errorText.text = "Invalid input. Please enter valid probabilities.";
            return;
        }

        // Calculate missing values
        for (int i = 0; i < 8; i++)
        {
            if (P_Bi[i] == -1)
            {
                // Calculate P(Bi) if not given
                P_Bi[i] = P_A_given_Bi[i] * P_A;
            }
            else if (P_A_given_Bi[i] == -1)
            {
                // Calculate P(A|Bi) if not given
                P_A_given_Bi[i] = P_A * P_Bi[i];
            }
            else if (P_A == -1)
            {
                // Calculate P(A) if not given
                P_A = P_A_given_Bi[i] / P_Bi[i];
            }

            // Update the output fields
            GetOutputBiField(i).text = P_Bi[i].ToString();
            GetOutputABiField(i).text = P_A_given_Bi[i].ToString();
        }

        // Update the output field for P(A)
        outputA.text = P_A.ToString();
    }

    // Helper methods to get the input and output fields
    TMP_InputField GetBiInputField(int index)
    {
        switch (index)
        {
            case 0: return Bi1;
            case 1: return Bi2;
            case 2: return Bi3;
            case 3: return Bi4;
            case 4: return Bi5;
            case 5: return Bi6;
            case 6: return Bi7;
            case 7: return Bi8;
            default: return null;
        }
    }

    TMP_InputField GetABiInputField(int index)
    {
        switch (index)
        {
            case 0: return ABi1;
            case 1: return ABi2;
            case 2: return ABi3;
            case 3: return ABi4;
            case 4: return ABi5;
            case 5: return ABi6;
            case 6: return ABi7;
            case 7: return ABi8;
            default: return null;
        }
    }

    TMP_InputField GetOutputBiField(int index)
    {
        switch (index)
        {
            case 0: return outputBi1;
            case 1: return outputBi2;
            case 2: return outputBi3;
            case 3: return outputBi4;
            case 4: return outputBi5;
            case 5: return outputBi6;
            case 6: return outputBi7;
            case 7: return outputBi8;
            default: return null;
        }
    }

    TMP_InputField GetOutputABiField(int index)
    {
        switch (index)
        {
            case 0: return outputABi1;
            case 1: return outputABi2;
            case 2: return outputABi3;
            case 3: return outputABi4;
            case 4: return outputABi5;
            case 5: return outputABi6;
            case 6: return outputABi7;
            case 7: return outputABi8;
            default: return null;
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
        return -2;
    }
}