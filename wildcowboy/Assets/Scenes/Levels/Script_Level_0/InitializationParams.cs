using UnityEngine;

public class InitializationParams : MonoBehaviour
{
    [SerializeField]
    private int PlayerDamage = 0;
    [SerializeField]
    private int PlayerHealth = 0;

    void Start()
    {
        PlayerPrefs.SetInt("PlayerDamage", PlayerDamage);
        PlayerPrefs.SetInt("PlayerHealth", PlayerHealth);
    }
}
