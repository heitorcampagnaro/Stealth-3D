using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 5f;
    public float rotacaoSuave = 10f; // Velocidade da rota��o suave
    public Transform cam; // Arraste a c�mera no Inspector
    private Rigidbody player;
    private Vector3 ultimaDirecao = Vector3.forward; // �ltima dire��o v�lida

    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Inputs
        float inputLateral = Input.GetAxis("Horizontal");
        float inputFrontal = Input.GetAxis("Vertical");

        // Dire��o da c�mera
        Vector3 cameraFrente = cam.transform.forward;
        Vector3 cameraLado = cam.transform.right;

        // Ajusta para n�o afetar a rota��o no eixo Y
        cameraFrente.y = 0;
        cameraLado.y = 0;

        // Define a dire��o do movimento baseada na c�mera
        Vector3 frenteRelativoCamera = inputFrontal * cameraFrente;
        Vector3 ladoRelativoCamera = inputLateral * cameraLado;
        Vector3 direcaoMovimento = frenteRelativoCamera + ladoRelativoCamera;

        // Verifica se h� entrada de movimento
        if (direcaoMovimento.magnitude > 0.1f)
        {
            ultimaDirecao = direcaoMovimento.normalized; // Armazena a �ltima dire��o v�lida
        }

        // Aplica rota��o suave
        Quaternion rotacaoAlvo = Quaternion.LookRotation(ultimaDirecao);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, rotacaoSuave * Time.deltaTime);

        // Aplica movimento APENAS se houver input
        Vector3 velocidadeAtual = (direcaoMovimento.magnitude > 0.1f) ? ultimaDirecao * velocidade : Vector3.zero;
        player.linearVelocity = new Vector3(velocidadeAtual.x, player.linearVelocity.y, velocidadeAtual.z);
    }
}