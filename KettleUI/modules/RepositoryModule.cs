using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KettleModel;
using Nancy;

namespace KettleUI.modules
{
    public class RepositoryModule : NancyModule
    {
        public RepositoryModule() : base("/repo")
        {
            Get["/{repo}"] = ctx =>
            {
                ViewBag.TypeFilter = "All";
                ViewBag.Filter = "All";
                return RepositoryManager.Instance.Get(ctx.repo);
            };
            Get["/{repo}/filter/{type}/{filter}"] = ctx =>
            {
                ViewBag.TypeFilter = ctx.Type;
                ViewBag.Filter = ctx.Filter;
                return RepositoryManager.Instance.Get(ctx.repo);
            };
            Get["/{repo}/Reload"] = ctx =>
            {
                ViewBag.TypeFilter = ctx.Type;
                ViewBag.Filter = ctx.Filter;
                Repository repo = RepositoryManager.Instance.Get(ctx.repo);
                repo.Reload();
                return repo;
            };
        }
    }
}
