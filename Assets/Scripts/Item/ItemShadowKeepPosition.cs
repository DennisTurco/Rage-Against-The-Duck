using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShadowKeepPosition : MonoBehaviour
{
    private Vector3 initialPosition;

    private void Start()
    {
        // Memorizza la posizione iniziale
        initialPosition = transform.position;
    }

    private void LateUpdate()
    {
        // Mantiene la posizione iniziale
        transform.position = initialPosition;
    }

}
