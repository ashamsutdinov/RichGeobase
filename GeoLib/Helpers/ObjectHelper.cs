using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json;

namespace GeoLib.Helpers
{
    public static class ObjectHelper
    {
        #region Constructors

        static ObjectHelper()
        {
            ExtensionMembers = new Dictionary<WeakReference, Dictionary<string, object>>();

            // ReSharper disable ObjectCreationAsStatement
            new Timer(o =>
                // ReSharper restore ObjectCreationAsStatement
            {
                foreach (var key in ExtensionMembers.Keys.Where(k => !k.IsAlive).ToArray())
                    ExtensionMembers.Remove(key);
            }, null, 0, 10000);
        }

        #endregion

        #region Properties

        #region Private

        #region Static

        private static Dictionary<WeakReference, Dictionary<string, object>> ExtensionMembers { get; set; }

        #endregion

        #endregion

        #endregion

        #region Constants

        private static readonly Type ExtensionsType = typeof(ObjectHelper);

        private static readonly MethodInfo CastToTypeMethod = ExtensionsType.GetMethod("CastToType");

        #endregion

        #region Members

        #region Private

        /// <summary>
        /// метод создания/получения существующего словаря свойств-расширений объекта
        /// </summary>
        private static Dictionary<string, object> Extensions(this object targetObject, bool createIfNotExist)
        {
            //нет объекта - расширять нечего
            if (targetObject == null)
                return null;

            lock (targetObject)
            {
                //получаем ключ из вспомогательного словаря
                var weakKey = ExtensionMembers.Keys.FirstOrDefault(w => ReferenceEquals(w.Target, targetObject));

                if (weakKey != null)
                {
                    //ключ найден, возвращаем словарь
                    return ExtensionMembers[weakKey];
                }
                if (createIfNotExist)
                {
                    //создаем новый ключ и словарь для объекта
                    weakKey = new WeakReference(targetObject, false);
                    var members = new Dictionary<string, object>(StringComparer.Ordinal);
                    ExtensionMembers[weakKey] = members;
                    return members;
                }
            }
            return null;
        }

        #endregion

        #region Public

        /// <summary>
        /// Сравнивает объект с другими объектами на совпадение
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="obj1">Объект</param>
        /// <param name="objects">Другие объекты</param>
        /// <returns>Истина, если совпадает со всеми</returns>
        public static bool Equals<T>(this T obj1, params T[] objects)
        {
            var equals = true;

            for (var i = 0; i < objects.Length && equals; i++)
            {
                equals = object.Equals(obj1, objects[i]);
                if (!equals)
                    break;
            }

            return equals;
        }

        /// <summary>
        /// Очищает расширения для объекта
        /// </summary>
        /// <param name="targetObject">Объект</param>
        [DebuggerStepThrough]
        public static void ClearExtensions(this object targetObject)
        {
            //нет объекта - удалять нечего
            if (targetObject == null)
                return;

            lock (targetObject)
            {
                //получаем ключ из вспомогательного словаря
                var weakKey = ExtensionMembers.Keys.FirstOrDefault(w => ReferenceEquals(w.Target, targetObject));

                if (weakKey != null)
                {
                    //ключ найден, удаляем словарь
                    ExtensionMembers.Remove(weakKey);
                }
            }
        }

        /// <summary>
        /// Устанавливает свойство-расширение для объекта
        /// </summary>
        /// <param name="targetObject">Объект</param>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        [DebuggerStepThrough]
        public static void Extension(this object targetObject, string key, object value)
        {
            if (targetObject == null || String.IsNullOrEmpty(key))
                return;

            //получаем словарь свойств-расширений, либо создаем новый
            var extensions = targetObject.Extensions(true);

            //устанавливаем значение, либо удаляем его, если value == null	
            if (value != null)
                extensions[key] = value;
            else
            {
                lock (targetObject)
                {
                    extensions.Remove(key);
                }
            }
        }

