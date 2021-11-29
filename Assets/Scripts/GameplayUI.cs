using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour{
    public Text weaponName;
    public Text ammoCount;
    public Text cureCount;

    void Update(){
        weaponName.text = WeaponSwitching.weaponNames;
        ammoCount.text = Gun.ammoCountTxt.ToString() + "/" + Gun.maxAmmoCountTxt.ToString();
        cureCount.text = Raycast.curesCount.ToString();
    }
}
