using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DateTimeExtension
{
    char[] separators = { '-', ',', '.', ',', '/', '\\', ' ' };
    string[] formatArray;

    public DateTimeExtension() {
        CreateFormats();
    }

    void CreateFormats() {

        List<string> formatsList = new List<string>();
        formatsList.Add("yyyy");
        formatsList.Add("yy");

        foreach(char separator in separators) {
            formatsList.Add($"yyyy{separator}MM");
            formatsList.Add($"yyyy{separator}MM{separator}dd");
            formatsList.Add($"yyyy{separator}MM{separator}dd{separator}HH");
            formatsList.Add($"yyyy{separator}MM{separator}dd{separator}HH{separator}mm");
            formatsList.Add($"yyyy{separator}MM{separator}dd{separator}HH{separator}mm{separator}ss");

            formatsList.Add($"yyyy{separator}M");
            formatsList.Add($"yyyy{separator}M{separator}dd");
            formatsList.Add($"yyyy{separator}M{separator}dd{separator}HH");
            formatsList.Add($"yyyy{separator}M{separator}dd{separator}HH{separator}mm");
            formatsList.Add($"yyyy{separator}M{separator}dd{separator}HH{separator}mm{separator}ss");

            formatsList.Add($"yyyy{separator}M");
            formatsList.Add($"yyyy{separator}M{separator}d");
            formatsList.Add($"yyyy{separator}M{separator}d{separator}HH");
            formatsList.Add($"yyyy{separator}M{separator}d{separator}HH{separator}mm");
            formatsList.Add($"yyyy{separator}M{separator}d{separator}HH{separator}mm{separator}ss");

            formatsList.Add($"yyyy{separator}MM");
            formatsList.Add($"yyyy{separator}MM{separator}d");
            formatsList.Add($"yyyy{separator}MM{separator}d{separator}HH");
            formatsList.Add($"yyyy{separator}MM{separator}d{separator}HH{separator}mm");
            formatsList.Add($"yyyy{separator}MM{separator}d{separator}HH{separator}mm{separator}ss");

            formatsList.Add($"yy{separator}MM");
            formatsList.Add($"yy{separator}MM{separator}dd");
            formatsList.Add($"yy{separator}MM{separator}dd{separator}HH");
            formatsList.Add($"yy{separator}MM{separator}dd{separator}HH{separator}mm");
            formatsList.Add($"yy{separator}MM{separator}dd{separator}HH{separator}mm{separator}ss");

            formatsList.Add($"yy{separator}M");
            formatsList.Add($"yy{separator}M{separator}dd");
            formatsList.Add($"yy{separator}M{separator}dd{separator}HH");
            formatsList.Add($"yy{separator}M{separator}dd{separator}HH{separator}mm");
            formatsList.Add($"yy{separator}M{separator}dd{separator}HH{separator}mm{separator}ss");

            formatsList.Add($"yy{separator}M");
            formatsList.Add($"yy{separator}M{separator}d");
            formatsList.Add($"yy{separator}M{separator}d{separator}HH");
            formatsList.Add($"yy{separator}M{separator}d{separator}HH{separator}mm");
            formatsList.Add($"yy{separator}M{separator}d{separator}HH{separator}mm{separator}ss");

            formatsList.Add($"yy{separator}MM");
            formatsList.Add($"yy{separator}MM{separator}d");
            formatsList.Add($"yy{separator}MM{separator}d{separator}HH");
            formatsList.Add($"yy{separator}MM{separator}d{separator}HH{separator}mm");
            formatsList.Add($"yy{separator}MM{separator}d{separator}HH{separator}mm{separator}ss");
        }

        formatArray = formatsList.ToArray();
    }

    public (DateTime, DateTime) FormatDate(string input) { //Some cases will cause this to crash not super safe but for now this will work

        DateTime lowerDate = new();
        DateTime upperDate = new();

        if(!DateTime.TryParseExact(input, formatArray, CultureInfo.InvariantCulture, DateTimeStyles.None, out lowerDate)) {
            //Console.WriteLine("Could not format");
            lowerDate = DateTime.Now;
        }

        //Console.WriteLine(lowerDate.ToString());
        upperDate = lowerDate;

        string[] splitString = input.Split(separators);
        if(splitString.Length == 1) { // year
            upperDate = upperDate.AddSeconds(-1);
            upperDate = upperDate.AddYears(1);
        }
        else if(splitString.Length == 2) { // month
            upperDate = upperDate.AddMonths(1);
            upperDate = upperDate.AddSeconds(-1);
        }
        else if(splitString.Length == 3) { // day
            upperDate = upperDate.AddDays(1);
            upperDate = upperDate.AddSeconds(-1);
        }
        else if(splitString.Length == 4) { // hour
            upperDate = upperDate.AddHours(1);
            upperDate = upperDate.AddSeconds(-1);
        }
        else if(splitString.Length == 5) { //minute
            upperDate = upperDate.AddMinutes(1);
            upperDate = upperDate.AddSeconds(-1);
        }
        //else if(splitString.Length == 6) { //Second //unity does not know what micro seconds are
        //    upperDate = upperDate.AddSeconds(1);
        //    upperDate = upperDate.AddMicroseconds(-1);
        //}
        //else if(splitString.Length == 7) { //milliseconds
        //    upperDate = upperDate.AddMilliseconds(1);
        //    upperDate = upperDate.AddMicroseconds(-1);
        //}
        Console.WriteLine(upperDate.ToString());




        return (lowerDate, upperDate);
    }
}
