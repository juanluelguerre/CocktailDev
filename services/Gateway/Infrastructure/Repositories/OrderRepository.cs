using System.Text.Json;
using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly HttpClient httpClient;

    public OrderRepository(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        var response = await this.httpClient.GetAsync("/api/orders");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var orders = JsonSerializer.Deserialize<List<Order>>(content);

        return orders ?? new List<Order>();
    }
}
