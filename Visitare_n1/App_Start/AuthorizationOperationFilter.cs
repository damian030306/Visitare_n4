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
                @default = "bearer WGlkqYRltAetsk3OYcAwLh6wiBwquUkXs2F2Om8YHvpVI7cUQb7juZpGUhS9bD9AUKg2YlFvR9KWXZ-_tH604eq_l7ht1k5kzRx2WHQvKjSer6V9P_qNYlXtaDwgxKUjqWF3PVUiDWgXo4mq39Ek4s-vVy__2xhQxjdw2HKhxlzWCAaM4dDM1PMOQx_SeWFaLj59Ki2xStY1-fR8ukBu3r_2dBsRVPOtKUGhDKmTVbK0fnMDORe0F01cBOZqZerTkH7eVflGtN1kq3bB8mb4A0zwG6e2SheizjD83Y3MdFx5Ub6ROSvQ-5H-_qk1qxTtnAguzbK8J46MhvjjKGiA3Zu9LyiKoSO3-fS_PIMbIq8n0JM5Pw-_nEKkEtVqqp2BqCUgan5eykxwVmiVBVHC4Xm86Ha-fryyavcib8BQzvQmg72r1N8acvL450TemC5HIVwzQp3NhB4_mG-vg9H7X9y53LnIT85oKT_hNKwWDzy-f_cHNc-TEllfra5P8rpfGnNAG821JFSwW1c9iZ5-LQ"
            });
        }
    }
}