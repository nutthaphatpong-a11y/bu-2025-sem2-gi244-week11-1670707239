using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform FocalPoint;
    private Rigidbody rb;

    private InputAction moveAction;
    private InputAction smashAction;
    private InputAction breakAction;

    public bool havePowerUp = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        smashAction = InputSystem.actions.FindAction("Smash");
        breakAction = InputSystem.actions.FindAction("Break");
    }

    // Update is called once per frame
    void Update()
    {
        var move = moveAction.ReadValue<Vector2>();
        rb.AddForce(move.y * speed * FocalPoint.forward);

        if (breakAction.IsPressed())
        {
            rb.linearVelocity = Vector3.zero;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            if (havePowerUp == true) {
                var rb = collision.gameObject.GetComponent<Rigidbody>();
                var dir = collision.transform.position - transform.position;
                dir.Normalize();
                rb.AddForce(100 * dir,ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            havePowerUp = true;
            Destroy(other.gameObject);
            if (countDownCoroutine != null) 
            {
            StopCoroutine(countDownCoroutine);
            }
            countDownCoroutine =  StartCoroutine(PowerUpCountDown());
        }

    }

    private Coroutine countDownCoroutine;
    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(10f);
        havePowerUp = false;
    }
}
