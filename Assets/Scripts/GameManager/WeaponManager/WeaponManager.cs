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
    private bool hasSword = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        EquipWeapon(WeaponType.None); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && hasBow)
            EquipWeapon(WeaponType.Bow);
        if (Input.GetKeyDown(KeyCode.Z) && hasSword)
            EquipWeapon(WeaponType.Sword);
        //if (Input.GetKeyDown(KeyCode.Z))
        //    EquipWeapon(WeaponType.None);
    }

    public void PickUpWeapon(WeaponType weapon)
    {
        if (weapon == WeaponType.Bow) hasBow = true;
        if (weapon == WeaponType.Sword) hasSword = true;
        EquipWeapon(weapon);
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
