using System.Net;
using GraphQL;
using Microsoft.AspNetCore.Mvc;

namespace CocktailDev.Gateway;

public class ExecutionResultActionResult : IActionResult
{
    private readonly ExecutionResult executionResult;

    public ExecutionResultActionResult(ExecutionResult executionResult)
    {
        this.executionResult = executionResult;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var serializer =
            context.HttpContext.RequestServices.GetRequiredService<IGraphQLSerializer>();
        var response = context.HttpContext.Response;
        response.ContentType = "application/json";
        response.StatusCode = this.executionResult.Executed
            ? (int)HttpStatusCode.OK
            : (int)HttpStatusCode.BadRequest;
        await serializer.WriteAsync(response.Body, this.executionResult,
            context.HttpContext.RequestAborted);
    }
}
