using UnityEngine;

public class InitializationParams : MonoBehaviour
{
    [SerializeField]
    private int PlayerDamage = 0;
    [SerializeField]
    private int PlayerHealth = 0;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("PlayerDamage", PlayerDamage);
        PlayerPrefs.SetInt("PlayerHealth", PlayerHealth);

        PlayerPrefs.SetInt("Perk_Reaction", PlayerHealth);
        PlayerPrefs.SetInt("Perk_Armor", PlayerHealth);
        PlayerPrefs.SetInt("Perk_Accuracy", PlayerHealth);
        PlayerPrefs.SetInt("Exp_Point", 100);
    }
}
