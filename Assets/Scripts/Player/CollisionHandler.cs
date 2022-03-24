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
    // постоянный вызов метода "CheatConsole".
    void Update() {
        CheatConsole();
    }
    // проверка коллизии игрока.
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
    // последовательность при успешном прохождении уровня.
    void NextLevelSequence() {
        TransitioningControl();
        audioSource.Stop();
        winParticle.Play();
        audioSource.PlayOneShot(winSound);
        Invoke("NextLevel", reloadAction);
    }
    // последовательность при неудачном завершении уровня.
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
    // загрузка текущей сцены
    void ReloadLevel() {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }
    // загрузка следующего уровня, при прохождении всех уровней обнуляет счетчик и возвращает на самый первый уровень.
    void NextLevel() {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings) {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }
    // метод позволяет включить читы. при нажатии кнопки L - "Level" будет переход на следующий уровень. при нажатии кнопки C = "Collision" выключается столкновение с коллизией.
    void CheatConsole() {
        if (Input.GetKeyDown(KeyCode.L)) NextLevel();
        if (Input.GetKeyDown(KeyCode.C)) collisionCheat = !collisionCheat;
    }
}
