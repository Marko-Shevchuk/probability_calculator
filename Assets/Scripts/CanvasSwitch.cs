
using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitch : MonoBehaviour
{
    public Button menu1Button;
    public Button menu2Button;
    public Button canvas1Button;
    public Button canvas2Button;
    public Button canvas3Button;
    public Button canvas4Button;
    public GameObject menu1;
    public GameObject menu2;
    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject canvas3;
    public GameObject canvas4;
    private bool menu1Open = false;
    private bool menu2Open = false;
    void Start()
    {
        canvas1Button.onClick.AddListener(Canvas1Activate);
        canvas2Button.onClick.AddListener(Canvas2Activate);
        canvas3Button.onClick.AddListener(Canvas3Activate);
        canvas4Button.onClick.AddListener(Canvas4Activate);
        menu1Button.onClick.AddListener(Menu1Activate);
        menu2Button.onClick.AddListener(Menu2Activate);
        Canvas1Activate();
        menu1.SetActive(true);
        menu2.SetActive(true);
        Menu1Activate();
        menu1.transform.position = new Vector3(menu1.transform.position.x, menu1.transform.position.y + 300, menu1.transform.position.z);
    }
    void Menu1Activate()
    {
        if (!menu1Open)
        {
            menu1.transform.position = new Vector3(menu1.transform.position.x, menu1.transform.position.y - 300, menu1.transform.position.z);
            menu2.transform.position = new Vector3(menu2.transform.position.x, menu2.transform.position.y + 300, menu2.transform.position.z);
            menu1Open = true;
            menu2Open = false;
        }

        MakeButtonBoldAndDarkenBackground(menu1Button);
        RevertStyle(menu2Button);
    }
    void Menu2Activate()
    {
        if (!menu2Open)
        {
            menu1.transform.position = new Vector3(menu1.transform.position.x, menu1.transform.position.y + 300, menu1.transform.position.z);
            menu2.transform.position = new Vector3(menu2.transform.position.x, 410, menu2.transform.position.z);
            menu2Open = true;
            menu1Open = false;
        }

        MakeButtonBoldAndDarkenBackground(menu2Button);
        RevertStyle(menu1Button);
    }


    void Canvas1Activate()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(false);
        canvas3.SetActive(false);
        canvas4.SetActive(false);

        MakeButtonBoldAndDarkenBackground(canvas1Button);
        RevertStyle(canvas2Button);
        RevertStyle(canvas3Button);
        RevertStyle(canvas4Button);
    }

    void Canvas2Activate()
    {
        canvas2.SetActive(true);
        canvas1.SetActive(false);
        canvas3.SetActive(false);
        canvas4.SetActive(false);

        MakeButtonBoldAndDarkenBackground(canvas2Button);
        RevertStyle(canvas1Button);
        RevertStyle(canvas3Button);
        RevertStyle(canvas4Button);
    }
    void Canvas3Activate()
    {
        canvas2.SetActive(false);
        canvas1.SetActive(false);
        canvas3.SetActive(true);
        canvas4.SetActive(false);

        MakeButtonBoldAndDarkenBackground(canvas3Button);
        RevertStyle(canvas1Button);
        RevertStyle(canvas2Button);
        RevertStyle(canvas4Button);
    }
    void Canvas4Activate()
    {
        canvas2.SetActive(false);
        canvas1.SetActive(false);
        canvas3.SetActive(false);
        canvas4.SetActive(true);

        MakeButtonBoldAndDarkenBackground(canvas4Button);
        RevertStyle(canvas1Button);
        RevertStyle(canvas2Button);
        RevertStyle(canvas3Button);
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