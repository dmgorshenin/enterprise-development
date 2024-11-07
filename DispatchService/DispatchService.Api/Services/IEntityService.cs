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
    public Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Получение сущности при помощи id
    /// </summary>
    public Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Добавление сущности
    /// </summary>
    public Task<T?> AddAsync(CreateDto newEntity);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    public Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Изменение сущности
    /// </summary>
    public Task<bool> UpdateAsync(UpdateDto updatedEntity);
}
