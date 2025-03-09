using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMovement : MonoBehaviour
{
    public enum CarAI { Drunk, Direct, Intercept, Overshoot, Swerve, Blockade };
    public CarAI carAI = CarAI.Direct;

    NavMeshAgent enemy;
    GameObject player;
    public ParticleSystem explosion;
    public AudioClip explosionSound;

    private AudioSource audioSource;
    private float swerveDirection = 1f;
    private float swerveTimer = 0f;
    public float swerveInterval = 1f;
    public float swerveAmount = 5f;

    private bool isBlocking = false;
    public float blockadeDistance = 20f;
    public float reBlockadeDistance = 30f;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        carAI = (CarAI)Random.Range(0, System.Enum.GetValues(typeof(CarAI)).Length);

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (carAI == CarAI.Drunk)
        {
            carAI = (CarAI)Random.Range(1, System.Enum.GetValues(typeof(CarAI)).Length);
        }

        switch (carAI)
        {
            case CarAI.Direct:
                Direct();
                break;
            case CarAI.Intercept:
                Intercept();
                break;
            case CarAI.Overshoot:
                Overshoot();
                break;
            case CarAI.Swerve:
                Swerve();
                break;
            case CarAI.Blockade:
                Blockade();
                break;
        }
    }

    private void Direct()
    {
        enemy.SetDestination(player.transform.position);
    }

    private void Intercept()
    {
        Vector3 playerVelocity = player.GetComponent<Rigidbody>().velocity;
        Vector3 playerPosition = player.transform.position;
        Vector3 carPosition = transform.position;

        if (playerVelocity.magnitude > 0)
        {
            float timeToIntercept = Vector3.Distance(carPosition, playerPosition) / enemy.speed;
            Vector3 predictedPosition = playerPosition + playerVelocity * timeToIntercept;
            enemy.SetDestination(predictedPosition);
        }
        else
        {
            enemy.SetDestination(playerPosition);
        }
    }

    void Overshoot()
    {
        Vector3 playerVelocity = player.GetComponent<Rigidbody>().velocity;
        Vector3 playerPosition = player.transform.position;
        Vector3 carPosition = transform.position;

        if (playerVelocity.magnitude > 0)
        {
            Vector3 overshootDirection = playerVelocity.normalized;
            float overshootDistance = 10f;
            Vector3 overshootPosition = playerPosition + overshootDirection * overshootDistance;

            enemy.SetDestination(overshootPosition);
        }
        else
        {
            enemy.SetDestination(playerPosition);
        }
    }

    void Swerve()
    {
        swerveTimer += Time.deltaTime;

        if (swerveTimer >= swerveInterval)
        {
            swerveTimer = 0f;
            swerveDirection *= -1f;
        }

        Vector3 targetPosition = player.transform.position;
        Vector3 swerveOffset = transform.right * swerveDirection * swerveAmount;
        enemy.SetDestination(targetPosition + swerveOffset);
    }

    void Blockade()
    {
        if (!isBlocking)
        {
            Vector3 blockadePosition = player.transform.position + player.GetComponent<Rigidbody>().velocity.normalized * blockadeDistance;
            enemy.SetDestination(blockadePosition);
            isBlocking = true;
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer >= reBlockadeDistance)
            {
                isBlocking = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car" || collision.gameObject.tag == "Obstacle")
        {
            audioSource.PlayOneShot(explosionSound);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}