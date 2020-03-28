using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PointAndHold : MonoBehaviour
{
    public Image _imageLoadCircle;
    private Button _button;
    private float _counter;
    private bool _loadingButtonOn;
    private float _loadingSpeed = 2f;
    public TeleType _teleTypeScript;
    public GameObject _storyGameObject;
    public Animator _fadeOutAnimator;
    public GameObject[] _otherButtons;


    private void Start()
    {
        _button = GetComponent<Button>();
    }


    public void ActivateButtonLoadSequence()
    {
        Debug.Log("StartLoading");
        _loadingButtonOn = true;
    }

    public void StopButtonLoadSequence()
    {
        Debug.Log("StopLoading");
        _loadingButtonOn = false;
        _counter = 0f;
        _imageLoadCircle.GetComponent<Image>().fillAmount = 0;
    }

    private void Update()
    {
        if (_loadingButtonOn)
        {
            _counter += Time.deltaTime;
            _imageLoadCircle.fillAmount = _counter / _loadingSpeed;

            
                if (_imageLoadCircle.fillAmount >= 1)
            {
                _loadingButtonOn = false;
                _counter = 0f;

                if (_button.tag == "ExitButton")
                {
                    _counter = 0f;
                    _imageLoadCircle.GetComponent<Image>().fillAmount = 0;
                    _teleTypeScript.ResetStory();
                    _storyGameObject.SetActive(false);
                }
                else if (_button.tag == "Menu")
                {
                    _fadeOutAnimator.SetBool("FadeIn", true);
                    Invoke("BackToMenu", 2);
                }
                else
                {
                    _counter = 0f;
                    _imageLoadCircle.GetComponent<Image>().fillAmount = 0;
                    _storyGameObject.SetActive(true);
                    _teleTypeScript.StartStory();
                }
            }
        }
    }

    private void BackToMenu()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
