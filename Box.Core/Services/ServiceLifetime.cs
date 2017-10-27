namespace Box.Core.Services
{
    public enum ServiceLifetime
    {
        /// <summary>
        ///     Instance is instanciated as singleton.
        /// </summary>
        Singleton,

        /// <summary>
        ///     Instance has a transient lifetime.
        /// </summary>
        Transient,

        /// <summary>
        ///     Instance is instanciated per request.
        /// </summary>
        Scoped
    }
}