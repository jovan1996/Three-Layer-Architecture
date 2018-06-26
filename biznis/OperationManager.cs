using biznis.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis
{
    public class OperationManager
    {
        private static volatile OperationManager singleton;

        public static object SyncObj = new object();

        public static OperationManager Singleton
        {
            get
            {
                if (singleton == null)
                {
                    lock (SyncObj)
                    {
                        if (singleton == null)
                        {
                            singleton = new OperationManager();
                        }
                    }
                }
                return singleton;
            }
        }

        private OperationManager() { }

        private skiiiEntities entities = new skiiiEntities();

        private OperationResult result = new OperationResult();

        public OperationResult ExecuteOperation(Operation op)
        {
            try
            {
                return op.Execute(entities);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = false;

            }
            return result;
        }
    }
}
