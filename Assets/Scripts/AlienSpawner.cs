using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienSpawner : MonoBehaviour
{
    public List<GameObject> aliens = new List<GameObject>();

    [SerializeField]
    float delay;

    [SerializeField]
    GameObject[] alien;

    [SerializeField]
    int xMin = 0, xMax = 0;

    bool canWeSwitchLeft = true;
    bool canWeSwitchRight = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                float xPosition = -12 + 2.5f * i;
                float yPostion = 0 + 2.5f * j;
                Vector2 newPosition = new Vector2(xPosition, yPostion);
                aliens.Add (Instantiate(alien[Random.Range(0, alien.Length)], newPosition, Quaternion.identity));   
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < aliens.Count; i++)
        {
          if (aliens[i].transform.position.x > xMax)
            {
                SwitchLeft();
            }

            if (aliens[i].transform.position.x < xMin)
            {
                SwitchRight();
            }
        }

        if (aliens.Count == 0)
        {
            StartCoroutine(LoadNextLevel(delay));
        }
    }

    void SwitchLeft()
    {
        if (canWeSwitchLeft)
        {
            canWeSwitchLeft = false;
            for (int j = 0; j < aliens.Count; j++)
            {
                AlienController alienController = aliens[j].GetComponent<AlienController>();
                if (alienController != null)
                {
                    alienController.ReverseDirection(-1);
                    alienController.MoveDown();
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
            for (int j = 0; j < aliens.Count; j++)
            {
                AlienController alienController = aliens[j].GetComponent<AlienController>();
                if (alienController != null)
                {
                    alienController.ReverseDirection(1);
                    alienController.MoveDown();
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
