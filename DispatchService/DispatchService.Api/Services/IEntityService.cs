namespace DispatchService.Api.Services;

using System.Threading.Tasks;

/// <summary>
///  Интерфейс для сервисов сущностей
/// </summary>
public interface IEntityService<T, CreateDto, UpdateDto>
{
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Получение сущности при помощи id
    /// </summary>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Добавление сущности
    /// </summary>
    Task<T?> AddAsync(CreateDto newEntity);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Изменение сущности
    /// </summary>
    Task<bool> UpdateAsync(UpdateDto updatedEntity);
}
