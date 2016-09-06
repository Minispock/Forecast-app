using UnityEngine;
using System.Collections;

public class UIMain : MonoBehaviour {

    [SerializeField]
    private Sprite[] pictures;
    public Sprite[] Pictures { get { return pictures; } }
    [SerializeField]
    private UIForecastElement[] elements;
    public UIForecastElement[] Elements { get { return elements; } }


    public void Fill()//method to fill all UI elements with parsed info
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].Fill(XMLReader.Forecast[i]);
        }
        
    }
}
