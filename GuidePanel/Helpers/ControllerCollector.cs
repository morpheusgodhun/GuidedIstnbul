using Core.Entities;
using Core.IService;
using GuidePanel.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;

namespace GuidePanel.Helpers
{
    public class ControllerCollector
    {
        public ControllerCollector()
        {

        }
        public static List<string> GetConrollerNames()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(CustomBaseController));
            var controllers = assembly.DefinedTypes.Where(x => x.IsDefined(typeof(ControllerAttribute))).ToList();
            List<string> controllerNames = new();

            controllerNames.AddRange(from controller in controllers
                                     select controller.Name);

            return controllerNames;
        }
    }
}
