using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 5f;
    public float rotacaoSuave = 10f; // Velocidade da rotação suave
    public Transform cam; // Arraste a câmera no Inspector
    private Rigidbody player;
    private Vector3 ultimaDirecao = Vector3.forward; // Última direção válida

    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Inputs
        float inputLateral = Input.GetAxis("Horizontal");
        float inputFrontal = Input.GetAxis("Vertical");

        // Direção da câmera
        Vector3 cameraFrente = cam.transform.forward;
        Vector3 cameraLado = cam.transform.right;

        // Ajusta para não afetar a rotação no eixo Y
        cameraFrente.y = 0;
        cameraLado.y = 0;

        // Define a direção do movimento baseada na câmera
        Vector3 frenteRelativoCamera = inputFrontal * cameraFrente;
        Vector3 ladoRelativoCamera = inputLateral * cameraLado;
        Vector3 direcaoMovimento = frenteRelativoCamera + ladoRelativoCamera;

        // Verifica se há entrada de movimento
        if (direcaoMovimento.magnitude > 0.1f)
        {
            ultimaDirecao = direcaoMovimento.normalized; // Armazena a última direção válida
        }

        // Aplica rotação suave
        Quaternion rotacaoAlvo = Quaternion.LookRotation(ultimaDirecao);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, rotacaoSuave * Time.deltaTime);

        // Aplica movimento APENAS se houver input
        Vector3 velocidadeAtual = (direcaoMovimento.magnitude > 0.1f) ? ultimaDirecao * velocidade : Vector3.zero;
        player.linearVelocity = new Vector3(velocidadeAtual.x, player.linearVelocity.y, velocidadeAtual.z);
    }
}