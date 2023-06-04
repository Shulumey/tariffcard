using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using TariffCardService.DataAccess.Entities;

namespace TariffCardService.DataAccess.Factory
{
	/// <summary>
	/// Фабрика для условий фильтраций сущностей <see cref="CommissionComplex"/>.
	/// </summary>
	public static class CommissionComplexConditionFactory
	{
		/// <summary>
		/// Создает выражение фильтрации сущностей <see cref="CommissionComplex"/>.
		/// </summary>
		/// <param name="searchStrings">Фильтрующие строки.</param>
		/// <returns>Выражение фильтрации.</returns>
		public static Expression<Func<CommissionComplex, bool>> Create(IEnumerable<string> searchStrings)
		{
			Expression<Func<CommissionComplex, bool>> defaultCondition = _ => true;

			if (searchStrings.Any())
			{
				Expression<Func<CommissionComplex, bool>>[] expressions = searchStrings
					.Select(searchString => (Expression<Func<CommissionComplex, bool>>)(cc => cc.SellerName.ToUpper().Contains(searchString.ToUpper()) ||
					                                                                           cc.ComplexName.ToUpper().Contains(searchString.ToUpper())))
					.ToArray();

				var param = Expression.Parameter(typeof(CommissionComplex), "x");

				Expression body = Expression.Constant(false);

				body = expressions.Aggregate(body, (current, expression) => Expression.OrElse(current, Expression.Invoke(expression, param)));

				if (Expression.Lambda(body, param) is Expression<Func<CommissionComplex, bool>> finalExp)
				{
					return finalExp;
				}
			}

			return defaultCondition;
		}
	}
}