using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.Audio;

public class TeleType : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro_Story;

    public AudioSource _voiceStory;
    public AudioMixer _enviromentSounds;

    public Slider _storyTimeLineSlider;
    public Light _highLightPointLight;

    [Range(0.054f, 0.065f)]
    public Animator _animator;
    public Animator _MovingObjectAnimatior;

    public GameObject[] _allButtons;
    public GameObject[] _allStorysGameObject;

    public float _speedText = 0.056f;
    private float _fadeOutEnviromentVolume = -30f;

    int counter = 0;
    int totalVisibleCharacters;

    public bool _storyActive;
    private string _storyString = "";


    private void Awake()
    {
        totalVisibleCharacters = textMeshPro_Story.textInfo.characterCount;
        textMeshPro_Story.maxVisibleCharacters = 0;
        _storyString = textMeshPro_Story.text;
    }

    private void Start()
    {
        if (gameObject.tag == "Intro")
        {
            Invoke("StartStory", 12f);
        }
    }
    [ContextMenu("Play Story")]
    public void StartStory()
    {
        if (!_storyActive)
        {

            if (_MovingObjectAnimatior)
            {
            _MovingObjectAnimatior.SetBool("ObjectEvent", true);
            }
            _storyActive = true;
            StartCoroutine(FadeOutEnviromentSoundsCoroutine());
            StartCoroutine(StartVoiceCoroutine());
        }
    }


    public void ResetStory()
    {
        if (_MovingObjectAnimatior)
        {
            _MovingObjectAnimatior.SetBool("ObjectEvent", false);
        }
        _highLightPointLight.gameObject.SetActive(false);
        _enviromentSounds.SetFloat("VolSounds", 0f);
        StopAllCoroutines();
        counter = 0;
        textMeshPro_Story.text = _storyString;
        _voiceStory.Stop();
        _storyActive = false;
        _animator.Play("RollTextUp", -1, 0f);
        ShowButtons();
    }




    private void  HideButtons()
    {
        foreach (GameObject go in _allButtons)
        {
            go.SetActive(false);
        }
    }


    private void ShowButtons()
    {
        foreach (GameObject go in _allButtons)
        {
            go.SetActive(true);
        }

        foreach (GameObject gameobject in _allStorysGameObject)
        {
            gameobject.SetActive(false);
        }
    }

    private IEnumerator FadeOutEnviromentSoundsCoroutine()
    {
        float counter = 0f;

        float test;
        _enviromentSounds.GetFloat("VolSounds",out test);

        while (counter < 1)
        {
            float vol = Mathf.Lerp(0f, _fadeOutEnviromentVolume, counter);
            _enviromentSounds.SetFloat("VolSounds", vol);
            counter += Time.deltaTime / 4;
            if (counter >= 1)
            {
                counter = 1f;
            }
            yield return 0f;
        }
    }



    private void Update()
    {
        if (_storyActive)
        {
            _highLightPointLight.gameObject.SetActive(true);
            _highLightPointLight.transform.position = new Vector3(_MovingObjectAnimatior.transform.position.x - 2f, _MovingObjectAnimatior.transform.position.y, _MovingObjectAnimatior.transform.position.z );
            Debug.Log(_storyTimeLineSlider.value);
            _storyTimeLineSlider.value = (_voiceStory.time / _voiceStory.clip.length);
            if (_storyTimeLineSlider.value == 1f)
            {
                _storyActive = false;
            }
        }
    }

    private IEnumerator StartVoiceCoroutine()
    {
        HideButtons();
        _animator.SetBool("RollTextUp", true);
        _voiceStory.Play();
        //textMeshPro_Story.text += "f";
        while (Regex.Matches(textMeshPro_Story.text, @"[a-zA-Z]").Count > 0) 
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            textMeshPro_Story.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
                yield return new WaitForSeconds(1.0f);
            //  Debug.Log(event_TMP_01.text[counter]);

            counter += 1;
            yield return new WaitForSeconds(_speedText);

            // If the current visible char is "." Then delay the coroutine with x seconds
            if (textMeshPro_Story.text[counter] == '.') 
            {
                counter += 1;
                visibleCount = counter % (totalVisibleCharacters + 1);
                textMeshPro_Story.maxVisibleCharacters = visibleCount;
                counter = 0;
                yield return new WaitForSeconds(1.0f);
                string str = textMeshPro_Story.text.Substring(textMeshPro_Story.text.IndexOf('.') + 1);
                textMeshPro_Story.text = str;
                _animator.Play("RollTextUp", -1, 0f);
                //yield return new WaitForSeconds(1.0f);
            }
        }

        textMeshPro_Story.text = _storyString;
        textMeshPro_Story.maxVisibleCharacters = 0;
        _storyActive = false;
        Debug.Log("KlarMedStoryn");
        ResetStory();
    }
}
