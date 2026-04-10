using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisionhandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] ParticleSystem crashVFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log($"{gameObject.name} Collided with: {other.gameObject.name}");
    }

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
        Debug.Log($"{gameObject.name} Triggered with: {other.gameObject.name}");
    }

    void StartCrashSequence()
    {
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<playerController>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
