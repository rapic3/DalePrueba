﻿using System;
using System.Collections;

namespace Dale.Utils.Enum
{
    /// <summary>
    /// Clase que se encarga de recuperar el decorado StrigValue de los enumeradores
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class StringValueAttribute : Attribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la <see cref="StringValueAttribute" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public StringValueAttribute(string value) { Value = value; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; }
    }

    /// <summary>
    /// Clase para agregar los métodos de extensión de los enumeradores
    /// </summary>
    public static class ExtendEnum
    {
        /// <summary>
        /// Método que recupera la cadena asignada en el decorado del enumerador
        /// </summary>
        /// <param name="value">Enumerador al cual se recuperará el avalor</param>
        /// <returns>System.String.</returns>
        public static string ToStringAttribute(this System.Enum value)
        {
            var stringValues = new Hashtable();

            string output = null;
            var type = value.GetType();

            //Comprueba si ya existe la búsqueda en caché
            if (stringValues.ContainsKey(value))
            {
                var stringValueAttribute = (StringValueAttribute)stringValues[value];
                if (stringValueAttribute != null)
                    output = stringValueAttribute.Value;
            }
            else
            {
                //Buscar el ToStringAttribute en los atributos personalizados
                System.Reflection.FieldInfo fi = type.GetField(value.ToString());
                var attrs = (StringValueAttribute[])fi.GetCustomAttributes(typeof(StringValueAttribute), false);
                if (attrs.Length > 0)
                {
                    stringValues.Add(value, attrs[0]);
                    output = attrs[0].Value;
                }
            }
            return output;
        }
    }
}
