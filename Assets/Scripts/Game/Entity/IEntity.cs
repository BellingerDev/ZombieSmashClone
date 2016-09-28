using System;


namespace Game.Entity
{
    public interface IEntity
    {
        int TakeDamage();
        void ObtainDamage(int damage);
        
        Action OnDied { get; set; }
    }
}
