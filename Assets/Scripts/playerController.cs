using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] InputAction movement;

    [Header("Gun Settings")]
    [SerializeField] InputAction fire;
    [SerializeField] GameObject[] guns;

    [Header("Movement Settings")]
    [SerializeField] float speed = 10f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 7f;

    [Header("Smooth Movement Between Points")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;

    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFireing();
    }

    void ProcessTranslation()
    {
        Vector2 inputVector = movement.ReadValue<Vector2>();

        xThrow = Mathf.Lerp(xThrow, inputVector.x, speed * Time.deltaTime); // smooth movement between x and y input
        yThrow = Mathf.Lerp(yThrow, inputVector.y, speed * Time.deltaTime);

        float xOffset = xThrow * speed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * speed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFireing()
    {
        if (fire.ReadValue<float>() > 0.5f)
        {
            //Debug.Log("Fireing!");
            SetGunsActive(true);
        }
        else
        {
            //Debug.Log("Not Fireing!");
            SetGunsActive(false);
        }
    }

    void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
