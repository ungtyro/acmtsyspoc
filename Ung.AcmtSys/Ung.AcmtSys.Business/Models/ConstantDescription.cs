using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ung.AcmtSys.Business.Models
{

    /// <summary>
    /// Provides a description for an enumerated type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ConstantDescription : Attribute
    {
        /// <summary>
        /// Gets the description stored in this attribute.
        /// </summary>
        /// <value>The description stored in the attribute.</value>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the
        /// </summary>
        /// <param name="description">The description to store in this attribute.
        /// </param>
        public ConstantDescription(string description)
        {
            Description = description;
        }
    }
}
