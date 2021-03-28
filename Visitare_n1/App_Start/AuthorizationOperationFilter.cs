using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace Visitare_n1.App_Start
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                description = "access token",
                required = false,
                type = "string",
                @default = "bearer D31giD91yqHLY-2s0YB7SJ_1Jy_Mky1H1J5t10MI9neEkWLrwt22WEHaEL8E3X2wEBHcfgTlwnAVYElWS2BZEsgqtcGs2tVGQXthGTk3jlclg0M6yq3DmvQwzIS4W4XuFPWHjCdKD75jNh7mYP8K8QQ8-amVCQpIf626SmkXerot4zG4u7hkiF6qLjqg6SDBtAgcMe9yyiFnSMOweakM-S7bgrn46YnQOwhkyaqSh4QV2lzS5W4lG45TT0MFEFPMWpuPtvuSKgeyBQiZcTJuPsawRNCI5nxvvRpZ6gZPIqjAuiWTtByVg42lto5ygoC3bBm8hAa_JlycQn0dX0o-kiRwglbwxcYsByTd1wdBS9AF6fiWDUT46gbVHRpSuAJay5hiWHufpFaZDPQTmRJ3vmqVrPoYEoBxuxbOYy0UUn5M7-F-aDNa2ua4xOmEDduDf03qlwCdzA0gjRMhOB-HMvCby3m2p97PSEKCIbYGN9nUyE1fMiYUX9nIuplI4uT-"
            });
        }
    }
}