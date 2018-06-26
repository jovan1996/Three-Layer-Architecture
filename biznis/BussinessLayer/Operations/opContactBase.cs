using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biznis.DataLayer;
using biznis.BussinessLayer.DTO;
namespace biznis.BussinessLayer.Operations
{
    public class ContactCriteria: SelectCriteria{ }


    public class opContactBase : Operation
    {
       public ContactCriteria Criteria = new ContactCriteria();

        public ContactDTO DTO = new ContactDTO();
        public override OperationResult Execute(skiiiEntities entities)
        {
            IQueryable<Contact> iqContact = entities.Contacts;

            if (Criteria.Id != 0)
            {
                iqContact = iqContact.Where(r => r.idContact == Criteria.Id);
            }

            IEnumerable<ContactDTO> ieContact =
                from c in iqContact
                select new ContactDTO
                {
                    Id=c.idContact,
                    Uuid=c.AspNetUser.UserName,     
                    message = c.message,
                    date = c.date
                };


            OperationResult result = new OperationResult();

            result.Items = ieContact.ToArray();
            result.Status = true;
            
            return result;
        }
    }
    
    public class opContactCreate : opContactBase
    {
        public override OperationResult Execute(skiiiEntities entities)
        {

            var contact = new Contact();
            contact.idUser = this.DTO.Uuid;
            contact.message = this.DTO.message;

            entities.Contacts.Add(contact);
            entities.SaveChanges();

            return base.Execute(entities);
        }

    }

    public class OpContactDelete : opContactBase
    {
        public int IdzaBrisanje { get; set; }
        public override OperationResult Execute(skiiiEntities entities)
        {
            Contact cat = entities.Contacts.Where(p => p.idContact == IdzaBrisanje).FirstOrDefault();

            if (cat != null)
            {
                entities.Contacts.Remove(cat);
                entities.SaveChanges();
                return base.Execute(entities);
            }
            else
            {
                OperationResult result = new OperationResult();
                result.Status = false;
                result.Message = "Doslo je do greske. Odredjeni postovi se nalaze u kategoriji";
                return result;
            }
        }
    }

}
