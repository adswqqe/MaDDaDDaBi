using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechManager : MonoBehaviour
{
    Text text;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
        Debug.Log(text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartSpeech(string text, Transform tr)
    {
        if (isActive)
            return;
        gameObject.SetActive(true);
        this.text.text = text;
        Vector3 pos = tr.position;
        pos.x += Mathf.Abs(pos.x / 20f);
        pos.y += Mathf.Abs(pos.y / 2f);
        transform.position = Camera.main.WorldToScreenPoint(pos);
        StartCoroutine(Speech());
    }

    IEnumerator Speech()
    {
        Debug.Log("start");
        float timer = 0.0f;
        isActive = true;
        while(true)
        {
            timer += Time.deltaTime;

            if (timer >= 1.5f)
            {
                isActive = false;
                gameObject.SetActive(false);
                break;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
}
