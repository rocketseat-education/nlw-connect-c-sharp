using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.Services.LoggedUser;
using TechLibrary.Api.UseCases.Checkouts;

namespace TechLibrary.Api.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class CheckoutsController : ControllerBase
{
    [HttpPost]
    [Route("{bookId}")]
    public IActionResult BookCheckout(Guid bookId)
    {
        var loggedUser = new LoggedUserService(HttpContext);

        var useCase = new RegisterBookCheckoutUseCase(loggedUser);

        useCase.Execute(bookId);

        return NoContent();
    }
}
