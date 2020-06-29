using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderBoxManager : MonoBehaviour
{
    [SerializeField]
    Renderer[] boxmaterials;
    [SerializeField]
    SpriteRenderer orderBoxBottom;

    List<Color32> colors = new List<Color32>();
    List<int> useIndex = new List<int>();

    // Start is called before the first frame update
    void Awake()
    {
        colors.Add(new Color32(225, 175, 156, 255));
        colors.Add(new Color32(173, 63, 78, 255));
        colors.Add(new Color32(255, 236, 182, 255));
        colors.Add(new Color32(171, 235, 255, 255));
        colors.Add(new Color32(141, 138, 255, 255));
        colors.Add(new Color32(71, 121, 255, 255));
        colors.Add(new Color32(171, 238, 208, 255));
        colors.Add(new Color32(172, 233, 138, 255));
        colors.Add(new Color32(255, 255, 255, 255));
        colors.Add(new Color32(255, 153, 112, 255));
        colors.Add(new Color32(182, 162, 221, 255));
    }

    private void OnEnable()
    {
        for (int i = 0; i < boxmaterials.Length; i++)
        {
            int rand = Random.Range(0, 11);

            if (useIndex.Contains(rand))
            {
                while (useIndex.Contains(rand))
                    rand = Random.Range(0, 11);
            }

            //Debug.Log(colors[rand]);
            //boxmaterials[i].material.SetColor("_Color", colors[0]);
            boxmaterials[i].material.color = colors[rand];
            useIndex.Add(rand);

        }

        useIndex.Clear();
        orderBoxBottom.color = new Color(1, 1, 1, 1);
    }
}
