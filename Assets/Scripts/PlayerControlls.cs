using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] [Range(0, 20)] private float maxSpeed = 3.0f;
    public GameObject GameOverPanel;
    private float stopper;
    protected float Timer;
    private Vector3 lastpos;
    private bool canRotate = true;
    public AudioClip death_clip, orb_clip;
    private AudioSource audioSource;
    private bool stopped = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 1){
            Timer = 0f;
            if (stopper != 0){
                if (maxSpeed > 40){
                    maxSpeed = 40;
                }
                else if (stopper != 0f) {
                    maxSpeed += (maxSpeed / 100) * 0.75f;
                }
                PlayerPrefs.SetFloat("PlayerSpeed", maxSpeed);
            }
        }
        if (Input.touchCount > 0){
            stopper = 0f;
            rb.velocity = Vector3.zero;
            transform.position = lastpos;
            canRotate = false;
            PlayerPrefs.SetInt("Stopped", 1);
        }
        else {
            stopper = 4f;
            lastpos = transform.position;
            canRotate = true;
            PlayerPrefs.SetInt("Stopped", 0);
        }
    }

    private void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        if (Mathf.Approximately(maxSpeed, 0f)) rb.velocity = Vector3.zero;
        else
        {
            rb.AddForce(0f, 0f, maxSpeed * stopper);
            if (canRotate) rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(maxSpeed, 0f, 0f)));
            if (vel.magnitude >= maxSpeed) rb.velocity = vel.normalized * maxSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Obstacle":
                if (PlayerPrefs.GetInt("Revived") == 1)
                {
                    Destroy(collision.collider);
                }
                else
                {
                    Time.timeScale = 0;
                    GameOverPanel.SetActive(true);
                    audioSource.PlayOneShot(death_clip, PlayerPrefs.GetInt("Sound"));
                }
                break;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Currency":
                PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency", 0) + 1);
                audioSource.PlayOneShot(orb_clip, PlayerPrefs.GetInt("Sound"));
                Destroy(other.gameObject);
                break;
        }
    }

}
