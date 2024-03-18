using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform playerTransform; // Referência à transformação do jogador
    public float speed = 5.0f; // Velocidade com que o inimigo segue o jogador

    private float fixedYPosition; // Posição Y fixa para o inimigo

    void Start()
    {
        // Guarda a posição Y inicial do inimigo para mantê-la constante
        fixedYPosition = transform.position.y;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Calcula a direção para o jogador, mas mantém a posição Y do inimigo constante
            Vector3 direction = new Vector3(playerTransform.position.x, fixedYPosition, playerTransform.position.z) - transform.position;
            direction.Normalize(); // Normaliza para garantir que o movimento tenha velocidade constante

            // Move o inimigo em direção ao jogador, mas apenas no plano horizontal
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
