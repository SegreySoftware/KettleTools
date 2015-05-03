using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KettleModel;
using Nancy;

namespace KettleUI.modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = ctx =>
            {
                List<string> names = RepositoryManager.Instance.GetNames();
                return View["index", names];
            };
        }
    }
}
