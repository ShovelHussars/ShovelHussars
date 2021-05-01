using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LengthOfRandomRun: MonoBehaviour
{
    #region Singleton
    public static LengthOfRandomRun instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion

    public int lengthOfRandomRun = 10;
    public GameObject inputBox = null;

    public void SetLength(string length)
    {
        if (length == "")
        {
            lengthOfRandomRun = 10;
        }
        else
        {
            lengthOfRandomRun = Int32.Parse(length);
        }
    }

    public void Regex(string input)
    {
        while (true)
        {
            try
            {
                int a = Int32.Parse(input);
                break;
            }
            catch (Exception)
            {
                if (input.Length != 0)
                {
                    input = input.Remove(input.Length - 1);
                    inputBox.GetComponent<TMP_InputField>().text = input;
                }
                else
                {
                    inputBox.GetComponent<TMP_InputField>().text = input;
                    break;
                }
            }
        }
    }
}
