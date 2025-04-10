using System;

namespace Game.Event
{
    [Serializable]
    public class GameEvent
    {
        public string sceneName;
        public IEventCall eventCall;
    }
}