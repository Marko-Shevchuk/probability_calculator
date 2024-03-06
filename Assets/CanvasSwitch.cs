
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

    }

    void Canvas1Activate()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(false);
    }
    void Canvas2Activate()
    {
        canvas2.SetActive(true);
        canvas1.SetActive(false);
    }
}