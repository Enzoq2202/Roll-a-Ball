using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform playerTransform; // Referência à transformação do jogador
    public float speed = 5.0f; // Velocidade com que o inimigo segue o jogador

    private Rigidbody rb; // Rigidbody do inimigo
    private float fixedYPosition; // Posição Y fixa para o inimigo

    void Start()
    {
        // Guarda a posição Y inicial do inimigo para mantê-la constante
        fixedYPosition = transform.position.y;

        // Obtém o componente Rigidbody do inimigo
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // Calcula a direção para o jogador, mas mantém a posição Y do inimigo constante
            Vector3 direction = new Vector3(playerTransform.position.x, fixedYPosition, playerTransform.position.z) - transform.position;
            direction.y = 0; // Certifica-se de que a direção é apenas no plano XZ
            direction.Normalize(); // Normaliza para garantir que o movimento tenha velocidade constante

            // Calcula a nova velocidade do inimigo
            Vector3 newVelocity = direction * speed;

            // Aplica a velocidade ao Rigidbody
            rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);

            // Rotação
            if (rb.velocity.magnitude > 0.1f) // Apenas roda se estiver se movendo
            {
                float rotationMagnitude = rb.velocity.magnitude;
                Vector3 rotationDirection = Vector3.Cross(Vector3.up, rb.velocity).normalized;
                rb.angularVelocity = rotationDirection * rotationMagnitude;
            }
        }
    }
}
