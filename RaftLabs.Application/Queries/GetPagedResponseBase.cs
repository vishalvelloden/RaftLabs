namespace RaftLabs.Application.Queries
{
    /// <summary>
    /// The get paged response base.
    /// </summary>
    public class GetPagedResponseBase<T>
        where T : class
    {
        /// <summary>
        /// Gets or sets the from.
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets the pages.
        /// </summary>
        public int Pages => (int)Math.Ceiling(this.Count / (double)this.Size);

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        public List<T> Records { get; set; } = new List<T>();
    }
}
