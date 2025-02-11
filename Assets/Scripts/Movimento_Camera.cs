using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Movimento_Camera : MonoBehaviour
{
    [SerializeField] private Transform player;
    // Sensibilidade do mouse na movimentação da câmera
    [SerializeField] private float sensibilidadeX = 2f, sensibilidadeY = 2f; 
    // Ângulo máximo para movimentação da câmera verticalmente
    [SerializeField] private float maxY = 45f, minY = -45f;
    // Ângulo incial da câmera
    [SerializeField] private float rotacaoX = 0f, rotacaoY = 0f;
    //Distancia da câmera ao player
    [SerializeField] private float distancia_cameraZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()

    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Captura de movimento do mouse
        rotacaoX += Input.GetAxis("Mouse X") * sensibilidadeX;
        rotacaoY += Input.GetAxis("Mouse Y") * sensibilidadeY;

        // Restringe rotação vertical
        rotacaoY = Mathf.Clamp(rotacaoY, minY, maxY);

        //Calcula nova posição do mouse
        Quaternion rotation = Quaternion.Euler(rotacaoY, rotacaoX, 0);
        Vector3 offset = new Vector3(0, 0, distancia_cameraZ);
        transform.position = player.position + rotation * offset;
        transform.LookAt(player.position);


    }
}
