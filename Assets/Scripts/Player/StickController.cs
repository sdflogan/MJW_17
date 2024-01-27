
using DG.Tweening;
using MJW.Items;
using MJW.Player.Inputs;
using System.Collections;
using UnityEngine;

namespace MJW.Player
{
    public class StickController : MonoBehaviour
    {
        #region Inspector

        [Header("References")]
        [SerializeField] private GameObject _tongue;
        [SerializeField] private PlayerBase _player;
        [SerializeField] private Transform _target;

        [Header("Config")]
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _pickRadius = 2.5f;

        [SerializeField] private float _pickCooldown = 1f;
        [SerializeField] private float _interactCooldown = 1f;

        [SerializeField] private float _tongueGoSeconds = 0.5f;
        [SerializeField] private float _tongueBackSeconds = 0.5f;

        #endregion

        #region Private properties

        private float _currentCooldown;
        private Coroutine _cooldownCoroutine;
        private Vector3 _tongueStartPosition;
        private Transform _tongueStartParent;

        private TorchItem _currentItem;
        private IPlayerInput _input;
        private PlayerType _playerType;

        #endregion

        #region Unity events

        private void Awake()
        {
            _tongueStartPosition = _tongue.transform.localPosition;
            _tongueStartParent = _tongue.transform.parent;
        }

        private void OnDestroy()
        {
            if (_cooldownCoroutine != null)
            {
                StopCoroutine(_cooldownCoroutine);
                _cooldownCoroutine = null;
            }
        }

        private void Update()
        {
            if (IsReady())
            {
                if (_input.GetInteractButton())
                {
                    InteractPressed();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _pickRadius);
        }

        #endregion

        #region Public methods

        public void Init(IPlayerInput playerInput)
        {
            _input = playerInput;
        }

        #endregion

        #region Private methods

        private void InteractPressed()
        {
            if (_currentItem != null)
            {
                if (_currentItem.Type == ItemType.Torch)
                {
                    ThrowStick();
                    Cooldown(_interactCooldown);
                }
            }
            else
            {
                FindItems();
            }
        }

        private void ThrowStick()
        {
            _currentItem.Throw(_target);
            _currentItem = null;
        }

        private bool IsReady()
        {
            return _currentCooldown <= 0;
        }

        private void Cooldown(float waitSeconds)
        {
            if (_cooldownCoroutine != null)
            {
                StopCoroutine(_cooldownCoroutine);
            }

            _cooldownCoroutine = StartCoroutine(CooldownCoroutine(waitSeconds));
        }

        private IEnumerator CooldownCoroutine(float waitSeconds)
        {
            _currentCooldown = waitSeconds;

            while (_currentCooldown > 0)
            {
                yield return new WaitForSeconds(0.1f);
                _currentCooldown -= 0.1f;
            }

            _cooldownCoroutine = null;
        }

        private void FindItems()
        {
            Collider[] detectedColliders = Physics.OverlapSphere(transform.position, _pickRadius, _targetLayer);

            for (int i=0; i<detectedColliders.Length; i++)
            {
                var itemFound = detectedColliders[i].gameObject.GetComponent<TorchItem>();

                if (itemFound != null && itemFound.IsFree)
                {
                    Pick(itemFound);
                    return;
                }
            }
        }

        private void Pick(TorchItem item)
        {
            Cooldown(_pickCooldown);

            if (_currentItem != null)
            {
                _currentItem.Drop();
            }
            
            PickAnimation(item);
        }

        private void PickAnimation(TorchItem item)
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(() => _tongue.transform.parent = item.transform);

            seq.Append(_tongue.transform.DOLocalMove(Vector3.zero, _tongueGoSeconds).SetEase(Ease.Linear));

            seq.AppendCallback(() =>
            {
                _tongue.transform.parent = _tongueStartParent;
                _currentItem = item;
                item.Pick(_tongue.transform);
            });

            seq.Append(_tongue.transform.DOLocalMove(_tongueStartPosition, _tongueBackSeconds).SetEase(Ease.Linear));

            seq.Play();
        }

        #endregion
    }
}