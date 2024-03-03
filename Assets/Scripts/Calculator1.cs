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

    void Start()
    {
        calculateButton.onClick.AddListener(CalculateProbability);
    }

    void CalculateProbability()
    {
        
    }
}
