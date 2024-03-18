using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public GameObject winTextObject;
    public GameObject[] pickups; // Todos os pickups disponíveis
    private Vector2 movementInput;
    public float speed = 10f;
    public float jumpForce = 6f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool isGrounded;
    private int count;
    private float startTime;
    private float timeLimit = 30f;
    private List<GameObject> activePickups = new List<GameObject>(); // Lista para os pickups ativos

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        startTime = Time.time;
        UpdateTimerText(timeLimit);

        // Encontra todos os objetos com a tag "PickUp" na cena e os atribui ao array de pickups
        pickups = GameObject.FindGameObjectsWithTag("PickUp");

        // Desativa todos inicialmente, se necessário
        foreach (GameObject pickup in pickups)
        {
            pickup.SetActive(false);
        }

        // Ativa um conjunto inicial de pickups aleatórios
        ActivateRandomPickups(10);
    }


    void Update()
    {
        float timeLeft = timeLimit - (Time.time - startTime);
        if (timeLeft <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            UpdateTimerText(timeLeft); 
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        // Verifica se o jogador está no chão antes de permitir o pulo
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Atualiza o estado de isGrounded
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count % 10 == 0 && count > 0)
        {
            // Aumenta o tempo em 33 segundos -> 3 segundos de cooldown, entao 30 segundos
            timeLimit += 33f;
            UpdateTimerText(timeLimit - (Time.time - startTime)); // Atualiza o texto do timer imediatamente
            StartCoroutine(ReappearPickupsWithDelay(3));
        }
    }

    IEnumerator ReappearPickupsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Cooldown de 3 segundos
        ActivateRandomPickups(10); // Ativa 10 pickups aleatórios
    }

    void UpdateTimerText(float timeLeft)
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementInput.x, 0.0f, movementInput.y);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;
        rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed), Time.fixedDeltaTime * speed);

        // Aplica uma gravidade personalizada
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Derrota! O inimigo te alcançou.");
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a bolinha colidiu com o chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void InitializePickups()
    {
        foreach (var pickup in pickups)
        {
            pickup.SetActive(false); // Desativa todos os pickups inicialmente
        }
        ActivateRandomPickups(10); // Ativa 10 pickups aleatórios
    }

    private void ActivateRandomPickups(int amount)
    {
        List<GameObject> inactivePickups = new List<GameObject>(pickups);
        inactivePickups.RemoveAll(pickup => pickup.activeSelf); // Remove todos os já ativos

        for (int i = 0; i < amount && inactivePickups.Count > 0; i++)
        {
            int index = Random.Range(0, inactivePickups.Count);
            inactivePickups[index].SetActive(true);
            inactivePickups.RemoveAt(index);
        }
    }
}
