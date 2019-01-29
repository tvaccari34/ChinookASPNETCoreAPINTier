﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly ChinookContext _context;
        
        

        public InvoiceLineRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> InvoiceLineExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<InvoiceLine>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.GetAllInvoiceLinesAsync();
        }

        public async Task<InvoiceLine> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.GetInvoiceLineAsync(id);
        }

        public async Task<InvoiceLine> AddAsync(InvoiceLine newInvoiceLine,
            CancellationToken ct = default(CancellationToken))
        {
            _context.InvoiceLine.Add(newInvoiceLine);
            await _context.SaveChangesAsync(ct);
            return newInvoiceLine;
        }

        public async Task<bool> UpdateAsync(InvoiceLine invoiceLine, CancellationToken ct = default(CancellationToken))
        {
            if (!await InvoiceLineExists(invoiceLine.InvoiceLineId, ct))
                return false;
            _context.InvoiceLine.Update(invoiceLine);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await InvoiceLineExists(id, ct))
                return false;
            var toRemove = _context.InvoiceLine.Find(id);
            _context.InvoiceLine.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<InvoiceLine>> GetByInvoiceIdAsync(int id,
            CancellationToken ct = default(CancellationToken))
        {
            return await _context.GetInvoiceLinesByInvoiceIdAsync(id);
        }

        public async Task<List<InvoiceLine>> GetByTrackIdAsync(int id,
            CancellationToken ct = default(CancellationToken))
        {
            return await _context.GetInvoiceLinesByTrackIdAsync(id);
        }
    }
}