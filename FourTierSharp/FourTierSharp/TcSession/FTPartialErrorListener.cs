using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Teamcenter.Soa.Client.Model;

namespace FourTierSharp.TcSession
{
    class FTPartialErrorListener : PartialErrorListener
    {
        public void HandlePartialError(ErrorStack[] partialErrors)
        {
            List<string> errMessage = new List<string>();
            foreach (var err in partialErrors.Select(e => e.Messages))
            {
                var errMsg = String.Join("\n", err);
                MessageBox.Show(errMsg);
                errMessage.Add(errMsg);
            }
            throw new Exception(String.Join("\n", errMessage.ToArray()));
        }
    }
}
