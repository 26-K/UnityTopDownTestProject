using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PPD
{

    public class InputControlBehaviour : MonoBehaviour
    {
        public static event System.Action<Vector2> MoveEvent;

        public void OnMove(Vector2 direction)
        {
            InputControlBehaviour.MoveEvent?.Invoke(direction);
        }

    }
}