﻿using System.Linq;
using System.Text.Json;

namespace WeatherDomainLibrary.Model
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
    public class JsonSerializerCustomOptions
    {

        public static JsonSerializerOptions SnakeCaseJsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            WriteIndented = true
        };
    }
}
