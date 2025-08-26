using UnityEngine;

public class HealthCollectibles : MonoBehaviour
{
    [SerializeField] private float healthValue;
    private Audio sound;
    private void Awake()
    {
        sound = GameObject.FindGameObjectWithTag("audio").GetComponent<Audio>();   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            sound.Playvfx(sound.pickupClip);
            gameObject.SetActive(false);    
        }
    }
}
