using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Teamcenter.Schemas.Soa._2006_03.Exceptions;
using Teamcenter.Soa.Client;
using Teamcenter.Soa.Exceptions;

namespace FourTierSharp.TcSession
{
    class FTExceptionHandler : ExceptionHandler
    {
        public void HandleException(InternalServerException ise)
        {
            MessageBox.Show(ise.Message);
            throw ise;
        }

        public void HandleException(CanceledOperationException coe)
        {
            MessageBox.Show(coe.Message);
            throw coe;
        }
    }
}
