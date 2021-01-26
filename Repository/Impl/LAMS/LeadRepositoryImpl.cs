using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloBiz.Data;
using HaloBiz.Model.LAMS;
using HaloBiz.Repository.LAMS;
using Microsoft.EntityFrameworkCore;

namespace HaloBiz.Repository.Impl.LAMS
{
    public class LeadRepositoryImpl : ILeadRepository
    {
        private readonly DataContext _context;

        public LeadRepositoryImpl(DataContext context)
        {
            this._context = context;
        }
        public void DeleteLead(Lead lead)
        {
            lead.IsDeleted = true;
            _context.Leads.Update(lead);
            
        }

        public async Task<IEnumerable<Lead>> FindAllLead()
        {
            return await _context.Leads.Where(lead => lead.IsDeleted == false).ToListAsync();

        }

        public async Task<Lead> FindLeadById(long Id)
        {
            var lead =  await _context.Leads.Include(lead => lead.GroupType)
                .Include(lead => lead.DropReason)
                .FirstOrDefaultAsync(lead => lead.Id == Id);
            lead.DropReason = await _context.DropReasons.FirstOrDefaultAsync(dropReason => lead.DropReasonId == dropReason.Id);
            if(lead.PrimaryContactId != null)
                lead.PrimaryContact = await _context.LeadContacts.FirstOrDefaultAsync(primaryContact => lead.PrimaryContactId == primaryContact.Id);
            if(lead.SecondaryContactId != null)
                lead.SecondaryContact = await _context.LeadContacts.FirstOrDefaultAsync(contact => lead.SecondaryContactId == contact.Id);
            lead.LeadDivisions = await _context.LeadDivisions.Where(division => division.LeadId == lead.Id).ToListAsync();
            return lead;
        }

        

        public async Task<Lead> FindLeadByReferenceNo(string refNo)
        {
            var lead =  await _context.Leads.Include(lead => lead.GroupType)
                .Include(lead => lead.DropReason)
                .FirstOrDefaultAsync(lead => lead.ReferenceNo == refNo);
            lead.DropReason = await _context.DropReasons.FirstOrDefaultAsync(dropReason => lead.DropReasonId == dropReason.Id);
            if(lead.PrimaryContactId != null)
                lead.PrimaryContact = await _context.LeadContacts.FirstOrDefaultAsync(primaryContact => lead.PrimaryContactId == primaryContact.Id);
            if(lead.SecondaryContactId != null)
                lead.SecondaryContact = await _context.LeadContacts.FirstOrDefaultAsync(contact => lead.SecondaryContactId == contact.Id);
            lead.LeadDivisions = await _context.LeadDivisions.Where(division => division.LeadId == lead.Id).ToListAsync();
            return lead;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Lead> SaveLead(Lead lead)
        {
            var savedLead = await _context.Leads.AddAsync(lead);
            return savedLead.Entity;
        }

        public Lead UpdateLead(Lead lead)
        {
            var updatedLead =  _context.Leads.Update(lead);
            return updatedLead.Entity;
        }
    }
}