﻿using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Repository.Implementation
{
    public class TenancyRepository : Repository<Tenancy>, ITenancyRepository
    {
        public TenancyRepository(CromwoodContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tenancy>> GetTenancyForList()
        {
            return await _context.Tenancies.Include(x => x.Property).ThenInclude(x => x.Asset).Include(x => x.Property).ThenInclude(x => x.PropertyType).Include(x => x.RentFrequency).ToListAsync();
        }

        public async Task<Tenancy> GetTenancyOverView(Guid tenancyId)
        {
            return await _context.Tenancies.Include(x => x.Property).ThenInclude(x => x.Asset).Include(x => x.Property).ThenInclude(x => x.PropertyType).Include(x => x.RentFrequency).Include(x => x.TenancyType).FirstOrDefaultAsync(x => x.Id == tenancyId);
        }

        public async Task<Tenancy> GetTenancyViewDetail(Guid tenancyId)
        {
            return await _context.Tenancies.FirstOrDefaultAsync(x => x.Id == tenancyId);
        }

        public async Task<int> AddTenancy(Tenancy tenancy)
        {
            try
            {
                await _context.Tenancies.AddAsync(tenancy);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> EditTenancy(Tenancy tenancy)
        {
            try
            {
                _context.Tenancies.Update(tenancy);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ICollection<Tenant>> GetTenancyTenants(Guid tenancyId)
        {
            return await _context.TenancyTenants.Where(x => x.TenancyId == tenancyId).Select(x => x.Tenant).ToListAsync();
        }

        public async Task<int> LinkTenancyTenant(TenancyTenant tenancyTenant)
        {
            try
            {
                _context.TenancyTenants.Update(tenancyTenant);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ICollection<TenancyNote>> GetTenancyNotes(Guid tenancyId)
        {
            return await _context.TenancyNotes.Where(x => x.TenancyId == tenancyId).ToListAsync();
        }

        public async Task<TenancyNote> GetTenancyNote(Guid id)
        {
            return await _context.TenancyNotes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddNote(TenancyNote note)
        {
            try
            {
                note.Id = Guid.NewGuid();
                await _context.TenancyNotes.AddAsync(note);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ModifyNote(TenancyNote note)
        {
            try
            {
                _context.TenancyNotes.Update(note);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteNote(Guid id)
        {
            try
            {
                var note = await _context.TenancyNotes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.TenancyNotes.Remove(note);
                await _context.SaveChangesAsync();
                return note.FileUrl;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ICollection<TenancyDocument>> GetTenancyDocuments(Guid tenancyId)
        {
            return await _context.TenancyDocuments.Where(x => x.TenancyId == tenancyId).ToListAsync();
        }

        public async Task<TenancyDocument> GetTenancyDocument(Guid id)
        {
            return await _context.TenancyDocuments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddDocument(TenancyDocument document)
        {
            try
            {
                document.Id = Guid.NewGuid();
                await _context.TenancyDocuments.AddAsync(document);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ModifyDocument(TenancyDocument document)
        {
            try
            {
                _context.TenancyDocuments.Update(document);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteDocument(Guid id)
        {
            try
            {
                var document = await _context.TenancyDocuments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.TenancyDocuments.Remove(document);
                await _context.SaveChangesAsync();
                return document.DocUrl;
            }
            catch
            {
                throw;
            }
        }
    }
}
