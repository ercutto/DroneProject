
using UnityEngine;
namespace PinBall {
    [CreateAssetMenu(fileName = "reflectorsAndPoints", menuName = "ScriptableObjects/PinballAsset", order = 1)]
    public class SpeedReflectors : ScriptableObject
    {
        /// <summary>
        /// this has to be added to reflector objects that you want to add in to tablead
        /// </summary>
        public GameObject prefab;
        public string objectName;
        public float force;
        public float pointValue;
        
    }
}

