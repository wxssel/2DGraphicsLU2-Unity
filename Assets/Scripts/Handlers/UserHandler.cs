using System;
using Game.Api.Models;
using Game.Api;
using Game.Api.Responses;
using UnityEngine;
using UnityEngine.UI;
using Scatter.Api.Responses;
using Game.Helpers;

namespace Game.Handler
{
    public class AuthenticationHandler : MonoBehaviour
    {
        private User _user = new User();
        [SerializeField] private TMPro.TextMeshProUGUI _errorText;
        [SerializeField] private Button _confirmButton;

        #region Variable Setters
        public void SetUserMail(string mail)
        {
            _user.email = mail;
        }
        public void SetUserPassword(string password)
        {
            _user.password = password;
        }
        public void SetErrorText(string text)
        {
            _errorText.text = text;
            _errorText.gameObject.SetActive(true);
        }
        #endregion

        #region WebRequests

        public async void Register()
        {
            IWebRequestReponse webRequestResponse = await ApiManager.Instance.UserClient.Register(_user);

            switch (webRequestResponse)
            {
                case WebRequestData<string>:
                    SceneLoader.LoadScene("Login");
                    break;
                case WebRequestError:
                    SetErrorText("Email address already exists!");
                    _confirmButton.interactable = true;
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }

        public async void Login()
        {
            _confirmButton.interactable = false;
            IWebRequestReponse webRequestResponse = await ApiManager.Instance.UserClient.Login(_user);

            switch (webRequestResponse)
            {
                case WebRequestData<string>:
                    SceneLoader.LoadScene("MainMenu");
                    break;
                case WebRequestError:
                    SetErrorText("Email or password incorrect!");
                    _confirmButton.interactable = true;
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }
        #endregion
    }
}