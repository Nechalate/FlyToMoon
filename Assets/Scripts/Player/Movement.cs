using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] AudioClip engineSound;

    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;
    [SerializeField] ParticleSystem mainThrust;

    Rigidbody rocket;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rocket = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Thrust();
        Rotation();
    }
    // при нажатии клавиши W выполняется тяга "ракета начинает лететь".
    void Thrust() {
        if (Input.GetKey(KeyCode.W)) {
            StartMainThrust();
        }
        else {
            StopMainThrust();
        } 
    }
    // выполняется тяга. при тяге проигрывается звук. 
    void StartMainThrust() {
        if (!mainThrust.isPlaying) mainThrust.Play();
        rocket.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        if (audioSource.isPlaying != true) audioSource.PlayOneShot(engineSound);
    }
    // отключение эффекта огня при тяге. отключение звукаю
    void StopMainThrust() {
        mainThrust.Stop();
        audioSource.Stop();
    }
    // разворот ракеты при нажатиях клавиш A и D.
    void Rotation() {
        if (Input.GetKey(KeyCode.A)) {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D)) {
            RotateRight();
        }
        else {
            StopRotate();
        }
    }

    void RotateLeft() {
        if (!rightThrust.isPlaying) rightThrust.Play();
            ApplyRotate(rotateSpeed);
    }

    void RotateRight() {
        if (!leftThrust.isPlaying) leftThrust.Play();
            ApplyRotate(-rotateSpeed);
    }
    // остановка разворотов ракеты при бездействии.
    void StopRotate() {
        rightThrust.Stop();
        leftThrust.Stop();
    }
    // метод отвечает за исправление бага с физикой.
    void ApplyRotate(float rotateSpeed) {
        rocket.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        rocket.freezeRotation = false;
    }
}
