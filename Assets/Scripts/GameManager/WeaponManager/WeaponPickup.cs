using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
         
            WeaponManager weaponManager = other.GetComponent<WeaponManager>();

            if (weaponManager != null)
            {
              
                weaponManager.PickUpWeapon(weaponType);

                
                Destroy(gameObject);
            }
        }
    }
}
