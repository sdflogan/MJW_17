
using MJW.Player.Inputs;
using System;
using UnityEngine;

namespace MJW.Player
{
    public class PlayerBase : MonoBehaviour
    {
        #region Inspector Properties

        [Header("References")]
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Animator _animator;
        [SerializeField] private StickController _pickController;

        [SerializeField] private PlayerType _type;

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxRotationSpeed;

        #endregion

        #region Public Properties

        public float MaxSpeed => _maxSpeed;
        public float MaxRotationSpeed => _maxRotationSpeed;

        #endregion

        #region Private properties

        private float _currentSpeed;
        private float _currentVelocity;

        private float _moveX;
        private float _moveY;

        private Camera _camera;

        private IPlayerInput _playerInput;

        #endregion

        #region Unity Events

        private void Awake()
        {
            _camera = Camera.main;

            if (_type == PlayerType.Player_1)
            {
                _playerInput = new PlayerInput_1();
            }
            else
            {
                _playerInput = new PlayerInput_2();
            }

            _pickController.Init(_playerInput);
        }

        private void Update()
        {
            GetInputs();
            UpdateCharacterAnimator();
        }

        private void FixedUpdate()
        {
            MoveCharacter();
            RotateCharacter();
        }

        #endregion

        public void TriggerTongue()
        {
            _animator.SetTrigger("Pick");
        }

        #region Private methods

        private void GetInputs()
        {
            _moveX = _playerInput.GetAxisHorizontal();
            _moveY = _playerInput.GetAxisVertical();
        }

        private void UpdateCharacterAnimator()
        {
            _animator.SetFloat("Speed", _currentSpeed, 0.1f, Time.deltaTime);
        }

        private void RotateCharacter()
        {
            // Rotación X-Z del Input
            Vector3 rotation = new Vector3(_moveX, 0f, _moveY);

            // Rotamos el vector para que se ajuste a la rotación de la cámara
            rotation = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * rotation;

            if (rotation != Vector3.zero)
            {
                // Obtenemos la rotación final
                Quaternion quatR = Quaternion.LookRotation(rotation);

                // Interpolación para que la rotación se realice de forma suave
                _rb.MoveRotation(Quaternion.Lerp(_rb.rotation, quatR, Time.deltaTime * MaxRotationSpeed));
            }
        }

        private void MoveCharacter()
        {
            // Movimiento X-Z del input
            Vector3 movement = new Vector3(_moveX, 0f, _moveY);

            // Obtenemos el desplazamiento del input
            _currentSpeed = (movement.magnitude > 1 ? 1 : movement.magnitude);

            // Normalizamos y lo hacemos proporcional a la velocidad por segundo
            movement = movement.normalized * MaxSpeed * Time.deltaTime;

            // Rotamos el vector para que se ajuste a la rotación de la cámara
            movement = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * movement;

            // Desplazamos el personaje
            _rb.MovePosition(transform.position + (movement * _currentSpeed));
        }

        #endregion
    }
}