using UnityEngine;
using TMPro;
using TestWorkSergeyGorobets.Obstacles;

public class CounterObstacleDisplayer : CounterDisplayer
{
    [SerializeField] private BackObstacle backObstacle;

    private void Awake()
    {
        countText = GetComponent<TextMeshProUGUI>();
        backObstacle = FindObjectOfType<BackObstacle>();
    }

    private void OnEnable()
    {
        backObstacle.OnObjectCollided += SetValue;
    }

    private void OnDisable()
    {
        backObstacle.OnObjectCollided -= SetValue;
    }

    protected override void SetValue()
    {
        currentValue++;

        countText.text = currentValue.ToString();
    }
}