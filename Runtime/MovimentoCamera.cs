using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MustacheGameStudioTV.ControleCamera {

    public class MovimentoCamera : MonoBehaviour {

        private bool movendo;
        private Vector2 posicaoAnterior;
        private Camera cameraPrincipal;

        [SerializeField]
        private Rect areaLimite;


        private void Start() {
            this.movendo = false;
            this.cameraPrincipal = Camera.main;
        }
        
        private void Update() {
            Mover();
            LimitarMovimentacao();
        }

        private void OnDrawGizmos() {
            // Linha vertical esquerda
            Gizmos.DrawLine(new Vector3(this.areaLimite.x, this.areaLimite.y, 0), // Ponto inicial
                            new Vector3(this.areaLimite.x, this.areaLimite.y + this.areaLimite.height, 0)); // Ponto final

            // Linha vertical direita
            Gizmos.DrawLine(new Vector3(this.areaLimite.x + this.areaLimite.width, this.areaLimite.y, 0), // Ponto inicial
                            new Vector3(this.areaLimite.x + this.areaLimite.width, this.areaLimite.y + this.areaLimite.height, 0)); // Ponto final

            // Linha horizontal inferior
            Gizmos.DrawLine(new Vector3(this.areaLimite.x, this.areaLimite.y, 0), // Ponto inicial
                            new Vector3(this.areaLimite.x + this.areaLimite.width, this.areaLimite.y, 0)); // Ponto final

            // Linha horizontal superior
            Gizmos.DrawLine(new Vector3(this.areaLimite.x, this.areaLimite.y + this.areaLimite.height, 0), // Ponto inicial
                            new Vector3(this.areaLimite.x + this.areaLimite.width, this.areaLimite.y + this.areaLimite.height, 0)); // Ponto final
        }

        private void Mover() {
            // Caso o botao esquerdo do mouse esteja pressionado
            if (Input.GetMouseButton(0)) {
                if (this.movendo) {
                    // Ja estava movendo, vou comprarar a posisao atual
                    // do mouse com a posisao anteior
                    Vector2 posicaoMouse = Input.mousePosition; // Posicao atual do mouse
                    Vector2 posicaoMouseMundo2d = this.cameraPrincipal.ScreenToWorldPoint(posicaoMouse); // Posisao do mouse no mundo 2d

                    float distanciaX = this.posicaoAnterior.x - posicaoMouseMundo2d.x;
                    float distanciaY = this.posicaoAnterior.y - posicaoMouseMundo2d.y;

                    Vector3 posicaoCamera = this.transform.position;
                    posicaoCamera.x += distanciaX;
                    posicaoCamera.y += distanciaY;
                    this.transform.position = posicaoCamera;

                    posicaoMouseMundo2d = this.cameraPrincipal.ScreenToWorldPoint(posicaoMouse); // Posicao do mouse no mundo 2d
                    this.posicaoAnterior = posicaoMouseMundo2d;
                } else {
                    // Ainda nao esta movendo
                    this.movendo = true;
                    Vector2 posicaoMouse = Input.mousePosition; // Posicao atual do mouse
                    Vector2 posicaoMouseMundo2d = this.cameraPrincipal.ScreenToWorldPoint(posicaoMouse); // Posicao do mouse no mundo 2d
                    this.posicaoAnterior = posicaoMouseMundo2d;
                }
            } else {
                // Botao do mouse nao esta pressionado
                this.movendo = false;
            }
        }

        private void LimitarMovimentacao() {
            Vector3 posicaoCamera = this.transform.position;

            Vector2 limiteCameraEsquerdaInferior = this.cameraPrincipal.ViewportToWorldPoint(Vector2.zero); // (0, 0)
            Vector2 limiteCameraDireitaSuperior = this.cameraPrincipal.ViewportToWorldPoint(Vector2.one); // (1, 1)

            // Movimentacao horizontal (eixo x)
            Vector2 posicaoAreaLimiteEsquerda = new Vector2(this.areaLimite.x, posicaoCamera.y);
            Vector2 posicaoAreaLimiteEsquerdaViewport = this.cameraPrincipal.WorldToViewportPoint(posicaoAreaLimiteEsquerda);

            if (posicaoAreaLimiteEsquerdaViewport.x > 0) { // Comparacao com o lado esquerdo da area limite
              // Camera passou do limite esquerdo
              // Mover a camera para a direita
                float distanciaX = posicaoAreaLimiteEsquerda.x - limiteCameraEsquerdaInferior.x;
                posicaoCamera.x += distanciaX;
                this.transform.position = posicaoCamera;
            } else {
                Vector2 posicaoAreaLimiteDireita = new Vector2(this.areaLimite.x + this.areaLimite.width, posicaoCamera.y);
                Vector2 posicaoAreaLimiteDireitaViewport = this.cameraPrincipal.WorldToViewportPoint(posicaoAreaLimiteDireita);

                if (posicaoAreaLimiteDireitaViewport.x < 1) { // Comparacao com o lado direito da area limite
                  // Camera passou do limite direito
                  // Mover a camera para a esquerda
                    float distanciaX = posicaoAreaLimiteDireita.x - limiteCameraDireitaSuperior.x;
                    posicaoCamera.x += distanciaX;
                    this.transform.position = posicaoCamera;
                }
            }

            // Movimentacao vertical (eixo Y)
            posicaoCamera = this.transform.position;
            Vector2 posicaoAreaLimiteInferior = new Vector2(posicaoCamera.x, this.areaLimite.y);
            Vector2 posicaoAreaLimiteInferiorViewport = this.cameraPrincipal.WorldToViewportPoint(posicaoAreaLimiteInferior);

            if (posicaoAreaLimiteInferiorViewport.y > 0) { // Comparacao com a parte inferior da area limite
              // Camera passou do limite inferior
              // Mover a camera para a cima
                float distanciaY = posicaoAreaLimiteInferior.y - limiteCameraEsquerdaInferior.y;
                posicaoCamera.y += distanciaY;
                this.transform.position = posicaoCamera;
            } else { // Comparacao com a parte superior da area limite
                Vector2 posicaoAreaLimiteSuperior = new Vector2(posicaoCamera.x, this.areaLimite.y + this.areaLimite.height);
                Vector2 posicaoAreaLimiteSuperiorViewport = this.cameraPrincipal.WorldToViewportPoint(posicaoAreaLimiteSuperior);

                if (posicaoAreaLimiteSuperiorViewport.y < 1) {
                    float distanciaY = posicaoAreaLimiteSuperior.y - limiteCameraDireitaSuperior.y;
                    posicaoCamera.y += distanciaY;
                    this.transform.position = posicaoCamera;
                }
            }
        }
    }

}