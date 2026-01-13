using TMPro;
using UnityEngine;

public class HUDHealth : MonoBehaviour
{
    public static HUDHealth Instance;

    [SerializeField] private TextMeshProUGUI healthText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateHealth(float current, float max)
    {
        healthText.text = current + " / " + max;
    }
}
