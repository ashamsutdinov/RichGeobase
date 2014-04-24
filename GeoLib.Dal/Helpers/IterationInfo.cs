namespace GeoLib.Dal.Helpers
{
    /// <summary>
    /// Информация об итерации с данными о следующем и предыдущем элементе
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IterationInfo<T> : IterationInfo
    {
        #region Constructors

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="index">номер итерации</param>
        /// <param name="isFirst">флаг: первая итерация</param>
        /// <param name="isLast">флаг: последняя итерация</param>
        /// <param name="previous">предыдущий элемент</param>
        /// <param name="next">следующий элемент</param>
        internal IterationInfo(int index, bool isFirst, bool isLast, T previous, T next)
            : base(index, isFirst, isLast)
        {
            Previous = previous;
            Next = next;
        }

        #endregion

        #region Properties

        #region Public

        /// <summary>
        /// Предыдущий элемент
        /// </summary>
        public T Previous { get; private set; }

        /// <summary>
        /// Следующий элемент
        /// </summary>
        public T Next { get; private set; }

        #endregion

        #endregion
    }

    public class IterationInfo
    {
        #region Constructors

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="index">номер итерации</param>
        /// <param name="isFirst">флаг: это первая итерация</param>
        /// <param name="isLast">флаг: это последняя итерация</param>
        internal IterationInfo(int index, bool isFirst, bool isLast)
        {
            Index = index;
            IsFirst = isFirst;
            IsLast = isLast;
        }

        #endregion

        #region Properties

        #region Public

        /// <summary>
        /// Номер итерации
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Флаг: итерация является первой
        /// </summary>
        public bool IsFirst { get; private set; }

        /// <summary>
        /// Флаг: итерация является последней
        /// </summary>
        public bool IsLast { get; private set; }

        #endregion

        #endregion
    }
}