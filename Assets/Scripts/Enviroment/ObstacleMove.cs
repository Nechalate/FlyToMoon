using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private float distance;

    void Update()
    {
        if (distance > 2) {
            transform.Translate(new Vector3 (0, -moveSpeed, 0) * Time.deltaTime);
            if (distance > 4) {
                distance = 0;
            }
        }
        else transform.Translate(new Vector3 (0, moveSpeed, 0) * Time.deltaTime);
    }

    void FixedUpdate() {
        distance += 0.01f;
    }
}
