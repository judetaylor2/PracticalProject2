using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelector : MonoBehaviour
{
    public Transform weaponTransform;
    [HideInInspector] public List<GameObject> playerWeapons = new List<GameObject>();
    List<GameObject> weaponUIObjects = new List<GameObject>();

    public GameObject currentWeaponUIObject;
    GameObject currentWeapon;
    int currentWeaponIndex;
    bool hasWeapon;
    public Gradient ammoColours;
    
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = Instantiate(new GameObject(), weaponTransform.position, weaponTransform.rotation, weaponTransform);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasWeapon)
        {
            if (playerWeapons.Count > 1)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    currentWeapon.SetActive(false);
                    currentWeaponUIObject.SetActive(false);
                    
                    if (currentWeaponIndex >= playerWeapons.Count - 1)
                    {
                        currentWeaponIndex = 0;
                        
                    }
                    else
                    {
                        currentWeaponIndex++;
                    }

                    currentWeapon = playerWeapons[currentWeaponIndex];
                    currentWeaponUIObject = weaponUIObjects[currentWeaponIndex];

                    currentWeapon.SetActive(true);
                    currentWeaponUIObject.SetActive(true);
                    

                    Debug.Log("current weapon: " + currentWeapon);
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    currentWeapon.SetActive(false);
                    currentWeaponUIObject.SetActive(false);
                    
                    if (currentWeaponIndex <= 0)
                    {
                        currentWeaponIndex = playerWeapons.Count - 1;
                        
                    }
                    else
                    {
                        currentWeaponIndex--;
                    }

                    currentWeapon = playerWeapons[currentWeaponIndex];
                    currentWeaponUIObject = weaponUIObjects[currentWeaponIndex];
            
                    currentWeapon.SetActive(true);
                    currentWeaponUIObject.SetActive(true);
                    

                    Debug.Log("current weapon: " + currentWeapon);
                }

            }    

            if (!currentWeapon.GetComponent<WeaponStats>().GetComponent<WeaponMelee>())
            {
                currentWeaponUIObject.GetComponentInChildren<Slider>().maxValue = currentWeapon.GetComponent<WeaponStats>().maxAmmo;
                currentWeaponUIObject.GetComponentInChildren<Slider>().value = currentWeapon.GetComponent<WeaponStats>().currentAmmo;

            }
            
            currentWeaponUIObject.transform.GetComponentInChildren<TMPro.TMP_Text>().text = currentWeapon.GetComponent<WeaponStats>().currentAmmo + " / " + currentWeapon.GetComponent<WeaponStats>().maxAmmo;

 
            currentWeaponUIObject.transform.GetChild(3).GetComponentInChildren<TMPro.TMP_Text>().text = "" + currentWeapon.GetComponent<WeaponStats>().currentClips;

            //change colour depending on ammo left
            //Color32 c = ammoColours.Evaluate(100 * currentWeapon.GetComponent<WeaponStats>().currentAmmo / (Mathf.Clamp(currentWeapon.GetComponent<WeaponStats>().maxAmmo, 1, 100)));
            float f = (float)currentWeapon.GetComponent<WeaponStats>().currentAmmo / Mathf.Clamp((float)currentWeapon.GetComponent<WeaponStats>().maxAmmo, 1, 100);
            Color c = ammoColours.Evaluate(f);
            
            Debug.Log("colours: " + f + " ammocolours: " + ammoColours.Evaluate(f));
            
            currentWeaponUIObject.GetComponentInChildren<TMPro.TMP_Text>().color = c;
            currentWeaponUIObject.GetComponentInChildren<Slider>().fillRect.GetComponent<Image>().color = c;
            currentWeaponUIObject.transform.GetChild(2).GetComponent<Image>().color = c;
            currentWeaponUIObject.transform.GetChild(3).GetChild(1).GetComponent<Image>().color = c;
            currentWeaponUIObject.transform.GetChild(3).GetComponentInChildren<TMPro.TMP_Text>().color = c;
        
            
        
            
        }

    }

    public void GiveWeapon(GameObject weapon, GameObject weaponObject)
    {
        playerWeapons.Add(Instantiate(weapon, weaponTransform.position, weaponTransform.rotation, weaponTransform));

        currentWeapon.SetActive(false);
        currentWeaponIndex = playerWeapons.Count - 1;

        weaponUIObjects.Add(weaponObject);

        
        if (hasWeapon)
        {
            currentWeaponUIObject.SetActive(false);
        }
        
            
        currentWeapon = playerWeapons[currentWeaponIndex];
        currentWeaponUIObject = weaponUIObjects[currentWeaponIndex];

        currentWeaponUIObject.GetComponentInChildren<Slider>().maxValue = currentWeapon.GetComponent<WeaponStats>().maxAmmo;
        
        currentWeaponUIObject.SetActive(true);
        
        
        hasWeapon = true;
    }
}
