using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PUCPR.AutoRollCredits
{
    public class Credits : MonoBehaviour
    {
        #region Reference Variables
        ///Components Ref of Images
        public Image _headerImage;
        public Image _footerImage;

        ///Components Ref of TMP
        public TextMeshProUGUI _TMP_developers;
        public TextMeshProUGUI _TMP_special;
        public TextMeshProUGUI _TMP_finalTxt;

        ///Component Ref Rects
        public RectMask2D _rectMask;
        public bool _showMaskedContent;
        public RectTransform _content;
        #endregion

        #region Tool Variables
        ///Sprites content
        public Sprite _spriteHeader;
        public bool _spriteHeaderPrimaryColor = true;
        public Color _spriteHeaderColor = Color.white;
        public Sprite _spriteFooter;

        ///Colors content
        public Color _primaryColor = Color.white;
        public Color _secondaryColor = Color.white;
        public Color _defaultColor = Color.white;

        ///Text size content
        public float _titleSize;
        public float _subTitleSize;
        public float _defaultSize;
        public float _finalSize;

        ///Text content
        public CreditsTexts _Developers = new CreditsTexts();
        public CreditsSubTexts _Special = new CreditsSubTexts();
        public string _Final;

        ///Animation values
        [Min(.1f)] public float _speed = 1;
        [Min(1)] public float _waitForCallback = 1;
        #endregion

        public event Action OnEndCredits;

        private void Awake()
        {
            _rectMask.enabled = true;
            OnEndCredits += () => Debug.Log("OnEndCredits Callback");
        }

        private void Start()
        {
            StartCoroutine(MoveContent());
        }

        #region OnValidate
        private void OnValidate()
        {
            ApplySprites();

            ApplyTexts();

            ApplyExtras();
        }

        private void ApplySprites()
        {
            if (_headerImage)
            {
                _headerImage.sprite = _spriteHeader;

            }
            if (_spriteHeaderPrimaryColor)
                _headerImage.color = _primaryColor;
            else
                _headerImage.color = _spriteHeaderColor;

            if (_footerImage)
                _footerImage.sprite = _spriteFooter;
        }

        private void ApplyTexts()
        {
            ApplyDevelopers();
            ApplySpecial();
            ApplyFinal();
        }

        private void ApplyExtras()
        {
            if (_rectMask)
                _rectMask.enabled = !_showMaskedContent;

            if (_rectMask && _content)
                _content.localPosition =
                    new Vector3(0,
                    //_content.rect.height - (_rectMask.GetComponent<RectTransform>().rect.height/2),
                    -_rectMask.GetComponent<RectTransform>().rect.height / 2,
                    0);
        }

        private void ApplyDevelopers()
        {
            string content = string.Empty;

            content += SizedColoredText(_primaryColor, _titleSize, _Developers.title);
            content += LineSpace();

            foreach (CreditsSubTexts cST in _Developers.content)
            {
                content += SizedColoredText(_secondaryColor, _subTitleSize, cST.subTitle);
                content += Paragraph();

                foreach (string name in cST.names)
                {
                    content += SizedColoredText(_defaultColor, _defaultSize, name);
                    content += Paragraph();
                }
                content += Paragraph();
            }

            if (_TMP_developers)
                _TMP_developers.text = content;
        }

        private void ApplySpecial()
        {
            string content = string.Empty;

            content += SizedColoredText(_primaryColor, _titleSize, _Special.subTitle);
            content += LineSpace();

            foreach (string name in _Special.names)
            {
                content += SizedColoredText(_defaultColor, _defaultSize, name);
                content += Paragraph();
            }
            content += Paragraph();

            if (_TMP_special)
                _TMP_special.text = content;
        }

        private void ApplyFinal()
        {
            string content = string.Empty;

            content += Paragraph();
            content += SizedColoredText(_primaryColor, _finalSize, _Final);
            content += Paragraph();

            if (_TMP_finalTxt)
                _TMP_finalTxt.text = content;
        }

        /// Text Formating
        private string Paragraph() => $"\n";
        private string LineSpace() => $"{Paragraph()}{Paragraph()}";
        private string SizedColoredText(Color col, float size, string text) =>
            $"<color=#{ColorUtility.ToHtmlStringRGB(col)}><size={size.ToString().Replace(",", ".")}>{text}</size></color>";

        #endregion

        private IEnumerator MoveContent()
        {
            yield return new WaitForSeconds(1);
            float target = _content.rect.height - (_rectMask.GetComponent<RectTransform>().rect.height / 2);

            do
            {
                _content.localPosition = Vector3.MoveTowards(
                    _content.localPosition,
                    new Vector3(0, target, 0),
                    _speed * Time.deltaTime);

                yield return new WaitForEndOfFrame();

            } while (Mathf.Abs(_content.localPosition.y - target) > .1f);

            Debug.Log("The End!");
            yield return new WaitForSeconds(_waitForCallback);
            OnEndCredits?.Invoke();

            yield return null;
        }
    }
}
