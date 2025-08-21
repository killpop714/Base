# 엔티티 시스템 (Entity System)

이 시스템은 Unity 게임에서 엔티티(게임 오브젝트)를 체계적으로 관리하기 위한 기본 구조를 제공합니다.

## 🏗️ 시스템 구조

### 1. IEntity (인터페이스)
- 모든 엔티티가 구현해야 하는 기본 기능들을 정의
- `EntityID`, `EntityName`, `IsActive` 등의 기본 속성
- `Initialize()`, `Cleanup()` 등의 기본 메서드

### 2. BaseEntity (추상 클래스)
- `MonoBehaviour`를 상속받아 Unity 생명주기와 연동
- `IEntity` 인터페이스를 구현
- 모든 엔티티의 공통 기능 제공
- 이벤트 시스템을 통한 상태 변화 알림

### 3. HealthSystem (컴포넌트)
- 체력 관리 전담 컴포넌트
- 데미지, 회복, 사망, 부활 등의 기능
- 이벤트를 통한 상태 변화 알림
- 무적 시간, 자동 회복 등 고급 기능

### 4. GameEntity (구체 클래스)
- `BaseEntity`를 상속받아 실제 게임에서 사용
- `HealthSystem`과 연동
- 기본적인 이동, 공격 기능 제공
- 확장 가능한 구조

## 🚀 사용법

### 기본 엔티티 생성
```csharp
// 1. 빈 GameObject 생성
GameObject entityObject = new GameObject("MyEntity");

// 2. GameEntity 컴포넌트 추가
GameEntity entity = entityObject.AddComponent<GameEntity>();

// 3. HealthSystem 자동 추가됨 (또는 수동으로 추가 가능)
HealthSystem health = entityObject.GetComponent<HealthSystem>();
```

### 체력 시스템 사용
```csharp
// 데미지 적용
health.TakeDamage(20);

// 체력 회복
health.RestoreHealth(10);

// 완전 회복
health.FullHeal();

// 부활
health.Revive(0.5f); // 50% 체력으로 부활

// 이벤트 구독
health.OnDeath += () => Debug.Log("엔티티가 사망했습니다!");
health.OnDamageTaken += (damage) => Debug.Log($"{damage} 데미지를 받았습니다!");
```

### 엔티티 이동 및 공격
```csharp
// 방향 이동
entity.Move(Vector3.forward);

// 목표 위치로 이동
entity.MoveTo(new Vector3(10, 0, 10));

// 다른 엔티티 공격
GameEntity target = FindObjectOfType<GameEntity>();
entity.Attack(target, 25);
```

## 🔧 확장 방법

### 새로운 엔티티 타입 생성
```csharp
public class PlayerEntity : GameEntity
{
    [Header("Player Specific")]
    [SerializeField] private float jumpForce = 5f;
    
    public void Jump()
    {
        // 점프 로직 구현
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
```

### 새로운 시스템 컴포넌트 추가
```csharp
public class StaminaSystem : MonoBehaviour
{
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina = 100f;
    
    public event Action<float> OnStaminaChanged;
    
    public void UseStamina(float amount)
    {
        currentStamina = Mathf.Max(0, currentStamina - amount);
        OnStaminaChanged?.Invoke(currentStamina);
    }
}
```

## 📋 Inspector 설정

### BaseEntity
- `Entity ID`: 엔티티의 고유 ID (자동 생성됨)
- `Entity Name`: 엔티티의 표시 이름
- `Is Active`: 엔티티 활성화 여부

### HealthSystem
- `Max Health`: 최대 체력
- `Current Health`: 현재 체력
- `Is Invincible`: 무적 상태 여부
- `Invincibility Time`: 무적 시간 (초)

### GameEntity
- `Move Speed`: 이동 속도
- `Rotation Speed`: 회전 속도

## 🎯 이벤트 시스템

모든 주요 상태 변화는 이벤트를 통해 알림을 받을 수 있습니다:

```csharp
// 엔티티 이벤트
entity.OnEntityInitialized += (e) => Debug.Log("엔티티 초기화됨");
entity.OnEntityActivated += (e) => Debug.Log("엔티티 활성화됨");
entity.OnEntityDeactivated += (e) => Debug.Log("엔티티 비활성화됨");

// 체력 이벤트
health.OnHealthChanged += (oldHealth, newHealth) => Debug.Log($"체력: {oldHealth} -> {newHealth}");
health.OnDamageTaken += (damage) => Debug.Log($"데미지: {damage}");
health.OnDeath += () => Debug.Log("사망!");
```

## 💡 팁과 주의사항

1. **컴포넌트 순서**: `BaseEntity` → `HealthSystem` → `GameEntity` 순서로 추가하는 것을 권장
2. **이벤트 구독**: `OnDestroy`에서 이벤트 구독을 해제하는 것을 잊지 마세요
3. **성능**: 많은 엔티티가 있을 때는 이벤트 호출을 최소화하는 것이 좋습니다
4. **확장성**: 새로운 기능을 추가할 때는 기존 시스템을 건드리지 않고 확장하는 것을 권장

## 🔄 업데이트 내역

- **v1.0**: 기본 엔티티 시스템 구현
- 체력 시스템, 기본 이동, 공격 기능
- 이벤트 기반 상태 변화 알림
- 확장 가능한 구조 설계
