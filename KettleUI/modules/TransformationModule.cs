using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KettleModel;
using Nancy;

namespace KettleUI.modules
{
    public class TransformationModule : NancyModule
    {
        public TransformationModule()
            : base("/repo")
        {
            Get["/{repo}/trans/{trans}"] = ctx =>
            {
                Repository repo = RepositoryManager.Instance.Get(ctx.repo);
                Transformation trans = repo.AllTransformations[ctx.trans];
                return trans;
            };
            Post["/{repo}/trans/{trans}"] = ctx =>
            {
                Repository repo = RepositoryManager.Instance.Get(ctx.repo);
                Transformation trans = repo.AllTransformations[ctx.trans];
                var name = Request.Form.Name;
                var directory = Request.Form.directory;
                trans.Move(directory, name);
                var key = trans.Key;

                // Reload repository with pottentially adjusted paths
                repo.Reload();
                trans = repo.AllTransformations[key];
                return trans;
            };

        }
    }
}
