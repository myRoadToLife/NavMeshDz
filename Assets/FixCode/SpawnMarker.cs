using UnityEngine;

public class SpawnMarker : MonoBehaviour
{
    [SerializeField] private Marker _markerPrefab;
    private Marker _currentMarker;

    public void CreateNew(Ray ray)
    {
        float random = Random.Range(0.0f, 360f);
        Vector3 randomYRotate = new Vector3(0, random, 0);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            _currentMarker = Instantiate(_markerPrefab, hitInfo.point, Quaternion.Euler(randomYRotate));
        }
    }

    public void DestroyOld()
    {
        if (_currentMarker != null)
        {
            Destroy(_currentMarker.gameObject);
        }
    }
}
