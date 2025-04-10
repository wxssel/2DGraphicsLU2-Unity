using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Api.Clients;

namespace Game.Helpers
{
    public static class JsonHelper
    {
        public static List<T> ParseJsonArray<T>(string jsonArray)
        {
            string extendedJson = "{\"list\":" + jsonArray + "}";
            JsonList<T> parsedList = JsonUtility.FromJson<JsonList<T>>(extendedJson);
            return parsedList.list;
        }

        public static string ExtractToken(string data)
        {
            Token token = JsonUtility.FromJson<Token>(data);
            return token.accessToken;
        }
    }

    [Serializable]
    public class JsonList<T>
    {
        public List<T> list;
    }
}