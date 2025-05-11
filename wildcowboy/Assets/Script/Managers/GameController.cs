using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private TargetManager targetManager;
    [SerializeField] private int score = 0;
    public GameObject hpEnemy;
    public GameObject hpSelf;
    public bool enemyStep = false;
    public int[] unlockLevels;


    private void FixedUpdate()
    {
        if (hpSelf.transform.childCount == 0 || hpEnemy.transform.childCount == 0)
        {
            foreach (var levelNumber in unlockLevels)
            {
                ProgressManager.UnlockLevel(levelNumber);
            }


            SceneManager.LoadScene("SelectLevel");
        }
    }
}