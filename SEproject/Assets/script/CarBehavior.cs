using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarBehavior : MonoBehaviour
{
    NavMeshAgent enemy; 
    GameObject player;  
    public ParticleSystem explosion; 
    public AudioClip explosionSound;
    private AudioSource audioSource;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>(); 
        player = GameObject.FindWithTag("Player");
        audioSource = gameObject.AddComponent<AudioSource>(); 
    }

    void Update()
    {
        ChasePlayer();
    }

    private Vector3 lastPosition;
    private float positionTolerance = 1f;

    private void ChasePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(lastPosition, player.transform.position) > positionTolerance)
            {
                enemy.SetDestination(player.transform.position);
                lastPosition = player.transform.position;
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            audioSource.PlayOneShot(explosionSound);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
