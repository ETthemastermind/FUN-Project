using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testSc : MonoBehaviour
{
    public GameObject[] Buttons; //https://answers.unity.com/questions/828666/46-how-to-get-name-of-button-that-was-clicked.html
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Replay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Replay()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<Button>().onClick.Invoke();
            yield return new WaitForSecondsRealtime(1f);
        }
        
    }
}
