using UnityEngine;
using TMPro;

public abstract class CounterDisplayer : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI countText;

    protected int currentValue = 0;
    
    protected abstract void SetValue();
}