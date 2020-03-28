using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour
{
    //private static DoNotDestroyOnLoad _instance;

    //public static DoNotDestroyOnLoad Instance
    //{
    //    get { return _instance; }
    //}

    void Awake()
    {
        //if (_instance == null)
        //{
        //    _instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}

        //else if (_instance != this)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DoNotDestroyOnLoad");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        //GameObject[] objects = GameObject.FindGameObjectsWithTag();
    }
}