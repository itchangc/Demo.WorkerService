using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WorkerService.Application
{
    public interface Icontainer
    { 
       string ContainerName { get; set; }
    }
    public class MyContainer: Icontainer
    {
        protected string containerName = "Custom my container";

        public string ContainerName
        {
            get
            {
                return containerName;
            }
            set
            {
                containerName = value;
            }
        }
    }
}
