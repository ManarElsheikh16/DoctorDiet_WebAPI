using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DoctorDiet.Models
{

  //public class FormFileModelBinder : IModelBinder
  //{
  //  public Task BindModelAsync(ModelBindingContext bindingContext)
  //  {
  //    var fieldName = bindingContext.FieldName;
  //    var httpRequest = bindingContext.HttpContext.Request;

  //    if (httpRequest.Form.Files.TryGetValue(fieldName, out var formFile))
  //    {
  //      bindingContext.Result = ModelBindingResult.Success(formFile);
  //    }

  //    return Task.CompletedTask;
  //  }
  //}
}
