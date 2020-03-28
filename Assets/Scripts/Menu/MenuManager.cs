using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public Animator _fadeOutAnimator;
    private Animation _FadeIn;
    private float _lengthOfAnimation;
    AnimatorClipInfo[] m_CurrentClipInfo;

    public void StartFadeOut()
    {
        _fadeOutAnimator.SetBool("FadeIn", true);
        _lengthOfAnimation = GetComponent<Animation>().clip.length;
        Debug.Log(_lengthOfAnimation);
        Invoke("StartAlleyScene", _lengthOfAnimation + 1F);
    }

    public void StartAlleyScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
