using UnityEngine;
using System;
using TMPro;

public class CounterPaddelDisaplayer : CounterDisplayer
{
    [SerializeField] private Paddle paddle;

    private void Awake()
    {
        countText = GetComponent<TextMeshProUGUI>();
        paddle = FindObjectOfType<Paddle>();
    }

    private void OnEnable()
    {
        paddle.OnBallCollide += SetValue;
    }

    private void OnDisable()
    {
        paddle.OnBallCollide -= SetValue;
    }


    protected override void SetValue()
    {
        currentValue++;

        countText.text = currentValue.ToString();
    }
}