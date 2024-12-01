using UnityEngine;
using UnityEngine.Tilemaps;

    public class RobotController : MonoBehaviour
{
    public Vector3Int currentGridPosition; // Текущая позиция в сетке
    public Vector3Int direction = Vector3Int.up; // Направление взгляда
    public Tilemap tilemap; // Ссылка на Tilemap

    // Переменная для отслеживания получения сигнала
    private bool signalReceived = false;

    void Start()
    {
        // Получаем текущую позицию в сетке и обновляем позицию врага
        currentGridPosition = tilemap.WorldToCell(transform.position);
        UpdateEnemyPosition();
    }

    void UpdateEnemyPosition()
    {
        // Обновляем позицию робота в мире на основе текущей позиции в сетке
        transform.position = tilemap.GetCellCenterWorld(currentGridPosition);
    }

    // Метод для получения сигнала (например, от другого компонента или системы)
    public void ReceiveSignal()
    {
        signalReceived = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }

    public void Step()
    {
        Vector3Int newPosition = currentGridPosition + direction;
        if (IsValidMove(newPosition))
        {
            currentGridPosition = newPosition;
            UpdateEnemyPosition();
        }
    }

    public bool IsValidMove(Vector3Int position)
    {
        return !tilemap.HasTile(position);
    }

    public void Rotate()
    {
        if (direction == Vector3Int.up) direction = Vector3Int.right;
        else if (direction == Vector3Int.right) direction = Vector3Int.down;
        else if (direction == Vector3Int.down) direction = Vector3Int.left;
        else direction = Vector3Int.up;
    }
}