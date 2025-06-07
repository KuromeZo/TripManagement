using Microsoft.EntityFrameworkCore;
using TripManagement.Data;
using TripManagement.DTOs;
using TripManagement.Models;

namespace TripManagement.Services;

public class TripService : ITripService
    {
        private readonly TripDbContext _context;

        public TripService(TripDbContext context)
        {
            _context = context;
        }

        public async Task<TripsResponseDto> GetTripsAsync(int page, int pageSize)
        {
            var totalTrips = await _context.Trips.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalTrips / pageSize);

            var trips = await _context.Trips
                .Include(t => t.CountryTrips)
                    .ThenInclude(ct => ct.Country)
                .Include(t => t.ClientTrips)
                    .ThenInclude(ct => ct.Client)
                .OrderByDescending(t => t.DateFrom)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TripDto
                {
                    Name = t.Name,
                    Description = t.Description,
                    DateFrom = t.DateFrom,
                    DateTo = t.DateTo,
                    MaxPeople = t.MaxPeople,
                    Countries = t.CountryTrips.Select(ct => new CountryDto
                    {
                        Name = ct.Country.Name
                    }).ToList(),
                    Clients = t.ClientTrips.Select(ct => new ClientDto
                    {
                        FirstName = ct.Client.FirstName,
                        LastName = ct.Client.LastName
                    }).ToList()
                })
                .ToListAsync();

            return new TripsResponseDto
            {
                PageNum = page,
                PageSize = pageSize,
                AllPages = totalPages,
                Trips = trips
            };
        }

        public async Task<bool> AssignClientToTripAsync(int idTrip, AssignClientDto clientDto)
        {
            // if client with given PESEL already exists
            var existingClient = await _context.Clients
                .FirstOrDefaultAsync(c => c.Pesel == clientDto.Pesel);
            
            if (existingClient != null)
            {
                throw new InvalidOperationException("Client with this PESEL already exists");
            }

            // if trip exists and DateFrom is in the future
            var trip = await _context.Trips.FindAsync(idTrip);
            if (trip == null)
            {
                throw new InvalidOperationException("Trip not found");
            }

            if (trip.DateFrom <= DateTime.Now)
            {
                throw new InvalidOperationException("Cannot register for a trip that has already occurred");
            }

            var newClient = new Client
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Telephone = clientDto.Telephone,
                Pesel = clientDto.Pesel
            };

            _context.Clients.Add(newClient);
            await _context.SaveChangesAsync();

            // if client is already registered for trip 
            var existingRegistration = await _context.ClientTrips
                .FirstOrDefaultAsync(ct => ct.IdClient == newClient.IdClient && ct.IdTrip == idTrip);
            
            if (existingRegistration != null)
            {
                throw new InvalidOperationException("Client is already registered for this trip");
            }

            // client-trip relation
            var clientTrip = new ClientTrip
            {
                IdClient = newClient.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientDto.PaymentDate
            };

            _context.ClientTrips.Add(clientTrip);
            await _context.SaveChangesAsync();

            return true;
        }
    }