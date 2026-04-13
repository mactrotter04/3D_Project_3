using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    GameObject particlesParent;

    [SerializeField] int scorePerKill = 10;
    [SerializeField] int hitPoints = 4;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
        particlesParent = GameObject.FindWithTag("ParticlesParent");

    }

    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
        
        Debug.Log($"{name} was killed by a particle from {other.name}");
    }

    void ProcessHit() //reusable method for processing a hit on the enemy, can be called from other places if needed
    {
        GameObject hit = Instantiate(hitVFX, transform.position, Quaternion.identity);
        hit.transform.parent = particlesParent.transform;
        hitPoints--;
        Debug.Log($"{name} was hit! Remaining hit points: {hitPoints}");
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = particlesParent.transform;
        scoreBoard.IncreaseScore(scorePerKill);
        Destroy(gameObject);
    }
}
