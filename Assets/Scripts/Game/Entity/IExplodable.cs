namespace Game.Entity
{
    public interface IExplodable
    {
        string BombId   { get; }
        int BombsCount  { get; }
        int BombDamage  { get; }

        void ExplodeBomb();
    }
}
