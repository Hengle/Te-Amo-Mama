using UnityEngine;

public class TimedSpawning : MonoBehaviour
{
    [SerializeField] private bool stopSpawning = false;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnDelay;

    private void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject()
    {
        if (!stopSpawning)
        {
            GameObject obj = ObjectPooler.instance.GetPooledObject();

            if (obj != null)
            {
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                obj.SetActive(true);
            }
        }

        else
        {
            CancelInvoke("SpawnObject");
        }
    }
}
