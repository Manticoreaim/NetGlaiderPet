/* Интерфес управление контроллером глайдера
 * Нужен для удобства передачи данных в глайдер
 */

using UnityEngine;

public interface InterfaisGlaider
{
    // перемещение глайдера
    public void MoveGlaiderInterfais(Vector2 direction);

    // поворот и наклон глайдера 
    public void DirectionsrGlaiderInterfais(Vector3 direction, bool ActiveTurnX);

    // ускорение в перд
    public void BoostGlaiderInterfais();

    //Рывок глайдера DashGlaiderInterfais
    public void DashGlaiderInterfais(float direction);

    // ручник поворота StopTurnGlaiderInterfaisInterfais
    public void StopTurnGlaiderInterfais();

    //переключение режима работы (первое/третье лицо) ControlModeInterfaisInterfais
    public void ControlModeInterfais(bool Mode);

    //направление оружия по таргету DirectionGunsInterfaisInterfais
    public void DirectionGunsInterfais(Vector3 TargetPosition);

    // выстрел из орудий GunsShotInterfaisInterfais
    public void GunsShotInterfais();
}
