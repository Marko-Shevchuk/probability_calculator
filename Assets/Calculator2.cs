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
    

    //bayesian formula fields
    public TMP_InputField outputBiA1;
    public TMP_InputField outputBiA2;
    public TMP_InputField outputBiA3;
    public TMP_InputField outputBiA4;
    public TMP_InputField outputBiA5;
    public TMP_InputField outputBiA6;
    public TMP_InputField outputBiA7;
    public TMP_InputField outputBiA8;
    public TMP_InputField outputBiNotA1;
    public TMP_InputField outputBiNotA2;
    public TMP_InputField outputBiNotA3;
    public TMP_InputField outputBiNotA4;
    public TMP_InputField outputBiNotA5;
    public TMP_InputField outputBiNotA6;
    public TMP_InputField outputBiNotA7;
    public TMP_InputField outputBiNotA8;


    public Button calculateButton;
    public TMP_Text errorText;
    void Start()
    {
        calculateButton.onClick.AddListener(CalculateFullProbability);


    }

    void CalculateFullProbability()
    {
        // Initialize variables to hold the probabilities
        double P_A = GetValidatedProbability(A.text);
        double[] P_Bi = new double[8];
        double[] P_A_given_Bi = new double[8];

        bool invalidInput = false;

        for (int i = 0; i < 8; i++)
        {
            P_Bi[i] = GetValidatedProbability(GetBiInputField(i).text);
            P_A_given_Bi[i] = GetValidatedProbability(GetABiInputField(i).text);

            if (P_Bi[i] == -2 || P_A_given_Bi[i] == -2)
            {
                invalidInput = true;
            }
        }
        if (P_A == -2) invalidInput = true;

        if (invalidInput)
        {
            errorText.text = "Invalid input. Please enter valid probabilities.";
            return;
        }

        
        int missingIndex = -1;
        int endCount = -1;
        bool dbz = false;
        for (int i = 0; i < 8; i++)
        {
            GetOutputABiField(i).text = GetABiInputField(i).text;
            GetOutputBiField(i).text = GetBiInputField(i).text;
            if ((P_A_given_Bi[i] == -1) ^ (P_Bi[i] == -1))
            {
                if (missingIndex != -1)
                {
                    invalidInput = true;
                    break;
                }
                else
                {
                    missingIndex = i;
                }
                
                
            }
            if (P_A_given_Bi[i] == -1 && P_Bi[i] == -1)
            {
                endCount = i;
                break;
            }
            
        }
        if (endCount == -1) endCount = 8;
        if (invalidInput)
        {
            errorText.text = "Can't calculate anything if 2 or more things are missing.";
            return;
        }
        if (P_A == -1)
        {
            double sum = 0;
            for (int i = 0; i < endCount; i++)
            {
                sum += P_Bi[i];
            }
            if (sum - 1 > 0.0002)
            {
                invalidInput = true;
                errorText.text = "The sum of Bi probabilities must be equal to 1.";
            }
        }
        if (P_A == -1)
        {
            P_A = 0;

            // Calculate P(A) if not given
            for (int i = 0; i < endCount; i++)
            {
                if (P_A_given_Bi[i] == -1 ^ P_Bi[i] == -1)
                {
                    invalidInput = true;
                    break;
                }
                if (P_A_given_Bi[i] == -1 && P_Bi[i] == -1)
                {
                    endCount = i;
                    break;
                }
                P_A += P_A_given_Bi[i] * P_Bi[i];
            }
            
            if (invalidInput)
            {
                errorText.text = "Invalid input. Can't calculate P(A) if not all data is given.";
                return;
            }
            else
            {
                outputA.text = P_A.ToString();
            }
        }
        else
        {
            outputA.text = A.text;
            double remainderA = P_A;
            for (int i = 0; i < endCount; i++)
            {
                if (i == missingIndex)
                {
                    continue;
                }
                remainderA -= P_A_given_Bi[i] * P_Bi[i];
            }
            if (P_Bi[missingIndex] == -1 && P_A_given_Bi[missingIndex] != -1)
            {
                if (P_A_given_Bi[missingIndex] == 0)
                {
                    GetOutputBiField(missingIndex).text = "???";
                    GetOutputABiField(missingIndex).text = "???";
                    dbz = true;
                }
                else
                {
                    P_Bi[missingIndex] = remainderA / P_A_given_Bi[missingIndex];
                    GetOutputBiField(missingIndex).text = P_Bi[missingIndex].ToString();
                }
                
            }
            else if (P_Bi[missingIndex] != -1 && P_A_given_Bi[missingIndex] == -1)
            {
                if (P_Bi[missingIndex] == 0)
                {
                    GetOutputABiField(missingIndex).text = "???";
                    GetOutputBiField(missingIndex).text = "???";
                    dbz = true;
                }
                else
                {
                    P_A_given_Bi[missingIndex] = remainderA / P_Bi[missingIndex];
                    GetOutputABiField(missingIndex).text = P_A_given_Bi[missingIndex].ToString();
                }
                
            }
            else
            {
                invalidInput = true;
                errorText.text = "Data doesn't add up.";
            }

        }

        if (invalidInput)
        {
            outputA.text = "";
            for (int i = 0; i < 8; i++)
            {
                
                GetOutputABiField(i).text = "";
                GetOutputBiField(i).text = "";
            }
        }
        else if (dbz == false)
        {
            // Calculate P(Bi|A) and P(Bi|NOT A) using Bayes' formula
            for (int i = 0; i < endCount; i++)
            {
                if (P_A_given_Bi[i] != -1 && P_Bi[i] != -1)
                {
                    double P_Bi_given_A = P_Bi[i] * P_A_given_Bi[i] / P_A;
                    double P_Bi_given_NotA = P_Bi[i] * (1 - P_A_given_Bi[i]) / (1 - P_A);

                    GetOutputBiAField(i).text = P_Bi_given_A.ToString();
                    GetOutputBiNotAField(i).text = P_Bi_given_NotA.ToString();
                }
                else
                {
                    errorText.text += "Bayesian calculation failed.";
                }
            }
        }
        else
        {
            errorText.text = "Division by zero detected.";
        }
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
            if ((value >= 0) & (value <= 1))
            {
                return value;
            }
        }
        errorText.text = "Input value not a number or not within [0,1].";
        return -2;
    }

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
    TMP_InputField GetOutputBiAField(int index)
    {
        switch (index)
        {
            case 0: return outputBiA1;
            case 1: return outputBiA2;
            case 2: return outputBiA3;
            case 3: return outputBiA4;
            case 4: return outputBiA5;
            case 5: return outputBiA6;
            case 6: return outputBiA7;
            case 7: return outputBiA8;
            default: return null;
        }
    }

    TMP_InputField GetOutputBiNotAField(int index)
    {
        switch (index)
        {
            case 0: return outputBiNotA1;
            case 1: return outputBiNotA2;
            case 2: return outputBiNotA3;
            case 3: return outputBiNotA4;
            case 4: return outputBiNotA5;
            case 5: return outputBiNotA6;
            case 6: return outputBiNotA7;
            case 7: return outputBiNotA8;
            default: return null;
        }
    }

    
}