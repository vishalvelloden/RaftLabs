namespace RaftLabs.Application.Queries
{
    /// <summary>
    /// The get paged request base.
    /// </summary>
    public class GetPagedRequestBase
    {
        private int _index = 0;
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public int Index
        {
            get => _index;
            set => _index = value < 0 ? 0 : value;
        }

        private int _size = 10;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public int Size
        {
            get => _size;
            set => _size = value <= 0 ? 10 : value;
        }
    }

}
