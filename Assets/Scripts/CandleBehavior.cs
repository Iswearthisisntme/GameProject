using UnityEngine;

public class CandleBehavior : MonoBehaviour
{
    private GameObject player;
    private LevelManager levelManager;

    public GameObject litCandle;
    public GameObject flame;
    GameObject[] candles;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = FindObjectOfType<LevelManager>();
        candles = GameObject.FindGameObjectsWithTag("Candle");
    }

    void Update()
    {
        bool allCandlesSnuffed = true;

        foreach (GameObject candle in candles)
        {
            if (candle.GetComponent<CandleBehavior>().flame.activeSelf)
            {
                allCandlesSnuffed = false;
                break;
            }
        }

        if (allCandlesSnuffed)
        {
            levelManager.LevelWon();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            float distance = Vector3.Distance(litCandle.transform.position, player.transform.position);
            if (distance < 2.0f) 
            {
                SnuffCandle();
            }
        }
    }

    private void SnuffCandle()
    {
        // Replace with audio sound for candle being put out
        // AudioSource.PlayClipAtPoint(pluginSFX, Camera.main.transform.position);

        flame.SetActive(false);
    }
}
