
using Game.Api.Models;
using System.Net;
using UnityEngine;
using Game.Api.Responses;
using Game.Helpers;
using Scatter.Api.Responses;

namespace Game.Api.Clients
{
    public class UserClient : MonoBehaviour
    {
        public WebClient webClient;

        public async Awaitable<IWebRequestReponse> Register(User user)
        {
            string route = "/account/register";
            string data = JsonUtility.ToJson(user);

            return await webClient.SendPostRequest(route, data);
        }

        public async Awaitable<IWebRequestReponse> Login(User user)
        {
            string route = "/account/login";
            string data = JsonUtility.ToJson(user);

            IWebRequestReponse response = await webClient.SendPostRequest(route, data);
            return ProcessLoginResponse(response);
        }

        private IWebRequestReponse ProcessLoginResponse(IWebRequestReponse webRequestResponse)
        {
            switch (webRequestResponse)
            {
                case WebRequestData<string> data:
                    //Debug.Log("Response data raw: " + data.Data);
                    string token = JsonHelper.ExtractToken(data.Data);
                    webClient.SetToken(token);
                    return new WebRequestData<string>("Succes");
                default:
                    return webRequestResponse;
            }
        }
    }
}