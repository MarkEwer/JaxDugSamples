using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain
{
    public class OperationResult
    {
        public string PersistenceId { get; protected set; }
        public bool IsSuccess { get; protected set; }

        public static OperationResult Failure(string id)
        {
            return new OperationResult { IsSuccess=false, PersistenceId=id };
        }

        public static OperationResult Success(string id)
        {
            return new OperationResult { IsSuccess = true, PersistenceId = id };
        }
    }
}
