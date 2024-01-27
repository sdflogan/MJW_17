
using UnityEngine;
using UnityEngine.UI;

namespace MJW.Simon.UI
{
    public class SimonButtonUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _result;

        private ButtonType _type;

        public void Setup(ButtonType buttonType)
        {
            _result.enabled = false;
            _icon.sprite = SimonManager.Instance.GetIcon(buttonType);
            _type = buttonType;
        }

        public bool Check(ButtonType input)
        {
            return input == _type;
        }

        public void Success()
        {
            _result.enabled = true;
            _result.color = SimonManager.Instance.SuccessColor;
        }

        public void Error()
        {
            _result.enabled = true;
            _result.color = SimonManager.Instance.ErrorColor;
        }
    }
}