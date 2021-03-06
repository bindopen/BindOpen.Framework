﻿using BindOpen.Data.Common;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class ElementFactory
    {
        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="dataValueType">The value type to consider.</param>
        public static ScalarElement CreateScalar(DataValueTypes dataValueType)
        {
            return CreateScalar(null, null, dataValueType, null);
        }

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public static ScalarElement CreateScalar(string name)
        {
            return CreateScalar(name, null, DataValueTypes.Any, null);
        }

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static ScalarElement CreateScalar(
            DataValueTypes valueType,
            params object[] items)
        {
            return CreateScalar(null, null, valueType, null, items);
        }

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static ScalarElement CreateScalar(
            string name,
            params object[] items)
        {
            return CreateScalar(name, null as string, DataValueTypes.Any, null, items);
        }

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static ScalarElement CreateScalar(
            string name,
            DataValueTypes valueType,
            params object[] items)
        {
            return CreateScalar(name, null as string, valueType, null, items);
        }

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static ScalarElement CreateScalar(
            string name,
            string id,
            DataValueTypes valueType,
            IScalarElementSpec specification,
            params object[] items)
        {
            if (valueType == DataValueTypes.Any)
            {
                if (items == null)
                {
                    valueType = DataValueTypes.None;
                }
                else
                {
                    valueType = items.GetValueType();
                }
            }

            ScalarElement element = new ScalarElement(name, id)
            {
                ValueType = valueType,
                Specification = specification as ScalarElementSpec
            };

            if (items != null)
            {
                element.Add(items);
            }

            return element;
        }
    }
}
