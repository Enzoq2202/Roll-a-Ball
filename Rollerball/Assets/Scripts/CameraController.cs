using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    public float rotationSpeed = 5.0f; // Ajuste para tornar a rotação mais rápida ou mais lenta
    private float mouseX;

    void Start()
    {
        offset = transform.position - player.transform.position;
        // Bloqueia o cursor no centro da tela e o torna invisível
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        // Obtém o movimento horizontal do mouse e multiplica pela velocidade de rotação
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;

        // Calcula a rotação com base no input do mouse
        Quaternion camTurnAngle = Quaternion.AngleAxis(mouseX, Vector3.up);

        // Aplica a rotação ao offset
        Vector3 newPos = camTurnAngle * offset;

        // Atualiza a posição da câmera para que ela gire ao redor do jogador
        transform.position = player.transform.position + newPos;

        // Garante que a câmera sempre olhe para o jogador
        transform.LookAt(player.transform);
    }
}
