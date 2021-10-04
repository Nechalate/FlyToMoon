using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadAction = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip winSound;

    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem winParticle;

    Movement control;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionCheat = false;

    void Start() {
        control = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        CheatConsole();
    }
    // Проверка коллизии игрока.
    private void OnCollisionEnter(Collision other) {
        if (isTransitioning || collisionCheat) return;

        switch (other.gameObject.tag) {
            case "Launch":
                break;
            case "Finish":
                NextLevelSequence();
                break;
            case "Fuel":
                Debug.Log("U take fuel!");
                break;
            default:
                CrashSequence();
                break;
        }
    }
    // Последовательность при успешном прохождении уровня.
    void NextLevelSequence() {
        TransitioningControl();
        audioSource.Stop();
        winParticle.Play();
        audioSource.PlayOneShot(winSound);
        Invoke("NextLevel", reloadAction);
    }
    // Последовательность при неудачном завершении уровня.
    void CrashSequence() {
        TransitioningControl();
        audioSource.Stop();
        crashParticle.Play();
        audioSource.PlayOneShot(crashSound);
        Invoke("ReloadLevel", reloadAction);
    }
    void TransitioningControl() {
        isTransitioning = true;
        control.enabled = false;
    }
    // Загрузка текущей сцены
    void ReloadLevel() {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }
    // Загрузка следующего уровня, при прохождении всех уровней обнуляет счетчик и возвращает на самый первый уровень.
    void NextLevel() {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings) {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }

    void CheatConsole() {
        if (Input.GetKeyDown(KeyCode.L)) NextLevel();
        if (Input.GetKeyDown(KeyCode.C)) collisionCheat = !collisionCheat;
    }
}
