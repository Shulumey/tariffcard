using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using TariffCardService.DataAccess.Entities;

namespace TariffCardService.DataAccess.Interfaces
{
	/// <summary>
	/// Интерфейс для работы с базой данных снимков данных тарифной карты.
	/// </summary>
	public interface ITariffCardDbContext
	{
		/// <summary>
		/// Снимок данных о комплексах, домах и объектах в них, на опредленную дату.
		/// </summary>
		DbSet<SnapshotCatalog> SnapshotsCatalog { get; }

		/// <summary>
		/// Снимок данных представления объектов недвижимости.
		/// </summary>
		DbSet<ObjectSnapshot> ObjectSnapshots { get; }

		/// <summary>
		/// Снимок данных представления корпуса с данными об установленной комиссии на него, типе объектов,
		/// максимальной и минимальной комиссии объектов в нём.
		/// </summary>
		DbSet<HouseSnapshot> HouseSnapshots { get; }

		/// <summary>
		/// Снимок данных представления комплекса с данными о продавце,
		/// типе объектов, максимальной и минимальной комиссии в нём, и данными о корпусах внутри него.
		/// </summary>
		DbSet<ComplexSnapshot> ComplexSnapshots { get; }

		/// <summary>
		/// Представление объекта недвижимости.
		/// </summary>
		DbSet<CommissionObjectGroup> CommissionObjects { get; }

		/// <summary>
		/// Представление корпуса с данными об установленной комиссии на него,
		/// типе объектов , максимальной и минимальной комиссии объектов в нём.
		/// </summary>
		DbSet<CommissionHouseGroup> CommissionHouseGroups { get; }

		/// <summary>
		/// Представление комплекса с данными о продавце, типе объектов,
		/// максимальной и минимальной комиссии в нём,
		/// и данными о корпусах внутри него.
		/// </summary>
		DbSet<CommissionComplex> CommissionComplexes { get; }

		/// <summary>
		/// Синонимы для поиска.
		/// </summary>
		DbSet<SearchParamAlias> SearchParamAliases { get; }

		/// <summary>
		/// Возвращает EntityEntry&lt;TEntity&gt; объект для данной сущности, предоставляющей доступ к сведениям о сущности, и возможность выполнять действия с сущностью.
		/// </summary>
		/// <param name="entity">Сущность из базы данных.</param>
		/// <typeparam name="TEntity">Класс сущности.</typeparam>
		/// <returns>EntityEntry&lt;TEntity&gt; объект для сущности класса &lt;TEntity&gt;.</returns>
		EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
			where TEntity : class;

		/// <summary>
		/// Задача, представляющая асинхронную операцию сохранения изменений.
		/// </summary>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns> Результат задачи содержит число записей о состоянии, записанных в базу данных.</returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}