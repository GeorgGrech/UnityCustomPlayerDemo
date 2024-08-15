using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class Weapon : MonoBehaviour
{
    private AudioManager audioManager;
    public AudioClip oneShotAudio; //Special case for enemies
    //Public properties
    public int price; // price for shop

    public enum ShotType
    {
        Hitscan,
        Projectile
    }

    public ShotType weaponShotType;

    public float range;
    public float damage; //This is per single "projectile", so shotguns would deal this damage * shotsPerRound

    [Header("Ammo")]
    public bool infiniteAmmo;
    public float shotsPerRound; //Multiple shots per round. For use with shotguns.
    public float magSize;
    public float currentAmmo;

    //private TextMeshProUGUI ammoUI;
    private GameObject reloadText;

    private Image[] ammoDisplay;
    public Sprite fullRound;
    public Sprite emptyRound;

    [Header("Time")]
    public float rateOfFire;
    public float reloadTime;

    [Space(10)]
    public float accuracy; //So far accuracy will be static. Perhaps later it will be dynamic according to movement, rapid fire, etc.
    public Transform shootSpot;

    [Header("Hitscan related vars")]
    public GameObject lineEffect;

    [Space(10)]
    public bool enableMuzzleEffect;
    public ParticleSystem muzzleEffect;

    [Header("Projectile related vars")]
    public GameObject projectilePrefab;
    public float projectileSpeed;

    //Private functional vars
    private float fireTimer;
    private float actualROF;

    WeaponSwitch weaponSwitch;

    [Space(10)]
    public bool aiWeapon;

    private void Awake()
    {
        if(!aiWeapon)
        {
            ammoDisplay = transform.root.Find("Canvas").Find("Ammo").GetComponentsInChildren<Image>();

            //ammoUI = transform.root.Find("Canvas").Find("AmmoCount").GetComponent<TextMeshProUGUI>();
            reloadText = transform.root.Find("Canvas").Find("ReloadText").gameObject;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        weaponSwitch = transform.parent.GetComponent<WeaponSwitch>();

        actualROF = 1f / rateOfFire;

        currentAmmo = magSize;

        UpdateUI();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (currentAmmo <= 0) //Reload automatically

            Reload();

        UserInput();
    }

    void UserInput()
    {
        if(fireTimer >= actualROF)
        {
            if (Input.GetButton("Fire1"))
            {
                Fire();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo<magSize)
            Reload();
    }

    public void AIFire()
    {
        if (fireTimer >= actualROF)
        {
            Fire();
        }
    }

    void Fire() //Contains functionality common to both fire types, before choosing between Hitscan and Projectile
    {
        fireTimer = 0f;

        if (currentAmmo <= 0)
        {
            //DryFire();
            return;
        }

        if(!infiniteAmmo)
            currentAmmo--;

        SendMessageUpwards("TrackShotFired", null, SendMessageOptions.DontRequireReceiver);

        for (int i = 0; i < shotsPerRound; i++)
        {

            if (weaponShotType == ShotType.Hitscan)
            {
                HitscanFire();
            }
            else ProjectileFire();

            UpdateUI();
        }
    }

    void HitscanFire()
    {
        if(enableMuzzleEffect)
            muzzleEffect.Play();

        if (!aiWeapon)
        {
            audioManager.playSound(0);
            Invoke("PlayShellSound", 0.5f);
        }
        else PlayOneShotAudio();
        
        float accuracyVary = (100 - accuracy) / 1000;
        Vector3 direction = shootSpot.forward;
        direction.x += Random.Range(-accuracyVary, accuracyVary);
        direction.y += Random.Range(-accuracyVary, accuracyVary);
        direction.z += Random.Range(-accuracyVary, accuracyVary);

        Ray ray = new Ray(shootSpot.position, direction);
        RaycastHit hit;

        Vector3 effectTarget = shootSpot.forward+direction*range;

        if (Physics.Raycast(ray, out hit, range))
        {
            //Get GameObject hit
            GameObject gb = hit.collider.gameObject;
            gb.SendMessageUpwards("ChangeHealth", -damage, SendMessageOptions.DontRequireReceiver);
        }
        LineEffect(effectTarget);
        //EditorApplication.isPaused = true; //Testing purposes. Leave commented.

    }

    private void PlayShellSound()
    {
        audioManager.playSound(2);
    }

    void ProjectileFire()
    {

        float accuracyVary = (100 - accuracy) / 1000;

        Quaternion randomRotation = shootSpot.rotation;
        randomRotation.x += Random.Range(-accuracyVary, accuracyVary);
        randomRotation.y += Random.Range(-accuracyVary, accuracyVary);

        GameObject projectile = Instantiate(projectilePrefab, shootSpot.position, randomRotation);

        projectile.GetComponent<Projectile>().damage = damage;
        projectile.GetComponent<Projectile>().range = range;

        projectile.GetComponent<Rigidbody>().AddRelativeForce(0, 0, projectileSpeed);
    }

    public void LineEffect(Vector3 direction)
    {
        GameObject lrObject = Instantiate(lineEffect);
        LineRenderer lr = lrObject.GetComponent<LineRenderer>();

        if(!aiWeapon)
            weaponSwitch.trailEffects.Add(lrObject);

        lr.SetPosition(0, shootSpot.position);
        lr.SetPosition(1, direction * range);
    }

    private void Reload() //This method reloads in one go. Perhaps later for weapons like shotguns, they'll reload one by one?
    {
        Debug.Log("Reloading...");
        ShowReloadMessage();
        currentAmmo = magSize;
        fireTimer = -reloadTime;
        Invoke("UpdateUI",reloadTime);
        Invoke("HideReloadMessage",reloadTime);
        audioManager.playSound(1);

    }

    //So far I'm just using this to display the debug reload messages, but will be used to update UI later on
    private void UpdateUI()
    {
        if (!aiWeapon)
        {
            if (gameObject.activeSelf) //Don't override if weapon switched
            {
                //Update UI

                //ammoUI.SetText(currentAmmo.ToString() + " / " + magSize.ToString());

                foreach (Image image in ammoDisplay)
                {
                    image.sprite = emptyRound;
                }

                for (int i = 0; i < currentAmmo; i++)
                {
                    ammoDisplay[i].sprite = fullRound;
                }

                Debug.Log(name + " reloaded.");
            }
        }
        
    }

    private void ShowReloadMessage()
    {
        reloadText.SetActive(true);
    }

    private void HideReloadMessage()
    {
        reloadText.SetActive(false);
    }

    private void PlayOneShotAudio()
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = oneShotAudio;
        audio.playOnAwake = false;
        audio.volume = 0.3f;
        audio.Play();
        StartCoroutine(RemoveAudioSource(audio));
    }

    private IEnumerator RemoveAudioSource(AudioSource audio)
    {
        yield return new WaitForSeconds(3);
        Destroy(audio);
    }
}
