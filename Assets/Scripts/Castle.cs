using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] private Text resultText;
    [SerializeField] private Text hpText;
    public int hp;

    private void Start()
    {
        resultText.text = " ";
    }
    private void Update()
    {
        hpText.text = hp.ToString();
        if (hp <= 0)
        {
            hpText.text = "END";
            ShowResultText();
        }
    }

    private void ShowResultText()
    {
        switch (this.gameObject.tag)
        {
            case "Player":
                resultText.text = "LOSE..";
                break;
            case "Enemy":
                resultText.text = "WIN!!!";
                break;
        }
    }
}
