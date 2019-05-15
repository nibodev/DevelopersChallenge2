using System;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Filters
{
    public class FileUploadFilter : IOperationFilter
    {
        public static readonly string ActionName = "uploadfile";
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.OperationId.Equals(ActionName)) return;

            operation.Parameters.Clear();
            operation.Parameters.Add(new NonBodyParameter()
            {
                Name = "file",
                In = "formData",
                Description = "Upload file",
                Required = true,
                Type = "file"
            });
            operation.Consumes.Add("application/form-data");
        }
    }
}