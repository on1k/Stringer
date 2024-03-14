using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stringer.Data.Db;
using Stringer.Data.Db.Extentions;
using Stringer.Data.Db.Repository.StringEntities;
using Stringer.Data.Domain.Models;

namespace Stringer.Data.Controllers;

/// <summary>
/// Конечно можно было бы сделать валидацию моделей отдельным слоем, а саму команду в тот же медиатор вставить
/// Но за один вечер как то не поспевается все это сделать
/// </summary>
[Route("api/stringers/")]
[ApiController]
[Authorize]
public class StringersController : BaseController
{
    private IStringEntityRepository _stringEntityRepository;

    public StringersController(IStringEntityRepository stringEntityRepository)
    {
        _stringEntityRepository = stringEntityRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetStringEntities()
    {
        var result = await _stringEntityRepository.GetStringEntities(UserId);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStringEntity([FromBody]string text)
    {
        if (!text.IsValidLength())
            return BadRequest("Text is too long");
        var result = await _stringEntityRepository.CreateStringEntity(text, UserId);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateStringEntity([FromBody]string text, [FromQuery]string id)
    {
        if (!text.IsValidLength() || string.IsNullOrEmpty(id))
            return BadRequest("Text is too long or empty id");
        await _stringEntityRepository.UpdateStringEntity(new StringEntity{Id = id, OwnerId = UserId, Text = text });
        return Ok("OK");
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteStringEntity([FromQuery]string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest("Empty id");
        await _stringEntityRepository.DeleteStringEntity(id);
        return Ok("OK");
    }
}