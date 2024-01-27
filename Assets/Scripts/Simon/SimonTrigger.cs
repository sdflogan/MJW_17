
using UnityEngine;

namespace MJW.Simon
{
    public class SimonTrigger : MonoBehaviour
    {
        [SerializeField] private SimonArea _area;

        private int _playerCounter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerCounter++;

                _area.UpdatePlayers(_playerCounter);
            }
        }

        private void OnTriggerExit(Collider other)
        {
         if (other.CompareTag("Player"))
            {
                _playerCounter--;

                _area.UpdatePlayers(_playerCounter);
            }   
        }
    }
}