using UnityEngine;

namespace FixCode
{
    public class ControllerGame : MonoBehaviour
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private Camera _camera;
        [SerializeField] private SpawnMarker _marker;

        private CharacterView _characterView;
        private Character _character;
        private Mover _mover;

        private void Awake()
        {
            _mover = new Mover();

            _character = Instantiate(_characterPrefab);
            _character.Initialize(new Health());

            CharacterView characterView = _character.GetComponent<CharacterView>();

            if (characterView != null)
                characterView.Initialize(_character);

        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (_character == null)
                    return;

                if (Physics.Raycast(ray, out RaycastHit hitRay))
                {
                    _mover.ClickToMove(hitRay, _character.Agent);

                    _marker.DestroyOld();
                    _marker.CreateNew(ray);
                }
            }
        }
    }
}