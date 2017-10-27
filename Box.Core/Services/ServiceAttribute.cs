using System;

namespace Box.Core.Services
{
    [AttributeUsage( AttributeTargets.Class )]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute() : this( null, ServiceLifetime.Singleton )
        {
        }

        public ServiceAttribute( ServiceLifetime lifetime ) : this( null, lifetime )
        {
        }

        public ServiceAttribute( Type exportedType ) : this( exportedType, ServiceLifetime.Singleton )
        {
        }

        public ServiceAttribute( Type exportedType, ServiceLifetime lifetime )
        {
            ExportedType = exportedType;
            Lifetime = lifetime;
        }

        /// <summary>
        /// </summary>
        public bool HasType => ExportedType != null;

        /// <summary>
        ///     Specified exported type.
        /// </summary>
        public Type ExportedType { get; }

        /// <summary>
        /// </summary>
        public ServiceLifetime Lifetime { get; }
    }
}