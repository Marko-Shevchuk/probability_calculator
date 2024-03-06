
using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitch : MonoBehaviour
{
    public Button canvas1Button;
    public Button canvas2Button;
    public GameObject canvas1;
    public GameObject canvas2;

    void Start()
    {
        canvas1Button.onClick.AddListener(Canvas1Activate);
        canvas2Button.onClick.AddListener(Canvas2Activate);


        Canvas1Activate();
    }

    void Canvas1Activate()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(false);


        MakeButtonBoldAndDarkenBackground(canvas1Button);
        RevertStyle(canvas2Button);
    }

    void Canvas2Activate()
    {
        canvas2.SetActive(true);
        canvas1.SetActive(false);


        MakeButtonBoldAndDarkenBackground(canvas2Button);
        RevertStyle(canvas1Button);
    }

    void MakeButtonBoldAndDarkenBackground(Button button)
    {

        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.fontStyle = FontStyle.Bold;
        }


        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = new Color(1.0f, 1.0f, 0.5f, 1f); 
        }
    }
    void RevertStyle(Button button)
    {

        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.fontStyle = FontStyle.Normal;
        }


        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = new Color(0.9f, 0.9f, 1f, 1f);
        }
    }
}