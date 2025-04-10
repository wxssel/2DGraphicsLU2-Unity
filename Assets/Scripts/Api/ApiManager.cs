using System;
using Game.Api.Models;
using Game.Api.Clients;     
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Game.Event;

namespace Game.Api
{
    public class ApiManager : MonoBehaviour
    {
        [Header("Api's")]
        public static ApiManager Instance { get; private set; }
        [field: SerializeField] public UserClient UserClient { get; private set; }
        [field: SerializeField] public EnvironmentClient EnvironmentClient { get; private set; }
        [field: SerializeField] public ObjectClient ObjectClient { get; private set; }
        [field: SerializeField] public List<GameEvent> GameEvents { get; private set; }

        [Header("Variables")]
        private bool _isLoggedIn;
        public Environment2D CurrentEnvironment { get; set; }
        public bool ShouldEnvironmentBeLoaded { get; set; } = false;

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance.gameObject);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            List<GameEvent> executedEvents = new List<GameEvent>();
            foreach (GameEvent gameEvent in GameEvents)
            {
                if (SceneManager.GetActiveScene().name == gameEvent.sceneName)
                {
                    gameEvent.eventCall.RunEvent();
                    executedEvents.Add(gameEvent);
                }
            }
            foreach (GameEvent gameEvent in executedEvents)
            {
                GameEvents.Remove(gameEvent);
            }
        }
        public bool IsUserLoggedIn()
        {
            return _isLoggedIn;
        }
    }
}