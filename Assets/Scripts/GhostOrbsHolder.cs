using UnityEngine;

public class GhostOrbsHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = enemy.localScale;
    }
}
