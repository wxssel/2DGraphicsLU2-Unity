using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Api.Models;
using Game.Api.Responses;
using Game.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.World;

namespace Game.Handler
{
    public class EnvironmentObjectHandler : MonoBehaviour
    {
        public static EnvironmentObjectHandler Instance { get; private set; }
        [SerializeField] private Environment2D _environment2D;
        [SerializeField] private GameObject _saveButton;
        [SerializeField] private Button _exitButton;

        #region Setters
        public void Awake()
        {
            Instance = this;
        }
        public void SetEnvironment(Environment2D environment)
        {
            _environment2D = environment;
            CameraController.Instance.ChangeEnvironmentSize(new Vector2(environment.maxLength, environment.maxHeight));
        }
        #endregion

        #region Environment Object Functions


        
        #endregion

        #region Requests
        public async Task<List<Object2D>> ReadObject2Ds(Environment2D environment)
        {
            if (string.IsNullOrWhiteSpace(environment.id))
            {
                Debug.Log("Environment id is null or empty!");
                return null;
            }
            IWebRequestReponse webRequestResponse = await ApiManager.Instance.ObjectClient.ReadObject2Ds(environment.id);

            switch (webRequestResponse)
            {
                case WebRequestData<List<Object2D>> dataResponse:
                    List<Object2D> object2Ds = dataResponse.Data;
                    return object2Ds;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("Read object2Ds error: " + errorMessage);
                    // TODO: Error scenario. Show the errormessage to the user.
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
            return null;
        }

        public async Task<string> CreateObject2D(Object2D object2D)
        {
            IWebRequestReponse webRequestResponse = await ApiManager.Instance.ObjectClient.CreateObject2D(object2D);

            switch (webRequestResponse)
            {
                case WebRequestData<Object2D> dataResponse:
                    object2D.id = dataResponse.Data.id;
                    return object2D.id;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("Create Object2D error: " + errorMessage);
                    // TODO: Handle error scenario. Show the errormessage to the user.
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
            return null;
        }

        public async Task<bool> UpdateObject2D(Object2D object2D)
        {
            IWebRequestReponse webRequestResponse = await ApiManager.Instance.ObjectClient.UpdateObject2D(object2D);

            switch (webRequestResponse)
            {
                case WebRequestData<string> dataResponse:
                    string responseData = dataResponse.Data;
                    return true;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("Update object2D error: " + errorMessage);
                    // TODO: Handle error scenario. Show the errormessage to the user.
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
            return false;
        }

        public async Task<bool> DeleteObject2D(Object2D object2D)
        {
            IWebRequestReponse webRequestResponse = await ApiManager.Instance.ObjectClient.DeleteObject2D(object2D);

            switch (webRequestResponse)
            {
                case WebRequestData<string> dataResponse:
                    string responseData = dataResponse.Data;
                    return true;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("Delete object2D error: " + errorMessage + ", object id: " + object2D.id);
                    // TODO: Handle error scenario. Show the errormessage to the user.
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
            return false;
        }
        #endregion
    }

   
}