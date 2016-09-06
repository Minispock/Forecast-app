using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIForecastElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float scale = 0;

    [SerializeField]
    private Text day;
    [SerializeField]
    private Text weatherInfo;
    [SerializeField]
    private Image picture;


   public void Fill(DailyForecast forecast)//method for UI filling with getting data
    {
        scale = 1.0F;
        UIMain main = FindObjectOfType<UIMain>();
        picture.sprite = main.Pictures[forecast.Code];
        this.day.text = forecast.Date.Day + " " + forecast.Date.DayOfWeek;

        string temp = (forecast.Temp.HasValue) ? string.Format("{0:#} \u00B0C\n", forecast.Temp) : string.Empty;
        string result = string.Format("{0}\n{1}Min: {2:#} \u00B0C\nMax: {3:#} \u00B0C", forecast.Text, temp, forecast.Low, forecast.High);

        this.weatherInfo.text = result;

    }

    void Update()//update per frame
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(scale,scale, 1), 0.05F);//method to make UI mosaic smaller/bigger
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        scale = 1.1F;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        scale = 1.0F;
    }
}
