using System.Collections;
using System.Collections.Generic;
using MeasurementUnit;
using UnityEngine;

namespace MeasurementUnits.Tests
{

    public class MyComponent : MonoBehaviour
    {

        public Length length;

        private void Start()
        {
            Debug.Log(length);
        }

    }

}
