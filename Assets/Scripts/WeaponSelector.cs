using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public Transform weaponTransform;
    public List<GameObject> weapons, playerWeapons;

    GameObject currentWeapon;
    int currentWeaponIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = Instantiate(new GameObject(), weaponTransform.position, weaponTransform.rotation, weaponTransform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            currentWeapon.SetActive(false);
            
            if (currentWeaponIndex >= playerWeapons.Count - 1)
            {
                currentWeaponIndex = 0;
                currentWeapon = playerWeapons[currentWeaponIndex];
                
            }
            else
            {
                currentWeaponIndex++;
                currentWeapon = playerWeapons[currentWeaponIndex];
            }
    
            currentWeapon.SetActive(true);
            

            Debug.Log("current weapon: " + currentWeapon);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            currentWeapon.SetActive(false);
            
            if (currentWeaponIndex <= 0)
            {
                currentWeaponIndex = playerWeapons.Count - 1;
                currentWeapon = playerWeapons[currentWeaponIndex];
                
            }
            else
            {
                currentWeaponIndex--;
                currentWeapon = playerWeapons[currentWeaponIndex];
            }
    
            currentWeapon.SetActive(true);
            

            Debug.Log("current weapon: " + currentWeapon);
        }
    }

    public void GiveWeapon(GameObject weapon)
    {
        playerWeapons.Add(Instantiate(weapon, weaponTransform.position, weaponTransform.rotation, weaponTransform));

        currentWeapon.SetActive(false);
        currentWeapon = playerWeapons[playerWeapons.Count - 1];
    }
}
