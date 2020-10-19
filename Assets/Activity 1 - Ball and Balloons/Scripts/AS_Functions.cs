﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AS_Functions : MonoBehaviour
{
    public AudioSource AS;
    public TextMeshProUGUI VolumeNumber;
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        UpdateLabel();
    }

    public void UpdateLabel()
    {
        int UI_Number = (int)(AS.volume * 10); //gets the current volume as in int
        VolumeNumber.text = UI_Number.ToString(); // set the text of the UI component
    }
}