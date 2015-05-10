//
// Segrey Software licenses this file to you under the Apache License, Version 2.0
// (the "License"); you may not use this file except in compliance with 
// the License. You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
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
