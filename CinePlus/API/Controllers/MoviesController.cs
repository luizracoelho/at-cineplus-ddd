using CinePlus.APP;
using CinePlus.Domain.ViewModels.Movies;
using Microsoft.AspNetCore.Mvc;

namespace CinePlus.API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class MoviesController : ControllerBase
{
    private readonly MovieApp _app;

    public MoviesController(MovieApp app) => _app = app;

    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        var movies = await _app.ListAsync();
        return Ok(movies);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> FindAsync(long id)
    {
        var movie = await _app.FindAsync(id);
        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateMovieVm vm)
    {
        var movie = await _app.AddAsync(vm);
        return Ok(movie);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateAsync(long id, [FromBody] CreateMovieVm vm)
    {
        var movie = await _app.UpdateAsync(id, vm);
        return Ok(movie);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> RemoveAsync(long id)
    {
        var isSuccess = await _app.RemoveAsync(id);

        if (!isSuccess)
            return BadRequest();

        return Ok();
    }
    
    [HttpPatch("{id:long}/activate")]
    public async Task<IActionResult> ActivateAsync(long id)
    {
        var isSuccess = await _app.ActivateAsync(id);

        if (!isSuccess)
            return BadRequest();

        return Ok();
    }
    
    [HttpPatch("{id:long}/deactivate")]
    public async Task<IActionResult> DeactivateAsync(long id)
    {
        var isSuccess = await _app.DeactivateAsync(id);

        if (!isSuccess)
            return BadRequest();

        return Ok();
    }
}