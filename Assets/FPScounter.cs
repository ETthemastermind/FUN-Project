using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPScounter : MonoBehaviour // https://answers.unity.com/questions/46745/how-do-i-find-the-frames-per-second-of-my-game.html
{
    int m_frameCounter = 0;
    float m_TimeCounter = 0.0f;
    float m_lastFrameRate = 0.0f;
    public float m_refreshTime = 0.5f;
    public TextMeshProUGUI FPStext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TimeCounter < m_refreshTime)
        {
            m_TimeCounter += Time.deltaTime;
            m_frameCounter++;
        }

        else
        {
            m_lastFrameRate = (float)m_frameCounter / m_TimeCounter;
            FPStext.text = ((int)m_lastFrameRate).ToString();
            m_frameCounter = 0;
            m_TimeCounter = 0.0f;
        }
    }
}
