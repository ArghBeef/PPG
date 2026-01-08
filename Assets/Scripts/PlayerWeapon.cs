using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    public InputActionReference aimAction;
    public InputActionReference fireAction;
    public InputActionReference switchProjectileAction;

    public GameObject weaponModel;
    public bool showWeaponOnlyWhenAiming = false;
    public Transform muzzle;

    public GameObject projectileInstantPrefab;
    public GameObject projectileDelayedPrefab;

    public float projectileSpeed = 200f;
    public float fireCooldown = 0.2f;

    public Camera mainCamera;
    public float normalFOV = 80f;
    public float aimFOV = 40f;
    public float fovLerpSpeed = 10f;

    public PlayerStats stats;
    public int ammoUsage = 10;

    public bool isAiming { get; private set; }
    float lastShotTime;
    bool useDelayedProjectile;

    void OnEnable()
    {
        if (aimAction != null) aimAction.action.Enable();
        if (fireAction != null) fireAction.action.Enable();
        if (switchProjectileAction != null) switchProjectileAction.action.Enable();
    }

    void OnDisable()
    {
        if (aimAction != null) aimAction.action.Disable();
        if (fireAction != null) fireAction.action.Disable();
        if (switchProjectileAction != null) switchProjectileAction.action.Disable();
    }

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            mainCamera.fieldOfView = normalFOV;
        }

        if (weaponModel != null && showWeaponOnlyWhenAiming)
            weaponModel.SetActive(false);

        useDelayedProjectile = false;
    }

    void Update()
    {
        HandleSwitchProjectileType();
        HandleAim();
        HandleFire();
        UpdateCameraFOV();
    }

    void HandleSwitchProjectileType()
    {
        if (switchProjectileAction == null) return;

        if (switchProjectileAction.action.triggered)
        {
            useDelayedProjectile = !useDelayedProjectile;
        }
    }

    void HandleAim()
    {
        if (aimAction == null) return;

        isAiming = aimAction.action.IsPressed();


        if (weaponModel != null && showWeaponOnlyWhenAiming)
            weaponModel.SetActive(isAiming);
    }

    void HandleFire()
    {
        if (fireAction == null) return;
        if (!fireAction.action.triggered) return;
        if (!isAiming) return;
        if (Time.time < lastShotTime + fireCooldown) return;
        if (muzzle == null) return;

        if (stats.ammo < ammoUsage) return;
        stats.ChangeAmmo(-ammoUsage);

        GameObject prefab = useDelayedProjectile ? projectileDelayedPrefab : projectileInstantPrefab;
        if (prefab == null) return;

        lastShotTime = Time.time;

        GameObject proj = Instantiate(prefab, muzzle.position, muzzle.rotation);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = muzzle.forward * projectileSpeed;
    }

    void UpdateCameraFOV()
    {
        if (mainCamera == null) return;

        float target = isAiming ? aimFOV : normalFOV;
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView,target,Time.deltaTime * fovLerpSpeed);
    }
}
