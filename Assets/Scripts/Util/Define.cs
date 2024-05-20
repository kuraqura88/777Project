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

    public enum EnemyType
    {
        IF,
        Switch
    }

    public enum CharacterType
    {
        Normal,
        Rare,
        Unique,
        Epic
    }

    #region ========== DATA ==========

    public enum LoadDataType
    {
        None,
        Projectiles,
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
