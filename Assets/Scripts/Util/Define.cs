public class Define
{
    #region ========== PROJECTILES ==========

    public enum Projectile
    {
        Normal,
        BigProjectile,
        TriangleBullet,
        DifferenctBullet
    }

    public enum CharacterType
    {
        Normal,
        Rare,
        Unique,
        Epic,
        MAX
    }
    public enum LoadDataType
    {
        None,
        Projectiles,
    }

    public enum AttackDirection
    {
        Up,
        Down,
        Left,
        Right,
        Target
    }

    public enum EnemyType
    {
        Straight,
        Wave,
        UpDown,
        Diagonal
    }

    public enum EnemyName
    {
        If,
        For,
        Switch,        
        Public
    }

    public enum EnemyAttack
    {
        Attack1, // 직선 공격
        Attack2, // 3갈래 공격
        Attack3,  // 저격 공격
        Attack4   // 자폭 (충돌)
    }

    public enum Prefabs
    {
        None,
        Player,
        Enemy,
        Projectiles,
    }
    #endregion
}
