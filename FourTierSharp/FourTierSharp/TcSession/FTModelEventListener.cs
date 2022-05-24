using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Teamcenter.Soa.Client.Model;
using Teamcenter.Soa.Exceptions;

namespace FourTierSharp.TcSession
{
    class FTModelEventListener : ModelEventListener
    {
        override public void LocalObjectChange(ModelObject[] objects)
        {
            if (objects.Length == 0) return;
            for (int i = 0; i < objects.Length; i++)
            {
                string uid = objects[i].Uid;
                string type = objects[i].GetType().Name;
                string name = "";
                if (objects[i].GetType().Name.Equals("WorkspaceObject"))
                {
                    ModelObject wo = objects[i];
                    try
                    {
                        name = wo.GetProperty("object_string").StringValue;
                    }
                    catch (NotLoadedException /*e*/) { } // just ignore
                }
                System.Console.WriteLine("    " + uid + " " + type + " " + name);
            }
        }


        override public void LocalObjectDelete(string[] uids)
        {
            if (uids.Length == 0) return;

            for (int i = 0; i < uids.Length; i++)
            {
                System.Console.WriteLine("    " + uids[i]);
            }
        }
    }
}
