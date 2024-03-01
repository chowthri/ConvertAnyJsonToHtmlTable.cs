using System;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main()
    {
        string json = "{\"person\":{\"name\":\"John\",\"age\":30,\"address\":{\"city\":\"New York\",\"zipcode\":\"10001\"}},\"skills\":[\"C#\",\"JavaScript\",\"HTML\"]}";

        string html = ConvertJsonToHtml(json);

        Console.WriteLine(html);
    }

    static string ConvertJsonToHtml(string json)
    {
        JObject jsonObject = JObject.Parse(json);

        string html = "<div class='json-object'>";
        html += ConvertObjectToHtml(jsonObject);
        html += "</div>";

        return html;
    }

    static string ConvertObjectToHtml(JObject obj)
    {
        string html = "<ul>";

        foreach (var property in obj.Properties())
        {
            html += "<li>";
            html += $"<span class='property'>{property.Name}</span>: ";

            if (property.Value.Type == JTokenType.Object)
            {
                html += ConvertObjectToHtml((JObject)property.Value);
            }
            else if (property.Value.Type == JTokenType.Array)
            {
                html += ConvertArrayToHtml((JArray)property.Value);
            }
            else
            {
                html += $"<span class='value'>{property.Value}</span>";
            }

            html += "</li>";
        }

        html += "</ul>";

        return html;
    }

    static string ConvertArrayToHtml(JArray array)
    {
        string html = "<ul>";

        foreach (var item in array)
        {
            html += "<li>";

            if (item.Type == JTokenType.Object)
            {
                html += ConvertObjectToHtml((JObject)item);
            }
            else if (item.Type == JTokenType.Array)
            {
                html += ConvertArrayToHtml((JArray)item);
            }
            else
            {
                html += $"<span class='value'>{item}</span>";
            }

            html += "</li>";
        }

        html += "</ul>";

        return html;
    }
}
