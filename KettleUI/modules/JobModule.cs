using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KettleModel;
using Nancy;

namespace KettleUI.modules
{
    public class JobModule : NancyModule
    {
        public JobModule() : base("/repo")
        {
            Get["/{repo}/job/{job}"] = ctx =>
            {
                Repository repo = RepositoryManager.Instance.Get(ctx.repo);
                Job job = repo.AllJobs[ctx.job];
                return job;
            };
            Post["/{repo}/job/{job}"] = ctx =>
            {
                Repository repo = RepositoryManager.Instance.Get(ctx.repo);
                Job job = repo.AllJobs[ctx.job];
                var name = Request.Form.Name;
                var directory = Request.Form.directory;
                job.Move(directory, name);
                var key = job.Key;

                // Reload repository with pottentially adjusted paths
                repo.Reload();
                job = repo.AllJobs[key];
                return job;
            };

        }
    }
}
