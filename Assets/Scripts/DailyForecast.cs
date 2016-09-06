using System;
using System.Xml.Linq;

public class DailyForecast // for parsing data in separate variables to use them in filling UI
    {
   
    public string Text { get; set; }
    public float? Temp { get; set; }
    public DateTime Date { get; set; }
    public float Low { get; set; }
    public float High { get; set; }
    public int Code { get; set; }


    public static implicit operator DailyForecast(XElement xForecast)
    {
        DailyForecast weather = new DailyForecast();

        weather.Text = xForecast.Attribute("text").Value;
        if (xForecast.Attribute("temp") != null)
           { weather.Temp = (float.Parse(xForecast.Attribute("temp").Value) - 32) * 5 / 9F; }
        weather.Date = DateTime.Parse(xForecast.Attribute("date").Value);
        weather.Low = (float.Parse(xForecast.Attribute("low").Value)- 32) * 5 / 9F;
        weather.High = (float.Parse(xForecast.Attribute("high").Value) - 32) * 5 / 9F;
        weather.Code = int.Parse(xForecast.Attribute("code").Value);

        return weather;
    }

    public override string ToString()
    {
        return String.Format("{0}, {1:#}, {2:#}, {3:#}, {4:#}, {5}", Date, Text, Temp, Low, High, Code);
    }

}


