using UnityEngine;
using System;

/// <summary>
/// 체력 시스템을 담당하는 컴포넌트
/// BaseEntity와 함께 사용하여 엔티티의 생명력을 관리합니다.
/// </summary>
public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private float invincibilityTime = 0.5f;
    
    // 이벤트들 - 다른 시스템들이 체력 변화를 감지할 수 있게 해줍니다
    public event Action<int, int> OnHealthChanged;        // (이전 체력, 현재 체력)
    public event Action<int> OnDamageTaken;               // 받은 데미지
    public event Action<int> OnHealthRestored;            // 회복된 체력
    public event Action OnDeath;                          // 사망
    public event Action OnRevive;                         // 부활
    
    // 프로퍼티들
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public float HealthPercentage => maxHealth > 0 ? (float)currentHealth / maxHealth : 0f;
    public bool IsDead => currentHealth <= 0;
    public bool IsInvincible => isInvincible;
    
    private float lastDamageTime;
    
    #region Unity Lifecycle
    
    private void Awake()
    {
        // 초기 체력을 최대 체력으로 설정
        currentHealth = maxHealth;
    }
    
    #endregion
    
    #region Public Methods
    
    /// <summary>
    /// 데미지를 받습니다
    /// </summary>
    /// <param name="damage">받을 데미지 양</param>
    /// <param name="source">데미지의 출처 (어떤 엔티티가 공격했는지)</param>
    public void TakeDamage(int damage, GameObject source = null)
    {
        // 무적 상태이거나 이미 죽어있으면 데미지를 받지 않음
        if (isInvincible || IsDead || damage <= 0)
            return;
        
        // 무적 시간 체크
        if (Time.time - lastDamageTime < invincibilityTime)
            return;
        
        int previousHealth = currentHealth;
        currentHealth = Mathf.Max(0, currentHealth - damage);
        lastDamageTime = Time.time;
        
        // 이벤트 발생
        OnDamageTaken?.Invoke(damage);
        OnHealthChanged?.Invoke(previousHealth, currentHealth);
        
        // 로그 출력
        string sourceName = source ? source.name : "Unknown";
        Debug.Log($"[{name}] Took {damage} damage from {sourceName}. Health: {currentHealth}/{maxHealth}");
        
        // 사망 체크
        if (IsDead)
        {
            OnDeath?.Invoke();
            Debug.Log($"[{name}] is DEAD!");
        }
    }
    
    /// <summary>
    /// 체력을 회복합니다
    /// </summary>
    /// <param name="amount">회복할 체력 양</param>
    public void RestoreHealth(int amount)
    {
        if (IsDead || amount <= 0)
            return;
        
        int previousHealth = currentHealth;
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        
        // 이벤트 발생
        OnHealthRestored?.Invoke(amount);
        OnHealthChanged?.Invoke(previousHealth, currentHealth);
        
        Debug.Log($"[{name}] Restored {amount} health. Health: {currentHealth}/{maxHealth}");
    }
    
    /// <summary>
    /// 최대 체력을 설정합니다
    /// </summary>
    /// <param name="newMaxHealth">새로운 최대 체력</param>
    public void SetMaxHealth(int newMaxHealth)
    {
        if (newMaxHealth <= 0)
            return;
        
        maxHealth = newMaxHealth;
        
        // 현재 체력이 새로운 최대 체력을 넘지 않도록 조정
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        Debug.Log($"[{name}] Max health set to {maxHealth}");
    }
    
    /// <summary>
    /// 체력을 완전히 회복합니다
    /// </summary>
    public void FullHeal()
    {
        if (IsDead)
            return;
        
        int previousHealth = currentHealth;
        currentHealth = maxHealth;
        
        OnHealthChanged?.Invoke(previousHealth, currentHealth);
        Debug.Log($"[{name}] Fully healed. Health: {currentHealth}/{maxHealth}");
    }
    
    /// <summary>
    /// 부활합니다
    /// </summary>
    /// <param name="healthPercentage">부활 후 체력 비율 (0.0 ~ 1.0)</param>
    public void Revive(float healthPercentage = 0.5f)
    {
        if (!IsDead)
            return;
        
        healthPercentage = Mathf.Clamp01(healthPercentage);
        currentHealth = Mathf.RoundToInt(maxHealth * healthPercentage);
        
        OnRevive?.Invoke();
        OnHealthChanged?.Invoke(0, currentHealth);
        
        Debug.Log($"[{name}] Revived with {healthPercentage * 100}% health. Health: {currentHealth}/{maxHealth}");
    }
    
    /// <summary>
    /// 무적 상태를 설정합니다
    /// </summary>
    /// <param name="invincible">무적 여부</param>
    public void SetInvincible(bool invincible)
    {
        isInvincible = invincible;
        Debug.Log($"[{name}] Invincibility set to: {invincible}");
    }
    
    #endregion
    
    #region Editor Methods (Inspector에서 테스트용)
    
    [ContextMenu("Take 10 Damage")]
    private void TestTakeDamage()
    {
        TakeDamage(10);
    }
    
    [ContextMenu("Restore 10 Health")]
    private void TestRestoreHealth()
    {
        RestoreHealth(10);
    }
    
    [ContextMenu("Full Heal")]
    private void TestFullHeal()
    {
        FullHeal();
    }
    
    #endregion
}
