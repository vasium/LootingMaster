using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuAndIntro : MonoBehaviour
{    
    public TextMeshProUGUI _textDisplayed;
    public float delay = 0.02f;
    [TextArea]
    public string _fullTextFirst;
    private string currentText;

    public Button _startButton;
    public Button _instructionsButton;
    public Button _exitButton;
    public Button _backButton;


    public AnimationCurve _imageAlphaCurve;

    public AudioSource _buttonAudioSource;
    public AudioClip[] _buttonSounds;
    


    public GameObject _menuPhoto;
    public AudioClip[] _menuPhotoSounds;
    public AudioSource _imageOnAudioSource;
    public AudioSource _imageOnAudioSourceBass;
    public CanvasRenderer _UIMaterialToTarget;
    public Image _eRRMAP;
    private Material _actualUIMaterial;

    public GameObject _titleText;

    public GameObject _roomObjectsToInactivate;

    public GameObject _start;
    public GameObject _instructions;
    public GameObject _exit;
    public GameObject _back;

    public GameObject _instructionsImage;

    private Color _transparentWhite = new Color(1f, 1f, 1f, 0f);

    private void Awake()
    {
        _roomObjectsToInactivate.SetActive(true);
        _titleText.SetActive(false);
        _instructionsImage.SetActive(false);
        _instructions.SetActive(false);
        _start.SetActive(false);
        _exit.SetActive(false);
        _back.SetActive(false);

        currentText = "";
        _textDisplayed.enabled = false;
        _menuPhoto.SetActive(true);
        _actualUIMaterial = Instantiate(_eRRMAP.material);
        _actualUIMaterial.SetFloat("_DissolveAmount", 1f);
        _actualUIMaterial.SetFloat("_DissolveWidth", 0f);
        _eRRMAP.material = _actualUIMaterial;
        StartCoroutine(DissolveIn());
    }

    public void InstructionsButtonIsPressed()
    {
        _buttonAudioSource.clip = _buttonSounds[Random.Range(0, 3)];
        _buttonAudioSource.pitch = 0.9f;
        _buttonAudioSource.Play();
        _instructionsButton.interactable = false;
        _instructions.SetActive(false);
        _startButton.interactable = false;
        _start.SetActive(false);
        _exitButton.interactable = false;
        _exit.SetActive(false);
        _back.SetActive(true);
        _backButton.interactable = true;
        _textDisplayed.text = "";
        _instructionsImage.SetActive(true);
    }

    public void StartButtonIsPressed()
    {
        _buttonAudioSource.clip = _buttonSounds[Random.Range(0, 3)];
        _buttonAudioSource.pitch = 0.8f;
        _buttonAudioSource.Play();
        _startButton.interactable = false;
        _start.SetActive(false);
        _instructionsButton.interactable = false;
        _instructions.SetActive(false);
        _exitButton.interactable = false;
        _exit.SetActive(false);
        StopAllCoroutines();
        _textDisplayed.text = "";
        _menuPhoto.SetActive(false);
        _roomObjectsToInactivate.SetActive(false);
        _titleText.SetActive(false);
        StartCoroutine(StartGame());
    }

    public void ExitButtonIsPressed()
    {
        
        StartCoroutine(ExitGame());
    }

    private IEnumerator ExitGame()
    {
        _buttonAudioSource.clip = _buttonSounds[Random.Range(0, 3)];
        _buttonAudioSource.pitch = 0.6f;
        _buttonAudioSource.Play();
        _startButton.interactable = false;
        _start.SetActive(false);
        _instructionsButton.interactable = false;
        _instructions.SetActive(false);
        _exitButton.interactable = false;
        _exit.SetActive(false);
        yield return new WaitForSeconds(0.3F);
        Application.Quit();
    }

    public void BackToMainButtonIsPressed()
    {
        _buttonAudioSource.clip = _buttonSounds[Random.Range(0, 3)];
        _buttonAudioSource.pitch = 1f;
        _buttonAudioSource.Play();
        _instructionsImage.SetActive(false);
        _instructions.SetActive(true);
        _instructionsButton.interactable = true;
        _start.SetActive(true);
        _startButton.interactable = true;
        _exit.SetActive(true);
        _exitButton.interactable = true;
        _backButton.interactable = false;
        _back.SetActive(false);
    }

    private IEnumerator StartGame()
    {
        _textDisplayed.enabled = true;
        StartCoroutine(TypeWriter(_fullTextFirst));
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);

        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        //// Wait until the asynchronous scene fully loads
        //while (!asyncLoad.isDone)
        //{
        //    yield return null;
        //}
    }

    private IEnumerator TypeWriter(string textToWrite)
    {
        for (int i = 0; i <= textToWrite.Length; i++)
        {
            currentText = textToWrite.Substring(0, i);
            _textDisplayed.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator DissolveIn()
    {
        while (!OVRPlugin.userPresent)
        {
            yield return null;
        }
        yield return new WaitForSeconds(4);
        _imageOnAudioSource.clip = _menuPhotoSounds[0];
        _imageOnAudioSource.volume = 0.784f;
        _imageOnAudioSource.Play();
        _actualUIMaterial.SetFloat("_DissolveAmount", 1f);
        _actualUIMaterial.SetFloat("_DissolveWidth", 0.1f);
        _eRRMAP.material = _actualUIMaterial;
        float count = 1f;

        while (count >= 0)
        {

            _actualUIMaterial.SetFloat("_DissolveAmount", _imageAlphaCurve.Evaluate(count));

            _actualUIMaterial.SetFloat("_DissolveWidth", _imageAlphaCurve.Evaluate(count * 0.5f));
            _eRRMAP.material = _actualUIMaterial;

            count -= Time.deltaTime * 0.25f;
            yield return null;
        }
        yield return null;
        _imageOnAudioSourceBass.Play();
        _imageOnAudioSource.clip = _menuPhotoSounds[1];
        _imageOnAudioSource.volume = 0.4f;
        _imageOnAudioSource.Play();
        _titleText.SetActive(true);
        _instructions.SetActive(true);
        _instructionsButton.interactable = true;
        _start.SetActive(true);
        _startButton.interactable = true;
        _exit.SetActive(true);
        _exitButton.interactable = true;
        _backButton.interactable = false;
        _back.SetActive(false);

    }

    //private IEnumerator FadeInImage(float fadeTime)
    //{
    //    _isFading = !_isFading;
    //    float currentIntensity = _zoneLight.intensity;
    //    float destinationIntensity = 0f;

    //    Debug.Log(_zoneLight.intensity);

    //    if (!_isFading)
    //    {
    //        destinationIntensity = _defaultIntensity;
    //    }

    //    float count = 0f;

    //    while (count <= 1)
    //    {
    //        _zoneLight.intensity = Mathf.Lerp(currentIntensity, destinationIntensity, _imageAlphaCurve.Evaluate(count));

    //        count += Time.deltaTime * fadeTime;
    //        yield return null;
    //    }

    //    yield return null;
    //}
}