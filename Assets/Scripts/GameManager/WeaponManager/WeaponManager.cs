using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponType currentWeapon = WeaponType.None;

    [Header("Animator Override Controllers")]
    [SerializeField] private AnimatorOverrideController unarmedOverride;
    [SerializeField] private AnimatorOverrideController bowOverride;
    [SerializeField] private AnimatorOverrideController swordOverride;


    //[Header("Weapon")]
    //[SerializeField] private GameObject bowObject;
    //[SerializeField] private GameObject swordObject;


    //[Header("Hitbox")]
    //[SerializeField] private GameObject bowHitbox;
    //[SerializeField] private GameObject swordHitbox;

    private Animator animator;
    private bool hasBow = false;
    private bool hasSword = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        EquipWeapon(WeaponType.None); 
    }

    private void Update()
    {
        // Only allow switching when both weapons are available
        if (hasBow && hasSword && Input.GetKeyDown(KeyCode.Z))
        {
            if (currentWeapon == WeaponType.Sword)
                EquipWeapon(WeaponType.Bow);
            else if (currentWeapon == WeaponType.Bow)
                EquipWeapon(WeaponType.Sword);
        }
    }

    public void PickUpWeapon(WeaponType weapon)
    {
        // Pick up and auto-equip if not already equipped
        if (weapon == WeaponType.Bow && !hasBow)
        {
            hasBow = true;

            // If currently unarmed or only had one weapon before
            if (currentWeapon == WeaponType.None || !hasSword)
                EquipWeapon(WeaponType.Bow);
        }

        if (weapon == WeaponType.Sword && !hasSword)
        {
            hasSword = true;

            if (currentWeapon == WeaponType.None || !hasBow)
                EquipWeapon(WeaponType.Sword);
        }
    }

    public void EquipWeapon(WeaponType weapon)
    {
        currentWeapon = weapon;

        switch (weapon)
        {
            case WeaponType.Bow:
                animator.runtimeAnimatorController = bowOverride;
                break;
            case WeaponType.Sword:
                animator.runtimeAnimatorController = swordOverride;
                break;
            case WeaponType.None:
                animator.runtimeAnimatorController = unarmedOverride;
                break;
        }

        //bowObject.SetActive(weapon == WeaponType.Bow);
        //swordObject.SetActive(weapon == WeaponType.Sword);

        //bowHitbox.SetActive(false);
        //swordHitbox.SetActive(false);
    }
}