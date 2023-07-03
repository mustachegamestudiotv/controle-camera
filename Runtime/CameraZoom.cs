using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MustacheGameStudioTV.ControleCamera {

    public class CameraZoom : MonoBehaviour {

        [SerializeField]
        private float tamanhoMinimoCamera;

        [SerializeField]
        private float tamanhoMaximoCamera;

        [SerializeField]
        private float sensibilidade;

        private Camera cameraPrincipal;

        
        private void Start() {
            this.cameraPrincipal = Camera.main;
        }
        
        private void Update() {
            Zoom();
        }

        private void Zoom() {
            float zoom = -Input.mouseScrollDelta.y * this.sensibilidade;
            float tamanhoCamera = this.cameraPrincipal.orthographicSize; // Tamanho atual da camera
            tamanhoCamera += zoom;

            if (tamanhoCamera > this.tamanhoMaximoCamera)
            {
                tamanhoCamera = this.tamanhoMaximoCamera;
            }
            else if (tamanhoCamera < this.tamanhoMinimoCamera)
            {
                tamanhoCamera = this.tamanhoMinimoCamera;
            }
            this.cameraPrincipal.orthographicSize = tamanhoCamera;
        }

    }

}