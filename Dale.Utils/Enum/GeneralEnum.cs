using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dale.Utils.Enum
{
    public class GeneralEnum
    {
        public enum EnumTypeReturn
        {
            /// <summary>
            /// Cuando sucede un error y se bloquea el flujo normal del programa.
            /// </summary>
            Error,

            /// <summary>
            /// Cuando el proceso es exitoso.
            /// </summary>
            OK,

            /// <summary>
            /// Cuando se quiere guardar una Advertencia del proceso, pero no se detiene el flujo normal del programa.
            /// </summary>
            Warning
        }
    }
}
