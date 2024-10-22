using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Mine _minePrefab;
    [SerializeField] private Transform[] _points;
    [SerializeField] private ViewCharacter _view;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (var point in _points)
            {
                Mine mine = Instantiate(_minePrefab, point.position, point.rotation);
                mine.Initialize(_view);
            }
        }
    }
}
