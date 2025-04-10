using System.Collections.Generic;
using System.Net;
using System.Numerics;
using Game.Api.Models;
using Game.Api.Responses;
using Game.Helpers;
using Scatter.Api.Responses;
using UnityEngine;

namespace Game.Api.Clients
{
    public class ObjectClient : MonoBehaviour
    {
        public WebClient webClient;

        public async Awaitable<IWebRequestReponse> ReadObject2Ds(string environmentId)
        {
            string route = "/environments/" + environmentId + "/objects";

            IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(route);
            return ParseObject2DListResponse(webRequestResponse);
        }

        public async Awaitable<IWebRequestReponse> CreateObject2D(Object2D object2D)
        {
            string route = "/environments/" + object2D.environmentId + "/objects";
            string data = JsonUtility.ToJson(object2D);

            IWebRequestReponse webRequestResponse = await webClient.SendPostRequest(route, data);
            return ParseObject2DResponse(webRequestResponse);
        }

        public async Awaitable<IWebRequestReponse> UpdateObject2D(Object2D object2D)
        {
            string route = "/environments/" + object2D.environmentId + "/objects/" + object2D.id;
            string data = JsonUtility.ToJson(object2D);

            return await webClient.SendPutRequest(route, data);
        }

        public async Awaitable<IWebRequestReponse> DeleteObject2D(Object2D object2D)
        {
            string route = "/environments/" + object2D.environmentId + "/objects/" + object2D.id;

            return await webClient.SendDeleteRequest(route);
        }


        private IWebRequestReponse ParseObject2DResponse(IWebRequestReponse webRequestResponse)
        {
            switch (webRequestResponse)
            {
                case WebRequestData<string> data:
                    //Debug.Log("Response data raw: " + data.Data);
                    Object object2D = JsonUtility.FromJson<Object>(data.Data);
                    WebRequestData<Object> parsedWebRequestData = new WebRequestData<Object>(object2D);
                    return parsedWebRequestData;
                default:
                    return webRequestResponse;
            }
        }
        private IWebRequestReponse ParseObject2DListResponse(IWebRequestReponse webRequestResponse)
        {
            switch (webRequestResponse)
            {
                case WebRequestData<string> data:
                    Debug.Log("Response data raw: " + data.Data);
                    List<Object2D> environments = JsonHelper.ParseJsonArray<Object2D>(data.Data);
                    WebRequestData<List<Object2D>> parsedData = new WebRequestData<List<Object2D>>(environments);
                    return parsedData;
                default:
                    return webRequestResponse;
            }
        }
    }
}