using UnityEngine;


namespace Game.Draggable
{
    public interface IDraggable
    {
        void Prepare();
        void Activate();
        void Deactivate();

        void SetPos(Vector3 position);
    }
}
