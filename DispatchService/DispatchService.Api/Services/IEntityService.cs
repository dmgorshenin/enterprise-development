﻿namespace DispatchService.Api.Services;

/// <summary>
///  Интерфейс для сервисов сущностей
/// </summary>
public interface IEntityService<T, CreateDTO, UpdateDTO>
{
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    public IEnumerable<T> GetAll();

    /// <summary>
    /// Получение сущности при помощи id
    /// </summary>
    T? GetById(int id);

    /// <summary>
    /// Добавление сущности
    /// </summary>
    T? Add(CreateDTO entity);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    bool Delete(int id);

    /// <summary>
    /// Изменение сущности
    /// </summary>
    bool Update(UpdateDTO updatedEntity);
}
