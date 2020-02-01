using UnityEngine;

public class HPSlider : MonoBehaviour
{
    private UnityEngine.UI.Slider s;
    private UnityEngine.UI.Text t;

    void Awake()
    {
        s = GetComponent<UnityEngine.UI.Slider>();
        s.maxValue = 100f;
        s.value = StaticPlayerController._playerMaxHealth;

        t = GetComponentInChildren<UnityEngine.UI.Text>();
        t.text = "HP: " + StaticPlayerController._playerHealth + "/" + StaticPlayerController._playerMaxHealth;
    }

    void Update()
    {
        s.value = StaticPlayerController._playerHealth;
        t.text = "HP: " + StaticPlayerController._playerHealth + "/" + StaticPlayerController._playerMaxHealth;
    }
}
