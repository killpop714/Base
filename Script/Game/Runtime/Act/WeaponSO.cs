using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Weapon", fileName = "NewWeapon")]
public class WeaponSO : ScriptableObject
{
    public string DisplayName = "Weapon";

    public string id = "";
    public string Id => Id;
    [Tooltip("이 무기를 끼면 제공되는 공격 스킬들")]
    public ActionDef[] ActList;

    #if UNITY_EDITOR
    // 프로젝트에 추가/이름바꿈할 때 자동으로 GUID 생성 (비어있을 때만)
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(id))
            id = System.Guid.NewGuid().ToString("N");
    }
#endif

}