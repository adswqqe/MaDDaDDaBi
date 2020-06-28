using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAni : MonoBehaviour
{
    float time;
    public Text text;
    // Update is called once per frame
    bool Big;

    void FixedUpdate()
    {
        if (Big == false)
        {
            if (time < 4f)
            {
                transform.localScale = Vector3.one * (1 + time);
                time += Time.deltaTime + 0.1f;
                text.fontSize += (int)time;
            }
            else if (time < 15f)
            {
                time += Time.deltaTime + 0.1f;
            }
            else
            {
                resetAnim();
                   Big = true;
            }
        }
        else
        {
            transform.localScale = Vector3.one * (5 - time);
            text.fontSize -= (int)time + 1; 
            if (time > 5f)
            {
                resetAnim();
                text.fontSize = 1;
                Big = false;
                transform.parent.gameObject.SetActive(false);
            }
            time += Time.deltaTime + 0.2f;
        }
    }

    public void resetAnim()
    {
        time = 0;
        transform.localScale = Vector3.one;
    }
}
