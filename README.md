# Controle Camera
Custom package, para projetos Unity, para controlar a movimentação e o zoom da câmera com o mouse em jogos 2D

## Funcionalidades
### Movimentação da câmera utilizando o mouse
- Uitlizando o botão esquerdo do mouse, você pode clicar e arrastar a câmera para outra posição em um mundo 2D
- Movimentação da câmera limitada em uma área predefinida

#### Como utilizar
- Adicione o script MovimentacaoCamera.cs no GameObject da câmera
- Defina a área limite de movimentação através da variáveis disponíveis no script MovimentacaoCamera no Editor da Unity
- Durante a definição da área limite da câmera, remenda-se que a aba Scene da Unity seja utilizada, para visualização dos Gizmos que representam a área disponível para a movimentação da câmera


### Zoom
- Aumentar e diminuir o zoom da câmera utilizando a rolagem (scroll) do mouse
- Limites de zoom mínimo e máximo podem ser predefinidos

#### Como utilizar
- Adicione o script CameraZoom.cs no GameObject da câmera
- Defina a os valores limites (zoom mínimo e zoom máximo) através da variáveis disponíveis no script MovimentacaoCamera no Editor da Unity