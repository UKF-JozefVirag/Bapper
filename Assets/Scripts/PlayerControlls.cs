using UnityEngine;
using UnityEngine.UI;

public class PlayerControlls : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] [Range(0, 20)] private float maxSpeed = 3.0f;
    public GameObject GameOverPanel;
    private float stopper;
    protected float Timer;
    private Vector3 lastpos;
    public Slider StopTimer;
    private bool canRotate = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StopTimer.maxValue = 4;
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 1)
        {
            Timer = 0f;
            if (stopper != 0)
            {
                if (maxSpeed > 40)
                {
                    maxSpeed = 40;
                }
                else if (stopper != 0f)
                {
                    maxSpeed += (maxSpeed / 100) * 0.75f;
                }
                PlayerPrefs.SetFloat("PlayerSpeed", maxSpeed);
            }
        }
        


        if (Input.touchCount > 0 && StopTimer.value <= 3.9f)
        {
            stopper = 0f;
            rb.velocity = Vector3.zero;
            transform.position = lastpos;
            StopTimer.value += Time.deltaTime;
            canRotate = false;
        }

        else 
        {
            stopper = 4f;
            lastpos = transform.position;
            StopTimer.value -= Time.deltaTime;
            canRotate = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 vel = rb.velocity;

        // ak je maxspeed == 0, rychlost lopticky je 0
        if (Mathf.Approximately(maxSpeed, 0f)) rb.velocity = Vector3.zero;
        else
        {
            // tlaci lopticku po osi "z" silou 4nasobne vacsou ako maxspeed, kvoli okamzitemu zrychleniu po zastaveni
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
                Destroy(other.gameObject);
                break;
        }
    }
}
