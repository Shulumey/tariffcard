using System;

using Microsoft.AspNetCore.Mvc;

namespace TariffCardService.API.Controllers
{
	/// <summary>
	/// Контроллер методов, возвращающих системную информацию.
	/// </summary>
	[ApiController]
	[ApiVersion("1.0")]
	[Route("[controller]")]
	public class SystemController : ControllerBase
	{
		/// <summary>
		/// Возвращает доступность приложения на текущее время.
		/// </summary>
		/// <returns>Доступность приложения на текущее время.</returns>
		[HttpGet]
		[Route("ping")]
		public IActionResult Ping() => Ok(DateTime.Now.ToUniversalTime());
	}
}