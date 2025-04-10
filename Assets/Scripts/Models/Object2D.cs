using System;
namespace Game.Api.Models
{
    [Serializable]
    public class Object2D
    {
        public string id;

        public string environmentId;

        public string prefabId;

        public float positionX;

        public float positionY;

        public float scaleX;

        public float scaleY;

        public float rotationZ;

        public int sortingLayer;
    }
}