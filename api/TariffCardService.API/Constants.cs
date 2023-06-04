using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TariffCardService.API
{
	/// <summary>
	/// Константы.
	/// </summary>
	public static class Constants
	{
		/// <summary>
		/// Аутентификация.
		/// </summary>
		public static class Authentication
		{
			/// <summary>
			/// Схема по-умолчанию.
			/// </summary>
			public const string DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		}
	}
}