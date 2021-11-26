using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public Transform weaponTransform;
    public List<GameObject> weapons, playerWeapons;

    GameObject currentWeapon;
    
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
            GameObject g = currentWeapon;

            for (int i = 0; i < playerWeapons.Count; i++)
            {
                if (currentWeapon != playerWeapons[i])
                {
                    currentWeapon = playerWeapons[i];
                    break;
                }
            }

    
            currentWeapon.SetActive(true);
            

            Debug.Log("current weapon: " + currentWeapon);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            currentWeapon.SetActive(false);
            GameObject g = currentWeapon;

            for (int i = playerWeapons.Count - 1; i >= 0; i--)
            {
                if (currentWeapon != playerWeapons[i])
                {
                    currentWeapon = playerWeapons[i];
                    break;
                }
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
