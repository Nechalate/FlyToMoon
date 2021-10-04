using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelAnimation : MonoBehaviour
{
    [SerializeField] float rotateSpeedX = 0.1f;
    [SerializeField] float rotateSpeedY = 0.5f;
    [SerializeField] float rotateSpeedZ = 0.5f;

    void Update()
    {
        transform.Rotate(rotateSpeedX, rotateSpeedY, rotateSpeedZ);
    }
}
