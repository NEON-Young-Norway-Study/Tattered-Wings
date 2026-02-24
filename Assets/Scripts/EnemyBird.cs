using UnityEngine;

public class EnemyBird : MonoBehaviour
{
    public enum PatrolBehaviour
    {
        Loop,
        BackAndForth
    }
    public Transform[] waypoints;
    [SerializeField] private PatrolBehaviour patrolBehaviour = PatrolBehaviour.Loop;
    [SerializeField] private float speed = 2.0f;
    private int waypointIndex = 0;
    private bool reverse = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool dmgToPlayer = true;
    private AudioSource audioSource;

    public float minInterval = 2f;   // Intervalo mínimo de tiempo entre sonidos
    public float maxInterval = 5f;   // Intervalo máximo de tiempo entre sonidos

    private float nextPlayTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        // Establece el primer tiempo aleatorio para reproducir el sonido
        SetNextPlayTime();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();

        // Comprueba si ha llegado el momento de reproducir el sonido
        if (Time.time >= nextPlayTime)
        {
            PlaySound();
            SetNextPlayTime();  // Calcula el próximo intervalo de tiempo
        }
    }

    // Establece el próximo tiempo de reproducción
    void SetNextPlayTime()
    {
        float interval = Random.Range(minInterval, maxInterval);
        nextPlayTime = Time.time + interval;
    }

    // Reproduce el sonido
    void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se ha asignado un AudioSource.");
        }
    }


    private void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[waypointIndex];

        // Turn towards the waypoint
        //RotateTowardsWaypoint(targetWaypoint);

        // Move to the next waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        spriteRenderer.flipX = reverse;

        // If tank reached the waypoint, move to the next one
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            switch (patrolBehaviour)
            {
                // Handle behavior based on patrol type
                case PatrolBehaviour.Loop:
                    waypointIndex = (waypointIndex + 1) % waypoints.Length;
                    break;
                case PatrolBehaviour.BackAndForth:
                    if (!reverse)
                    {
                        waypointIndex++;
                        if (waypointIndex >= waypoints.Length)
                        {
                            waypointIndex = waypoints.Length - 2;
                            reverse = true;
                        }
                    }
                    else
                    {
                        waypointIndex--;
                        if (waypointIndex < 0)
                        {
                            waypointIndex = 1;
                            reverse = false;
                        }
                    }
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && dmgToPlayer)
        {
           
            var playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage();
        }
    }

}
