using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    internal class MessageBoard : EntityWithTimer
    {
        [SerializeField] private Text _Text;
        [SerializeField] private float _TextSpeed = 20.0f;
        [SerializeField] private int _SymbolsPerTick = 3;
        [SerializeField] private RectTransform _TextField;

        private const string _TextTimerName = "textspeedtimer";

        private string _fulltext = "";
        private int _textLengthToShow = 0;

        private const int _BaseHeight = 100;
        private const int _SymbolsPerLine = 35;
        private const int _LineHeight = 25;

        public void ShowMessage(string text)
        {
            _textLengthToShow = 0;
            _fulltext = text;

            int linesNumber = Mathf.CeilToInt(_fulltext.Length / _SymbolsPerLine) + 1;
            _TextField.sizeDelta = new Vector2(_TextField.sizeDelta.x, _BaseHeight + _LineHeight * linesNumber);

            AddTimer(_TextTimerName, 1.0f / _TextSpeed);
            AddCallback(_TextTimerName, UpdateText);

            gameObject.SetActive(true);
        }

        private void UpdateText()
        {
            if (_textLengthToShow == _fulltext.Length)
            {
                RemoveTimer(_TextTimerName);
                return;
            }

            _textLengthToShow += _SymbolsPerTick;
            if (_textLengthToShow > _fulltext.Length) _textLengthToShow = _fulltext.Length;

            _Text.text = _fulltext.Substring(0, _textLengthToShow);
        }

        public void HideMessage()
        {
            _fulltext = "";
            _Text.text = "";

            _TextField.sizeDelta = new Vector2(_TextField.sizeDelta.x, _BaseHeight);

            RemoveTimer(_TextTimerName);
            gameObject.SetActive(false);
        }
    }
}
