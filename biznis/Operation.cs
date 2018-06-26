using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biznis.DataLayer;
namespace biznis
{
    public abstract class Operation
    {
        public abstract OperationResult Execute(skiiiEntities entities);
    }

    public class OperationResult
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public int Broj { get; set; }
        public BaseDTO[] Items { get; set; }

    }
}
