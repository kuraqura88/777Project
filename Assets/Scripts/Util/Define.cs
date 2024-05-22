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

    public enum AttackDirection
    {
        Up,
        Down,
        Left,
        Right,
        Target
    }


    #endregion

    #region ========== Character Type ==========

    public enum CharacterType
    {
        Normal,
        Rare,
        Unique,
        Epic,
        MAX
    }


    #endregion

    public enum LoadDataType
    {
        None,
        Projectiles,
    }

    #region ========== Enemy ==========

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
        Straight, // 직선 공격
        Sector, // 3갈래 공격
        Targetting,  // 저격 공격
        Suicide   // 자폭 (충돌)
    }

    #endregion


    public enum Prefabs
    {
        None,
        Player,
        Audio,
        Enemy,
        Projectiles,
    }

    public enum SoundType
    {
        Bgm,
        Effect,

    }

    public enum Scene
    {
        Start,
        BasicStage,
        StandardStage,
        ChallangeStage,
        ClearStage,
        GameoverStage,
        BasicBossStage,
        StandardBossStage,
        ChallangeBossStage
    }
}
