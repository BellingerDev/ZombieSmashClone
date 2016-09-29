using System;


namespace Game.Entity
{
    public interface IEntity
    {
        int TakeDamage();
        void ObtainDamage(int damage);
        void ConfigureBalance(object data);

        int Health                      { get; }
        
        Action<int> OnDamageObtained    { get; set; }
        Action<int> OnDamageTaken       { get; set; }
        Action OnDied                   { get; set; }
    }
}
