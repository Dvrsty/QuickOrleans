using Quick.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Quick.Web.Framework
{
    public class ApiResultAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            // 判断是否有异常,如果没有异常执行正常逻辑,如果出现任何异常,进入异常处理逻辑
            if (context.Result is ObjectResult || context.Result is EmptyResult || context.Result == null)
            {
                if (context.Exception == null)
                {
                    // 定义返回模型
                    var result = new ApiOutput();
                    result.Success = true;
                    // 取得由 API 返回的状态代码
                    result.Status = 200;

                    if (context.Result == null)
                    {
                        context.Result = new ObjectResult(result);
                    }
                    else
                    {
                        // 取得由 API 返回的资料
                        var objectResult = context.Result as ObjectResult;
                        result.Data = objectResult?.Value;
                        context.Result = new ObjectResult(result);
                    }
                }
            }
        }
    }
}
