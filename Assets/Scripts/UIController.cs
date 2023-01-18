using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PinBall
{
    public class UIController : MonoBehaviour
    {
        public GameObject TriggerButton;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ActiveOrFalse(GameObject obj, bool value)
        {
            obj.SetActive(value);
        }
        
    }
}

