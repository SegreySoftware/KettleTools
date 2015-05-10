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
