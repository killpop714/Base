using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private List<WeaponSO> data;

    [SerializeField] private List<Weapon> runtimeWeapon = new List<Weapon>();

    public void SetWeapon()
    {
        for(int i = 0; i<data.Count; i++)
        {
            runtimeWeapon.Add(new Weapon());
            data[i].CreateCheckDisplayname(runtimeWeapon[i].actList);
        }
    }
}