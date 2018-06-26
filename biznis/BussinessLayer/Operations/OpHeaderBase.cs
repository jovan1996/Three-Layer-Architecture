using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biznis.DataLayer;
using biznis.BussinessLayer.DTO;
namespace biznis.BussinessLayer.Operations
{
    public class OpHeaderBase : Operation
    {
        public override OperationResult Execute(skiiiEntities entities)
        {
            IEnumerable<HeaderDTO> ieHeader =
              (from h in entities.Headers
               orderby h.idHeader descending
               select new HeaderDTO
               {
                   Id = h.idHeader,
                   heading = h.heading,
                   src = h.Image.src
               }).Take(1);

            OperationResult result = new OperationResult
            {
                Status = true,
                Items = ieHeader.ToArray()
            };

            return result;
        }
    }
}
