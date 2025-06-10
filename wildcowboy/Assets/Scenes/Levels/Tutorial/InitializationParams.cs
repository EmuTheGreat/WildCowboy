using UnityEngine;

public class InitializationParams : MonoBehaviour
{
    [SerializeField]
    private int PlayerDamage = 30;
    [SerializeField]
    private int PlayerHealth = 100;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("PlayerDamage"))
        {
            PlayerPrefs.SetInt("PlayerDamage", PlayerDamage);
            PlayerPrefs.SetInt("PlayerHealth", PlayerHealth);

            PlayerPrefs.SetInt("Perk_Reaction", 0);
            PlayerPrefs.SetInt("Perk_Armor", 0);
            PlayerPrefs.SetInt("Perk_Accuracy", 0);
            PlayerPrefs.SetInt("Exp_Point", 100);
        }
    }
}
