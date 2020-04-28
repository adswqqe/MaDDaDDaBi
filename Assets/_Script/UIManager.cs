using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text levelText;
    [SerializeField]
    Text goldText;
    [SerializeField]
    Text reputationText;
    [SerializeField]
    Text bagSpaceText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnChangeValueUI(Data data)
    {
        levelText.text = "레벨 : " + data.Level.ToString();
        goldText.text = data.GOLD.ToString();
        reputationText.text = data.REPUTATION.ToString();
        bagSpaceText.text = "공간 : " + data.BAGSPACE + "/" + " " + data.MAX_BAGSPCE;
    }
}
