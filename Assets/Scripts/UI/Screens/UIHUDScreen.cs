using Game;
using Game.Draggable;
using UnityEngine;
using UnityEngine.UI;
using Utils.Poolable;

namespace UI.Screens
{
    public class UIHUDScreen : UIScreen
    {
        [SerializeField]
        private string healthPointPoolId;

        [SerializeField]
        private Transform healthBarContainer;

        [SerializeField]
        private Text bombCountIndicator;

        [SerializeField]
        private string bombCountStringFormat;

        private Player player;


        public void OnBombClicked()
        {
            Player player = FindObjectOfType<Player>();

            if (player.BombsCount > 0)
            {
                GameObject bombGO = Pool.Instance.Get(player.BombId);
                IDraggable draggable = (IDraggable)bombGO.GetComponent(typeof(IDraggable));

                draggable.Prepare();
                TouchController.DragObject = draggable;

                Bomb bomb = bombGO.GetComponent<Bomb>();
                bomb.Setup(player.BombDamage, 4);

                bombGO.SetActive(true);
            }
        }

        public void OnPauseClicked()
        {
            GameController.Instance.SwitchState(GameController.GameState.Pause);
        }

        private void Update()
        {
            if (player == null)
                player = FindObjectOfType<Player>();

            if (player == null)
                return;

            bombCountIndicator.text = string.Format(bombCountStringFormat, player.BombsCount);

            SetHealthPointCount(player.Health);
        }

        public void SetHealthPointCount(int count)
        {
            while (healthBarContainer.childCount > count)
                Pool.Instance.Retrieve(healthBarContainer.GetChild(healthBarContainer.childCount - 1).gameObject);
            
            while (healthBarContainer.childCount < count)
            {
                GameObject point = Pool.Instance.Get(healthPointPoolId);

                point.transform.SetParent(healthBarContainer);

                point.transform.localPosition = Vector3.zero;
                point.transform.localScale = Vector3.one;
                point.transform.localEulerAngles = Vector3.zero;
                point.SetActive(true);
            }
        }
    }
}
