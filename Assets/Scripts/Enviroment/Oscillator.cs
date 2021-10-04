using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 endPosition;
    [SerializeField] [Range(0,1)] float rangeDiapozone;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Wave();
    }

    void Wave() {
        if (period == 0) return;
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        rangeDiapozone = (rawSinWave + 1f) / 2f;

        Vector3 offset = endPosition * rangeDiapozone;
        transform.position = startingPosition + offset;
    }
}
