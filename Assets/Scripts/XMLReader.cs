using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System;

public class XMLReader : MonoBehaviour

{
    private static List<DailyForecast> forecast = new List<DailyForecast>();
    public static List<DailyForecast> Forecast { get { return forecast; } }
    [SerializeField]
    private GameObject splashImage; // picture for loading screenshot
    private const string yweatherNamespace = "http://xml.weather.yahoo.com/ns/rss/1.0";
    private const string xmlUrl = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text=%22kyiv%22)";

    private IEnumerator LoadDataFromXML()
    {
        WWW request = new WWW(xmlUrl); 
        yield return request;

        splashImage.SetActive(false);
                
        XElement item = XDocument.Parse(request.text).Element("query").Element("results").Element("channel").Element("item");//parsing of data from xml
        IEnumerable<XElement> query = from element in item.Elements() // LINQ to xml query to get data from namespace yweather
                                      where element.Name.Namespace == yweatherNamespace
                                      select element;
        List<XElement> elements = query.ToList(); // execute LINQ request

        float temp = (float.Parse(elements[0].Attribute("temp").Value) - 32) * 5 / 9F; // get temp value for condition, not common for other elements
        string text = elements[0].Attribute("text").Value; 
        int code = int.Parse(elements[0].Attribute("code").Value);
        elements.Remove(elements[0]);
              
        foreach (XElement element in elements)
        {
            Forecast.Add(element);
        }

        Forecast[0].Temp = temp;//write down to forecast new value of the first list element for temp
        Forecast[0].Text = text;//the same for text     
        Forecast[0].Code = code;//the same for code

        FindObjectOfType<UIMain>().Fill(); //call the method to fill UI
        FindObjectOfType<ParticleController>().ChooseEffect(forecast[0].Code); //call the method for choosing particles due to weather conditions
    }
    

    void Start() // unity initial method
    {
        StartCoroutine(LoadDataFromXML());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }


}
