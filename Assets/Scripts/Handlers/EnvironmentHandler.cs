using System;
using Game.Api.Models;
using Game.Api.Responses;
using Game.Api;
using Game.Helpers;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Handler
{
    public class EnvironmentCreationHandler : MonoBehaviour
    {
        [field: SerializeField] public Environment2D Environment2D { get; private set; }
        [field: SerializeField] public List<Environment2D> Environments { get; private set; } = new List<Environment2D>();
        [SerializeField] private TMP_InputField _heightInput;
        [SerializeField] private TMP_InputField _lengthInput;
        [SerializeField] private TMPro.TextMeshProUGUI _errorText;

        #region Variable Setters
        public void SetErrorText(string text)
        {
            _errorText.text = text;
            _errorText.gameObject.SetActive(true);
        }

        public void SetEnvironmentName(string name)
        {
            Environment2D.name = name;
        }
        public void SetEnvironmentHeight(string height)
        {
            if (string.IsNullOrWhiteSpace(height))
            {
                if (_heightInput != null)
                    _heightInput.text = "0";
                Environment2D.maxHeight = 10;
                return;
            }

            Environment2D.maxHeight = int.Parse(height);

            if (Environment2D.maxHeight > 100)
            {
                Environment2D.maxHeight = 100;
                if (_heightInput != null)
                    _heightInput.text = "100";
            }
            else if (Environment2D.maxHeight < 10)
            {
                Environment2D.maxHeight = 10;
            }
        }
        public void SetEnvironmentLength(string length)
        {
            if (string.IsNullOrWhiteSpace(length))
            {
                if (_heightInput != null)
                    _lengthInput.text = "0";
                Environment2D.maxHeight = 20;
                return;
            }

            Environment2D.maxLength = int.Parse(length);

            if (Environment2D.maxLength > 200)
            {
                Environment2D.maxLength = 200;
                if (_heightInput != null)
                    _lengthInput.text = "200";
            }
            else if (Environment2D.maxLength < 20)
            {
                Environment2D.maxLength = 20;
            }
        }
        #endregion

        #region WebRequests
        public async void CreateEnvironment2D()
        {
            IWebRequestReponse webRequestResponse = await ApiManager.Instance.EnvironmentClient.CreateEnvironment(Environment2D);

            switch (webRequestResponse)
            {
                case WebRequestData<Environment2D> dataResponse:
                    Environment2D.id = dataResponse.Data.id;
                    SceneLoader.Loadscene("Worlds");
                    break;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    SetErrorText("Couldn't create world, are you sure that the values are correct and the name is original?");
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }
        public async void ReadEnvironment2Ds()
        {
            IWebRequestReponse webRequestResponse = await ApiManager.Instance.EnvironmentClient.ReadEnvironment2Ds();

            switch (webRequestResponse)
            {
                case WebRequestData<List<Environment2D>> dataResponse:
                    Environments = dataResponse.Data;
                    break;
                case WebRequestError errorRespone:
                    string errorMessage = errorRespone.ErrorMessage;
                    Debug.Log("Read Environment error: " + errorMessage);
                    SetErrorText("Geen Environment gevonden!");
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }

        public async void DeleteEnvironment2D(string id)
        {
            IWebRequestReponse webRequestResponse = await ApiManager.Instance.EnvironmentClient.DeleteEnvironment(id);

            switch (webRequestResponse)
            {
                case WebRequestData<string> dataResponse:
                    string responseData = dataResponse.Data;
                    break;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("Delete environment error: " + errorMessage);
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }
        #endregion
    }
}