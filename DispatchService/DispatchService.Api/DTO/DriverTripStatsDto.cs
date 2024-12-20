﻿using DispatchService.Model.Entities;

namespace DispatchService.Api.DTO;

/// <summary>
/// DTO для хранения статистики поездок водителя.
/// </summary>
public class DriverTripStatsDto
{
    /// <summary>
    /// Получает или устанавливает водителя.
    /// </summary>
    public Driver? Driver { get; set; }

    /// <summary>
    /// Получает или устанавливает количество поездок для данного водителя.
    /// </summary>
    public int TripCount { get; set; }

    /// <summary>
    /// Получает или устанавливает среднее время поездки для данного водителя в часах.
    /// </summary>
    public double AvgTripTime { get; set; }

    /// <summary>
    /// Получает или устанавливает максимальное время поездки для данного водителя в часах.
    /// </summary>
    public double MaxTripTime { get; set; }
}