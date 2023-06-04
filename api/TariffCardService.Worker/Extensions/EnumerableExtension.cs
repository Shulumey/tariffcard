using System;
using System.Collections.Generic;

namespace TariffCardService.Worker.Extensions
{
	/// <summary>
	/// Класс для дополнительных методов обработки перечислений.
	/// </summary>
    public static class EnumerableExtension
    {
	    /// <summary>
	    /// Возвращает отдельные элементы из последовательности в соответствии с указанной функцией селектора ключа.
	    /// </summary>
	    /// <param name="sources"> Последовательность, из которой удаляются повторяющиеся элементы.</param>
	    /// <param name="keySelector"> Функция для извлечения ключа для каждого элемента.</param>
	    /// <typeparam name="TSource"> Тип элементов источника данных.</typeparam>
	    /// <typeparam name="TKey"> Тип ключа, по которому нужно различать элементы источника данных.</typeparam>
	    /// <returns> Последовательность которая содержит различные элементы из исходной последовательности.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> sources,
            Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var source in sources)
            {
                if (seenKeys.Add(keySelector(source)))
                    yield return source;
            }
        }
    }
}