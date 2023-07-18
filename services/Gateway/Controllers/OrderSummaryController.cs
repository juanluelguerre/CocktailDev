using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Transport;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CocktailDev.Gateway.Controllers;

[ApiController]
[Route("api")]
public class OrderSummaryController : Controller
{
    private readonly IDocumentExecuter documentExecuter;
    private readonly ISchema schema;
    private readonly IOptions<GraphQLSettings> graphQLOptions;

    public OrderSummaryController(IDocumentExecuter documentExecuter, ISchema schema,
        IOptions<GraphQLSettings> options)
    {
        this.documentExecuter = documentExecuter;
        this.schema = schema;
        this.graphQLOptions = options;
    }

    [HttpPost("graphql")]
    public async Task<IActionResult> GraphQLAsync([FromBody] GraphQLRequest request)
    {
        var startTime = DateTime.UtcNow;

        var result = await this.documentExecuter.ExecuteAsync(s =>
        {
            s.Schema = this.schema;
            s.Query = request.Query;
            s.Variables = request.Variables;
            s.OperationName = request.OperationName;
            s.RequestServices = this.HttpContext.RequestServices;
            s.UserContext = new GraphQLUserContext
            {
                User = HttpContext.User,
            };
            s.CancellationToken = this.HttpContext.RequestAborted;
        });

        if (this.graphQLOptions.Value.EnableMetrics)
        {
            result.EnrichWithApolloTracing(startTime);
        }

        return new ExecutionResultActionResult(result);
    }
}
