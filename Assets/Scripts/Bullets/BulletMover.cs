using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private BulletData _data; // Данные пули (скорость и т.д.)
    private Vector3 _direction; // Направление движения пули

    private bool _isCollided;
    private Wall _previousWall;

    private void OnEnable()
    {
        // Инициализация направления при активации пули
        _direction = transform.forward;
    }

    private void OnDisable()
    {
        _previousWall = null;
    }

    private void Update()
    {
        // Движение пули в текущем направлении
        transform.Translate(_direction * _data.Speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Wall wall))
        {
            if (wall == _previousWall)
                return;
            _previousWall = wall;
            // Получаем нормаль поверхности, с которой столкнулась пуля
            Vector3 normal = collision.contacts[0].normal;

            // Обновляем направление движения пули после рикошета
            _direction = Vector3.Reflect(_direction, normal).normalized;

            transform.forward = _direction; // Обновляем forward, чтобы пуля смотрела в правильном направлении

            _isCollided = true;
            Debug.Log("Рикошет! Новое направление: " + _direction);
        }
    }
}