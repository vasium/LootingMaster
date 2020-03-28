using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDisplayed;
    private string _currentText;

    void Start()
    {
        _textDisplayed.text = "";
        _currentText = "";
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        _textDisplayed.text = "";
        _currentText = "";
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _textDisplayed.text = "";
        _currentText = "";
    }

    public void PassTextToTypeWrite(string textToWrite, float delay)
    {
        StopAllCoroutines();
        _textDisplayed.text = "";
        _currentText = "";
        StartCoroutine(TypeWrite(textToWrite, delay));
    }

    //public void DisplayTextAtOnce(string textToDisplay)
    //{
    //    StopAllCoroutines();
    //    _textDisplayed.text = "";
    //    _currentText = "";
    //    textToDisplay = textToDisplay.Replace("|", "");
    //    _textDisplayed.text = textToDisplay;
    //}

    public void ClearText()
    {
        StopAllCoroutines();
        _textDisplayed.text = "";
        _currentText = "";
    }

    private IEnumerator TypeWrite(string textToWrite, float delay)
    {
        int startPosition = 0;
        for (int i = 0; i <= textToWrite.Length; i++)
        {
            _currentText = textToWrite.Substring(startPosition, i - startPosition);
            //inserted pause (invisible)
            if (_currentText.EndsWith("|"))
            {
                yield return new WaitForSeconds(0.2f);
            }
            //start a new page (invisible)
            else if (_currentText.EndsWith(">"))
            {
                _textDisplayed.text = "";
                startPosition = i;
                yield return null;
            }
            //punctuation pause (visible)
            else if (_currentText.EndsWith("."))
            {
                _currentText = _currentText.Replace("|", "");
                _currentText = _currentText.Replace(">", "");
                _textDisplayed.text = _currentText;
                yield return new WaitForSeconds(delay * 2f);
            }
            //normal text (visible)
            else
            {
                _currentText = _currentText.Replace("|", "");
                _currentText = _currentText.Replace(">", "");
                _textDisplayed.text = _currentText;
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
