using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;

namespace GeoLib.Dal.Helpers
{
    public static class CollectionHelper
    {
        #region Properties

        #region Private

        private static readonly Random Randomizer = new Random();

        #endregion

        #endregion

        #region Members

        #region Public

        /// <summary>
        /// Проверяет, пустая или null ли коллекция
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="value">Коллекция</param>
        /// <returns>Истина, если коллекция пустая или null</returns>
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return value == null || !value.Any();
        }

        /// <summary>
        /// Проверяет, есть ли все элементы в коллекции
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="enumerable">Коллекция</param>
        /// <param name="values">Контрольная коллекция</param>
        /// <returns>Истина, если проверка успешна</returns>
        [DebuggerStepThrough]
        public static bool ContainsAll<T>(this IEnumerable<T> enumerable, IEnumerable<T> values)
        {
            return ContainsAll(enumerable, values, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Проверяет, есть ли все элементы в коллекции
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="enumerable">Коллекция</param>
        /// <param name="values">Контрольная коллекция</param>
        /// <param name="comparer">Сравниватель</param>
        /// <returns>Истина, если проверка успешна</returns>
        [DebuggerStepThrough]
        public static bool ContainsAll<T>(this IEnumerable<T> enumerable, IEnumerable<T> values, IEqualityComparer<T> comparer)
        {
            return values.All(value => enumerable.Contains(value, comparer));
        }

        /// <summary>
        /// Проверяет, есть ли хотя бы один элемент в коллекции
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="enumerable">Коллекция</param>
        /// <param name="values">Контрольная коллекция</param>
        /// <returns>Истина, если проверка успешна</returns>
        [DebuggerStepThrough]
        public static bool ContainsAny<T>(this IEnumerable<T> enumerable, IEnumerable<T> values)
        {
            return enumerable.Intersect(values).Any();
        }

        /// <summary>
        /// Проверяет, есть ли хотя бы один элемент в коллекции
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="enumerable">Коллекция</param>
        /// <param name="values">Контрольная коллекция</param>
        /// <returns>Истина, если проверка успешна</returns>
        [DebuggerStepThrough]
        public static bool ContainsAny<T>(this IEnumerable<T> enumerable, params T[] values)
        {
            return enumerable.ContainsAny(values.AsEnumerable());
        }

        /*
        /// <summary>
        /// Возвращает все элементы, кроме элементы контрольной коллекции
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="enumerable">Коллекция</param>
        /// <param name="values">Контрольная коллекция</param>
        /// <returns>Коллекция из элементов исходной коллекции, кроме элементы контрольной коллекции</returns>
        [DebuggerStepThrough]
        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, params T[] values)
        {
            return Except(enumerable, values.AsEnumerable());
        }
         * */

        /// <summary>
        /// Итерирует коллекцию
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="enumerable">Исходная коллекция</param>
        /// <param name="action">Итератор</param>
        public static void Iterate<T>(this IEnumerable<T> enumerable, Action<T, IterationInfo<T>> action)
        {
            if (enumerable == null)
                return;

            var index = 0;
            var any = false;
            var next = default(T);
            var previous = default(T);
            foreach (var item in enumerable)
            {
                if (any)
                    action(next, new IterationInfo<T>(index++, index == 1, false, previous, item));
                any = true;
                previous = next;
                next = item;
            }
            if (any)
                action(next, new IterationInfo<T>(index++, index == 1, true, previous, next));
        }

        /// <summary>
        /// Добавляет подколлекцию в коллекцию
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="source">Исходная коллекция</param>
        /// <param name="collection">Подколлекция</param>
        public static void Add<T>(this IEnumerable<T> source, params T[] collection)
        {
            Contract.Requires(source != null);
            if (collection == null)
                return;

            source.EvaluateAs<ICollection<T>>(list =>
            {
                var typed = list as List<T>;
                if (typed != null)
                    typed.AddRange(collection);
                else
                {
                    foreach (var item in collection)
                        list.Add(item);
                }
            }, true);
        }

        /// <summary>
        /// Приводит коллекцию к коллекции нуллабельных элементов
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="values">Исходная коллекция</param>
        /// <returns>Коллекция нуллабельных объектов</returns>
        public static IEnumerable<T?> AsNullable<T>(this IEnumerable<T> values)
            where T : struct
        {
            return values.Select(v => v.AsNullable());
        }

        /// <summary>
        /// Возвращает пустую коллекцию, если исходная - null
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="enumerable">Исходая коллекция</param>
        /// <returns>Пустая коллекция, если исходная - null</returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Возвращает случайный элемент из коллекции
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="collection"></param>
        /// <returns>Случайный элемент из коллекции</returns>
        public static T Random<T>(this IEnumerable<T> collection)
        {
            return collection.Randomize().FirstOrDefault();
        }

        /// <summary>
        /// Перемешивает коллецию
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="target">Исходная коллекция</param>
        /// <returns>Перемешанная коллекция</returns>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> target)
        {
            return target.OrderBy(x => Randomizer.Next());
        }

        /// <summary>
        /// Присоединяет элемент в коллекцию
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="collection">Коллекция</param>
        /// <param name="item">Объект</param>
        /// <returns>Новая коллекция</returns>
        public static IEnumerable<T> ConcatItem<T>(this IEnumerable<T> collection, T item)
        {
            return collection.Concat(new[] { item });
        }

        /// <summary>
        /// Пробует выполнить запрос
        /// </summary>
        /// <typeparam name="TSource">Тип исходных объектов</typeparam>
        /// <typeparam name="TResult">Тип результирующих объектов</typeparam>
        /// <param name="source">Исходная коллекция</param>
        /// <param name="selector">Селектор</param>
        /// <returns>Результат запроса</returns>
        public static IEnumerable<TResult> TrySelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (var item in source)
            {
                TResult result;
                try
                {
                    result = selector(item);
                }
                catch (Exception)
                {
                    continue;
                }

                yield return result;
            }

        }

        /// <summary>
        /// Возвращает первый неnull элемент
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="values">Исходная коллекция</param>
        /// <returns>Первый неnull элемент</returns>
        public static T FirstNotNull<T>(this IEnumerable<T> values)
            where T : class
        {
            var coll = values.Where(v => v != null).ToList();
            var firstNotNull = coll.Any() ? coll.First() : null;
            return firstNotNull;
        }

        /// <summary>
        /// Возвращает первый случайный элемент
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="container">Коллекция</param>
        /// <param name="random">Рандомизатор</param>
        /// <returns>Первый случайный элемент</returns>
        public static T FirstRandom<T>(this IList<T> container, Random random = null)
        {
            try
            {
                if (random == null)
                    random = new Random((int)DateTime.Now.Ticks);

                const int min = 0;
                var max = container.Count;

                var idx = random.Next(min, max);

                return container[idx];
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Манипулирует коллекцией
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="instance">Исходная коллекция</param>
        /// <param name="action">Действие</param>
        /// <param name="throwOnError">Флаг: выбрасывать исключения</param>
        /// <returns>Сманипулированная коллекция</returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> instance, Action<T> action, bool throwOnError = false)
        {
            foreach (var obj in instance)
            {
                try
                {
                    action(obj);
                }
                catch (Exception)
                {
                    if (throwOnError)
                    {
                        throw;
                    }
                }
                yield return obj;
            }
        }

        /// <summary>
        /// Манипулирует коллекцией
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="instance">Исходная коллекция</param>
        /// <param name="action">Действие</param>
        /// <returns>Сманипулированная коллекция</returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> instance, Action<T, int> action)
        {
            var index = 0;
            foreach (var obj in instance)
            {
                action(obj, index++);
                yield return obj;
            }
        }

        /// <summary>
        /// Манипулирует коллекцией
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="instance">Исходная коллекция</param>
        /// <param name="action">Действие</param>
        /// <returns>Сманипулированная коллекция</returns>
        public static void ForEach<T>(this IEnumerable<T> instance, Action<T> action)
        {
            foreach (var obj in instance)
                action(obj);
        }

        /// <summary>
        /// Манипулирует коллекцией
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="instance">Исходная коллекция</param>
        /// <param name="action">Действие</param>
        /// <returns>Сманипулированная коллекция</returns>
        public static void ForEach<T>(this IEnumerable<T> instance, Action<T, int> action)
        {
            var num = 0;
            foreach (var obj in instance)
                action(obj, num++);
        }

        /// <summary>
        /// Перемешивает коллекцию
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="source">Исходая коллекция</param>
        /// <returns>Перемешанная коллекция</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(s => Guid.NewGuid());
        }

        /// <summary>
        /// Возвращает все перестановки перечисления
        /// </summary>
        /// <typeparam name="T">Тип элементов перечисления</typeparam>
        /// <param name="source">Исходное перечисление</param>
        /// <returns>Все возможные перестановки перечисления</returns>
        [DebuggerStepThrough]
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> source)
        {
            var lst = source.ToList();
            if (lst.Count <= 1)
            {
                yield return lst;
            }
            else
            {
                foreach (var e in lst)
                {
                    var e1 = e;
                    var sourceWithoutE = lst.Where(i => !i.Equals(e1)).ToList();
                    var trans = sourceWithoutE.Permutations();
                    foreach (var tran in trans)
                    {
                        var r = new List<T> { e1 };
                        r.AddRange(tran);
                        yield return r;
                    }
                }
            }
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> AtLeastEmpty<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        public static bool NotNullAndAny<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        #endregion

        #endregion
    }
}