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
        while (input.Length != 0)
        {
            if(input.Length > 3)
            {
                input = input.Remove(3);
                inputBox.GetComponent<TMP_InputField>().text = input;
            }
            try
            {
                if (input.StartsWith("0"))
                {
                    input = input.Replace("0", "1");
                    inputBox.GetComponent<TMP_InputField>().text = input;
                }
                int a = Int32.Parse(input);
                return;
            }
            catch (Exception)
            {
                input = input.Remove(input.Length - 1);
                inputBox.GetComponent<TMP_InputField>().text = input;
            }
        }
    }
}
