using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Teamcenter.Schemas.Soa._2006_03.Exceptions;
using Teamcenter.Soa.Client;
using Teamcenter.Soa.Exceptions;

namespace FourTierSharp.TcSession
{
    class FTExceptionHandler : ExceptionHandler
    {
        public void HandleException(InternalServerException ise)
        {
            
        }

        public void HandleException(CanceledOperationException coe)
        {
            
        }
    }
}