        /// <summary>
        /// Получает значение свойства-расширения для объекта
        /// </summary>
        /// <typeparam name="T">Тип свойства</typeparam>
        /// <param name="targetObject">Целевой объект</param>
        /// <param name="key">Ключ</param>
        /// <returns>Значение свойства-расширения</returns>
        [DebuggerStepThrough]
        public static T Extension<T>(this object targetObject, string key)
        {
            if (targetObject != null && !String.IsNullOrEmpty(key))
            {
                //получаем словарь свойств-расширений, новый не создаем
                var extensions = targetObject.Extensions(false);

                //если есть словарь
                if (extensions != null)
                {
                    //если есть свойство нужное в словаре
                    if (extensions.ContainsKey(key))
                    {
                        lock (targetObject)
                        {
                            if (extensions.ContainsKey(key))
                            {
                                //и это свойство того типа, который мы ожидаем
                                var value = extensions[key];
                                if (value is T)
                                    //возвращаем значение
                                    return (T)extensions[key];
                            }
                        }
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        /// Преобразует к типу
        /// </summary>
        /// <typeparam name="T">Целевой тип</typeparam>
        /// <param name="targetObject">Объект</param>
        /// <returns>Объект целевого типа</returns>
        [DebuggerStepThrough]
        public static T As<T>(this object targetObject)
        {
            return targetObject == DBNull.Value ?
                default(T) :
                As(targetObject, default(T));
        }

        /// <summary>
        /// Преобразует к типу
        /// </summary>
        /// <typeparam name="T">Целевой тип</typeparam>
        /// <param name="targetObject">Объект</param>
        /// <param name="format">Провайдер преобразования</param>
        /// <returns>Объект целевого типа</returns>
        [DebuggerStepThrough]
        public static T As<T>(this object targetObject, IFormatProvider format)
        {
            return targetObject.As(default(T), format);
        }

        /// <summary>
        /// Преобразует к типу
        /// </summary>
        /// <typeparam name="T">Целевой тип</typeparam>
        /// <param name="targetObject">Объект</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <returns>Объект целевого типа</returns>
        [DebuggerStepThrough]
        public static T As<T>(this object targetObject, T defaultValue)
        {
            return targetObject.As(defaultValue, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Преобразует к типу
        /// </summary>
        /// <typeparam name="T">Целевой тип</typeparam>
        /// <param name="targetObject">Объект</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <param name="format">Провайдер преобразования</param>
        /// <returns>Объект целевого типа</returns>
        [DebuggerStepThrough]
        public static T As<T>(this object targetObject, T defaultValue, IFormatProvider format)
        {
            var t = typeof(T);
            var genericNullable = typeof(Nullable<>);
            if (t.IsGenericType && t.GetGenericTypeDefinition() == genericNullable)
            {
                var gArgs = t.GetGenericArguments();
                var notNullableType = gArgs.FirstOrDefault();
                t = notNullableType ?? t;
            }
            return (T)Convert.ChangeType(targetObject, t, format);
        }

        /// <summary>
        /// Получает результат выражения над объектом
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <param name="container">Объект</param>
        /// <param name="expression">Выражение</param>
        /// <returns>Результат выражения</returns>
        [DebuggerStepThrough]
        public static TResult Evaluate<T, TResult>(this T container, Func<T, TResult> expression)
            where T : class
        {
            return Evaluate(container, expression, default(TResult));
        }

        /// <summary>
        /// Получает результат выражения над объектом
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <param name="container">Объект</param>
        /// <param name="expression">Выражение</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <returns>Результат выражения</returns>
        [DebuggerStepThrough]
        public static TResult Evaluate<T, TResult>(this T container, Func<T, TResult> expression, TResult defaultValue)
            where T : class
        {
            return container == null ? defaultValue : expression(container);
        }

        /// <summary>
        /// Получает результат выражения над объектом
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="container">Объект</param>
        /// <param name="expression">Выражение</param>
        /// <returns>Результат выражения</returns>
        [DebuggerStepThrough]
        public static void Evaluate<T>(this T container, Action<T> expression)
            where T : class
        {
            if (container != null)
                expression(container);
        }

        /// <summary>
        /// Получает результат выражения над объектом с предварительным преобразованием типа
        /// </summary>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <typeparam name="TCast">Тип для преобразования</typeparam>
        /// <param name="container">Объект</param>
        /// <param name="expression">Выражение</param>
        /// <param name="throwIfNotCastable">Флаг: выбросить исключение, если не кастуется</param>
        /// <returns>Результат выражения</returns>
        [DebuggerStepThrough]
        public static TResult EvaluateAs<TCast, TResult>(this object container, Func<TCast, TResult> expression, bool throwIfNotCastable = false)
            where TCast : class
        {
            return EvaluateAs(container, expression, default(TResult), throwIfNotCastable);
        }

        /// <summary>
        /// Получает результат выражения над объектом с предварительным преобразованием типа
        /// </summary>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <typeparam name="TCast">Тип для преобразования</typeparam>
        /// <param name="container">Объект</param>
        /// <param name="expression">Выражение</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <param name="throwIfNotCastable">Флаг: выбросить исключение, если не кастуется</param>
        /// <returns>Результат выражения</returns>
        [DebuggerStepThrough]
        public static TResult EvaluateAs<TCast, TResult>(this object container, Func<TCast, TResult> expression, TResult defaultValue, bool throwIfNotCastable = false)
            where TCast : class
        {
            var asCont = container as TCast;

            if (asCont != null)
            {
                return expression(asCont);
            }

            if (throwIfNotCastable)
                throw new NotSupportedException();
            return defaultValue;
        }

        /// <summary>
        /// Получает результат выражения над объектом с предварительным преобразованием типа
        /// </summary>
        /// <typeparam name="TCast">Тип для преобразования</typeparam>
        /// <param name="container">Объект</param>
        /// <param name="expression">Выражение</param>
        /// <param name="throwIfNotCastable">Флаг: выбросить исключение, если не кастуется</param>
        /// <returns>Результат выражения</returns>
        [DebuggerStepThrough]
        public static void EvaluateAs<TCast>(this object container, Action<TCast> expression, bool throwIfNotCastable = false)
            where TCast : class
        {
            var asCont = container as TCast;
            if (asCont != null)
            {
                expression(asCont);
            }
            else
            {
                if (throwIfNotCastable)
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Содержится ли объект среди заданных
        /// </summary>
        [DebuggerStepThrough]
        public static bool In<T>(this T targetObject, IEqualityComparer<T> comparer, params T[] values)
        {
            return targetObject.In(comparer, values.AsEnumerable());
        }

        /// <summary>
        /// Проверяет на совпадение типа и выкидывает исключение, если объект не приводится к типу
        /// </summary>
        /// <typeparam name="T">Целевой тип</typeparam>
        /// <param name="obj">Исходный объект</param>
        /// <param name="throwIfNotCastable">Исключение в случае несоответствия типа</param>
        public static void CheckType<T>(this object obj, Exception throwIfNotCastable = null)
            where T : class
        {
            var cObj = obj as T;
            if (cObj == null && throwIfNotCastable != null)
                throw throwIfNotCastable;
        }

        /// <summary>
        /// Содержится ли объект среди заданных
        /// </summary>
        [DebuggerStepThrough]
        public static bool In<T>(this T targetObject, params T[] values)
        {
            return targetObject.In(values.AsEnumerable());
        }

        /// <summary>
        /// Содержится ли объект среди заданных
        /// </summary>
        [DebuggerStepThrough]
        public static bool In<T>(this T targetObject, IEnumerable<T> values)
        {
            return targetObject.In(EqualityComparer<T>.Default, values);
        }

        /// <summary>
        /// Содержится ли объект среди заданных
        /// </summary>
        [DebuggerStepThrough]
        public static bool In<T>(this T targetObject, IEqualityComparer<T> comparer, IEnumerable<T> values)
        {
            return values.Contains(targetObject, comparer);
        }

        /// <summary>
        /// Преобразует объект в строку формата JSON
        /// </summary>
        /// <param name="target">Объект</param>
        /// <returns>Строка в формате JSON</returns>
        [DebuggerStepThrough]
        public static string ToJson(this object target)
        {
            return JsonConvert.SerializeObject(target);
        }

        /// <summary>
        /// Преобразует объект в нулабельный
        /// </summary>
        /// <typeparam name="T">Исходный тип</typeparam>
        /// <param name="value">Исходное значение</param>
        /// <returns>Нулабельное значение</returns>
        [DebuggerStepThrough]
        public static T? AsNullable<T>(this T value)
            where T : struct
        {
            return value;
        }

        /// <summary>
        /// Преобразует объект к типу
        /// </summary>
        /// <typeparam name="T">Целевой тип</typeparam>
        /// <param name="obj">Исходный объект</param>
        /// <returns>Объект в целевом типе</returns>
        [DebuggerStepThrough]
        public static T CastToType<T>(this object obj)
        {
            try
            {
                if (obj is IConvertible)
                {
                    var targetType = typeof(T);
                    var castedResult = Convert.ChangeType(obj, targetType);
                    var typedResult = (T)castedResult;
                    return typedResult;
                }
                else
                {
                    var typedResult = (T)obj;
                    return typedResult;
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Преобразует объект к типу
        /// </summary>
        /// <param name="obj">Исходный объект</param>
        /// <param name="castToType">Тип, в который требуется преобразовать</param>
        /// <returns>Объект в целевом типе</returns>
        [DebuggerStepThrough]
        public static object CastToType(this object obj, Type castToType)
        {
            var genericMethod = CastToTypeMethod.MakeGenericMethod(castToType);
            var castedObject = genericMethod.Invoke(null, new[] { obj });
            return castedObject;
        }

        /// <summary>
        /// Возвращает не-null экземпляр
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="source">Исходный объект</param>
        /// <returns>Исходный объект, если он не null, new T() в противном случае</returns>
        [DebuggerStepThrough]
        public static T Define<T>(this T source)
            where T : class, new()
        {
            return source.ChooseOneIfNull(new T());
        }

        /// <summary>
        /// Возвращает не-null экземпляр
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="source">Исходный объект</param>
        /// <param name="candidates">Кандидаты на значение по умолчанию</param>
        /// <returns>Исходный объект, если он не null, defaultValue в противном случае</returns>
        [DebuggerStepThrough]
        public static T ChooseOneIfNull<T>(this T source, params T[] candidates)
            where T : class
        {
            return source ?? candidates.FirstOrDefault(candidate => candidate != null);
        }

        [DebuggerStepThrough]
        public static T ValueOr<T>(this T source, T orValue)
        {
            if (source.Equals(default(T)))
                return orValue;
            return source;
        }

        #endregion

        #endregion
    }
}