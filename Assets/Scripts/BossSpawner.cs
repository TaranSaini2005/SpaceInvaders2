using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossSpawner : MonoBehaviour
{
    public List<GameObject> bosses = new List<GameObject>();

    [SerializeField]
    float delay;

    [SerializeField]
    GameObject[] boss;

    [SerializeField]
    int xMin = 0, xMax = 0;

    [SerializeField]
    int amount;

    [SerializeField]
    int xPosition; 
        
    [SerializeField]
    int yPosition;



    bool canWeSwitchLeft = true;
    bool canWeSwitchRight = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            xPosition = -10 + 10 * i;
            Vector2 newPosition = new Vector2(xPosition, yPosition);
            bosses.Add(Instantiate(boss[Random.Range(0, boss.Length)], newPosition, Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bosses.Count; i++)
        {
            if (bosses[i].transform.position.x > xMax)
            {
                SwitchLeft();
            }

            if (bosses[i].transform.position.x < xMin)
            {
                SwitchRight();
            }
        }

        if (bosses.Count == 0)
        {
            StartCoroutine(LoadNextLevel(delay));
        }
    }

    void SwitchLeft()
    {
        if (canWeSwitchLeft)
        {
            canWeSwitchLeft = false;
            for (int j = 0; j < bosses.Count; j++)
            {
                BossController bossController = bosses[j].GetComponent<BossController>();
                if (bossController != null)
                {
                    bossController.ReverseDirection(-1);
                }
            }
            canWeSwitchRight = true;
        }
    }

    void SwitchRight()
    {
        if (canWeSwitchRight)
        {
            canWeSwitchRight = false;
            for (int j = 0; j < bosses.Count; j++)
            {
                BossController bossController = bosses[j].GetComponent<BossController>();
                if (bossController != null)
                {
                    bossController.ReverseDirection(1);
                }
            }
            canWeSwitchLeft = true;
        }

    }

    IEnumerator LoadNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
