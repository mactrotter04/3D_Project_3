using UnityEngine;
using UnityEngine.Rendering;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 2f;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
