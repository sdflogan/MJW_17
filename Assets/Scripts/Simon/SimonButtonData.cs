
using UnityEngine;

namespace MJW.Simon
{
    [System.Serializable]
    public struct SimonButtonData
    {
        [SerializeField] private ButtonType _type;
        [SerializeField] private Sprite _icon;

        public ButtonType Type => _type;
        public Sprite Icon => _icon;
    }
}