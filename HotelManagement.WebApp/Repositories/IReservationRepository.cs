﻿namespace HotelManagement.WebApp.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task AddAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task DeleteAsync(int id);
    Task<bool> IsRoomAvailableAsync(int roomId, DateTime startDate, DateTime endDate);
}
