using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private TargetManager targetManager;
    [SerializeField] private int score = 0;
    public GameObject hpEnemy;
    public GameObject hpSelf;
    public bool enemyStep = false;


    private void FixedUpdate()
    {
        if (hpSelf.transform.childCount == 0 || hpEnemy.transform.childCount == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}