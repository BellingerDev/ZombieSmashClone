using UnityEngine;


namespace Game.Entity
{
    public interface IEntitySpawnLimits
    {
        Vector3 LeftLimit { get; set; }
        Vector3 RightLimit { get; set; }
    }
}
