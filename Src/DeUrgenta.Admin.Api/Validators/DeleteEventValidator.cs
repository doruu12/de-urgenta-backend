﻿using System.Threading.Tasks;
using DeUrgenta.Admin.Api.Commands;
using DeUrgenta.Common.Validation;
using DeUrgenta.Domain;
using Microsoft.EntityFrameworkCore;

namespace DeUrgenta.Admin.Api.Validators
{
    public class DeleteEventValidator : IValidateRequest<DeleteEvent>
    {
        private readonly DeUrgentaContext _context;

        public DeleteEventValidator(DeUrgentaContext context)
        {
            _context = context;
        }

        public async Task<bool> IsValidAsync(DeleteEvent request)
        {
            var ev = await _context.Events.FirstOrDefaultAsync(x => x.Id == request.EventId);
            if (ev == null)
            {
                return false;
            }

            return true;
        }
    }
}
