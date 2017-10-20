using System;

namespace Box.Api.Services
{
    public enum Lifetime
    {
        /// <summary>
        /// Instance is instanciated as singleton.
        /// </summary>
        Singleton,

        /// <summary>
        /// Instance has a transient lifetime.
        /// </summary>
        Transient,

        /// <summary>
        /// Instance is instanciated per request.
        /// </summary>
        Scoped
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool HasType => ExportedType != null;

        /// <summary>
        /// Specified exported type.
        /// </summary>
        public Type ExportedType { get; }

        /// <summary>
        /// 
        /// </summary>
        public Lifetime Lifetime { get; }

        public ServiceAttribute() : this(null, Lifetime.Singleton)
        {
        }

        public ServiceAttribute(Lifetime lifetime) : this(null, lifetime)
        {
        }

        public ServiceAttribute(Type exportedType) : this(exportedType, Lifetime.Singleton)
        {
        }

        public ServiceAttribute(Type exportedType, Lifetime lifetime)
        {
            ExportedType = exportedType;
            Lifetime = lifetime;
        }
    }
}