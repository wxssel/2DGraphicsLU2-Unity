using System;
using Game.Api.Models;
using Game.Api;
using Game.Api.Responses;
using UnityEngine;
using UnityEngine.UI;
using Game.Api.Clients;
using Game.Helpers;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace Game.Handler
{
    //    public class UserHandler : MonoBehaviour
    ////    {
    //private User _User = new User();
    //[SerializeField] private TMP_InputField _emailInputField;
    //[SerializeField] private TMP_InputField _passwordInputField;
    //[SerializeField] private TMPro.TextMeshProUGUI _errorText;
    //[SerializeField] private GameObject _loginButton;
    //[SerializeField] private GameObject _registerButton;

    //        #region Variable Setters

    //public void SaveUserMail()
    //{
    //    string mail = _emailInputField.text;
    //    _User.email = mail;
    //}
    //public void SaveUserPassword()
    //{
    //    string password = _passwordInputField.text;
    //    _User.password = password;
    //}
    //        public void SetErrorText(string text)
    //        {
    //            _errorText.text = text;
    //            _errorText.gameObject.SetActive(true);
    //        }
    //        #endregion

    //        #region WebRequests

    //        public async void Register()
    //        {
//    var user = new User
//    {
//        email = _emailInputField.text,
//        password = _passwordInputField.text
//    };

//                if (CheckInputValidation(user))
//                {

//                    IWebRequestReponse webRequestResponse = await ApiManager.Instance.UserClient.Register(user);

//                    switch (webRequestResponse)
//                    {
//                        case WebRequestData<string> dataResponse:
//                            Debug.Log("Register succes!");


//                            // Maybe return back to login panel to verify login
//                            SceneManager.LoadScene("Menu Screen");

//                            break;
//                        case WebRequestError errorResponse:
//                            string errorMessage = errorResponse.ErrorMessage;
//    Debug.Log("Register error: " + errorMessage);
//                            // TODO: Handle error scenario. Show the errormessage to the user.
//                            break;
//                        default:
//                            throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
//}
//                }
//                else
//{
//    // TODO: Add red text to inputfield that is null
//    if (string.IsNullOrEmpty(_emailInputField.text))
//    {
//        Debug.Log("Username is null or empty");
//    }

//    if (string.IsNullOrEmpty(_passwordInputField.text))
//    {
//        Debug.Log("Passowrd is null or empty");
//    }
//}

//            }

    //        public async void Login()
    //        {
    //            IWebRequestReponse webRequestResponse = await ApiManager.Instance.UserClient.Login(_User);

    //            switch (webRequestResponse)
    //            {
    //                case WebRequestData<string>:
    //                    SceneLoader.Loadscene("EnvironmentChoose");
    //                    break;
    //                case WebRequestError:
    //                    SetErrorText("Email or password incorrect!");
    //                    break;
    //                default:
    //                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
    //            }
    //        }

    //        private bool CheckInputValidation(User user)
    //        {
    //            return !string.IsNullOrEmpty(user.email) || !string.IsNullOrEmpty(user.password);
    //        }
    //        #endregion
    //    }
    //}


    public class UserHandler : MonoBehaviour
    {
        [Header("Test data")]
        [SerializeField] private User user = new User();
        [SerializeField] private TMP_InputField _emailInputField;
        [SerializeField] private TMP_InputField _passwordInputField;
        [SerializeField] private TMPro.TextMeshProUGUI _errorText;
        [SerializeField] private GameObject _loginButton;
        [SerializeField] private GameObject _registerButton;

        

        [Header("Dependencies")]
        public UserClient userClient;

        #region Login

        [ContextMenu("User/Register")]
        public async void Register()
        {
            var user = new User
            {
                email = _emailInputField.text,
                password = _passwordInputField.text
            };

            if (CheckInputValidation(user))
            {

                IWebRequestReponse webRequestResponse = await ApiManager.Instance.UserClient.Register(user);

                switch (webRequestResponse)
                {
                    case WebRequestData<string> dataResponse:
                        Debug.Log("Register succes!");


                        

                        break;
                    case WebRequestError errorResponse:
                        string errorMessage = errorResponse.ErrorMessage;
                        Debug.Log("Register error: " + errorMessage);
                        // TODO: Handle error scenario. Show the errormessage to the user.
                        break;
                    default:
                        throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
                }
            }
            else
            {
                // TODO: Add red text to inputfield that is null
                if (string.IsNullOrEmpty(_emailInputField.text))
                {
                    Debug.Log("Username is null or empty");
                }

                if (string.IsNullOrEmpty(_passwordInputField.text))
                {
                    Debug.Log("Passowrd is null or empty");
                }
            }

        }
   

        [ContextMenu("User/Login")]
        public async void Login()
        {
            var user = new User
            {
                email = _emailInputField.text,
                password = _passwordInputField.text
            };

            if (CheckInputValidation(user))
            {

                IWebRequestReponse webRequestResponse = await ApiManager.Instance.UserClient.Login(user);

                switch (webRequestResponse)
                {
                    case WebRequestData<string> dataResponse:
                        Debug.Log("Login succes!");

                        // Maybe return back to login panel to verify login
                        SceneManager.LoadScene("EnvironmentChoose");

                        break;
                    case WebRequestError errorResponse:
                        string errorMessage = errorResponse.ErrorMessage;
                        Debug.Log("Login error: " + errorMessage);
                        // TODO: Handle error scenario. Show the errormessage to the user.
                        break;
                    default:
                        throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
                }
            }
            else
            {
                // TODO: Add red text to inputfield that is null
                if (string.IsNullOrEmpty(_emailInputField.text))
                {
                    Debug.Log("Username is null or empty");
                }

                if (string.IsNullOrEmpty(_passwordInputField.text))
                {
                    Debug.Log("Passowrd is null or empty");
                }
            }

        }
        private bool CheckInputValidation(User user)
        {
            return !string.IsNullOrEmpty(user.email) || !string.IsNullOrEmpty(user.password);
        }
    }
}


#endregion
