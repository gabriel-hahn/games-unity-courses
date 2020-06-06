using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    enum State { Alive, Dying, Transcending };

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 1200f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip levelCompleted;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem levelCompletedParticles;
    [SerializeField] ParticleSystem deathParticles;

    Rigidbody rigiBody;
    AudioSource audioSource;
    State state = State.Alive;

    void Start()
    {
        rigiBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // Do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(levelCompleted);
        levelCompletedParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        mainEngineParticles.Stop();
        deathParticles.Play();
        Invoke("HandleRocketDeath", levelLoadDelay);
    }

    private void HandleRocketDeath()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        int currrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        bool isLastScene = SceneManager.sceneCountInBuildSettings == (currrentSceneIndex + 1);

        int nextSceneIndex = isLastScene ? 0 : (currrentSceneIndex + 1);

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void RespondToRotateInput()
    {
        float rotationThisFrameSpeed = rcsThrust * Time.deltaTime;
        rigiBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrameSpeed);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrameSpeed);
        }

        rigiBody.freezeRotation = false;
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else if (audioSource.isPlaying)
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigiBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        mainEngineParticles.Play();
    }
}
