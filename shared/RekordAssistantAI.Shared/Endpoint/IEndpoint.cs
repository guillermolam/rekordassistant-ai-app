using Microsoft.AspNetCore.Routing;

namespace RekordAssistant.Shared.Endpoint;

public interface IEndpoint
{
	void MapEndpoint(IEndpointRouteBuilder app);
}
