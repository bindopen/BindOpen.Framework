﻿using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Diagnostics.Events;
using System.Xml.Serialization;

namespace BindOpen.Data.Specification
{
    /// <summary>
    /// This class represents the data constraint statement.
    /// </summary>
    [XmlType("DataConstraintStatement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "constraintStatement", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class DataConstraintStatement : TDataItemSet<BdoRoutineConfiguration>, IDataConstraintStatement
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataConstraintStatement class.
        /// </summary>
        public DataConstraintStatement()
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified constraint.
        /// </summary>
        /// <param name="routine">The constraint to add.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public void AddConstraint(IBdoRoutineConfiguration routine)
        {
            if (routine != null) Add(routine as BdoRoutineConfiguration);
        }

        /// <summary>
        /// Adds the specified constraint.
        /// </summary>
        /// <param name="name">The name of constraint to create.</param>
        /// <param name="definitionUniqueId">The definition unique ID to create.</param>
        /// <param name="parameterDetail">The parameter detail to consider.</param>
        /// <param name="outputEventSet">The output event set to consider.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public IBdoRoutineConfiguration AddConstraint(
            string name,
            string definitionUniqueId,
            IDataElementSet parameterDetail = null,
            ITDataItemSet<BdoConditionalEvent> outputEventSet = null)
        {
            IBdoRoutineConfiguration routine = null; // new RoutineConfiguration(null, definitionUniqueId, commandSet, outputEventSet, parameterDetail?.Elements?.ToArray());
            Add(routine as BdoRoutineConfiguration);

            return routine;
        }

        /// <summary>
        /// Sets the constraint parameter value.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="definitionUniqueId">The name of the definition to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <param name="dataValueType">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public IDataElement AddConstraintParameter(
            string constraintName,
            string definitionUniqueId = null,
            string parameterName = null,
            DataValueTypes dataValueType = DataValueTypes.Any)
        {
            IDataElement dataElement = null;

            IBdoRoutineConfiguration routine = GetConstraint(constraintName);
            if ((routine == null) || (!routine.DefinitionUniqueId.KeyEquals(definitionUniqueId)))
                routine = AddConstraint(constraintName, definitionUniqueId);

            if (routine != null)
            {
                if (parameterName == null && routine.Count > 0)
                    dataElement = routine[0];
                else
                    dataElement = routine[parameterName];
                if (dataElement == null)
                {
                    routine.Add(dataElement = ElementFactory.CreateScalar(
                       parameterName,
                       dataValueType == DataValueTypes.Any ? dataValueType.GetValueType() : dataValueType));
                }
            }

            return dataElement;
        }

        /// <summary>
        /// Sets the constraint parameter value.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="definitionUniqueId">The name of the definition to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="dataValueType">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public bool SetConstraintParameterValue(
            string constraintName,
            string definitionUniqueId = null,
            string parameterName = null,
            object value = null,
            DataValueTypes dataValueType = DataValueTypes.Any)
        {
            IBdoRoutineConfiguration routine = GetConstraint(constraintName);
            if (routine?.DefinitionUniqueId.KeyEquals(definitionUniqueId) != true)
                routine = AddConstraint(constraintName, definitionUniqueId);

            IDataElement dataElement;
            if (parameterName == null && routine.Count > 0)
                dataElement = routine[0];
            else
                dataElement = routine[parameterName];
            if (dataElement == null)
            {
                routine.Add(
                    ElementFactory.CreateScalar(
                        parameterName,
                        dataValueType == DataValueTypes.Any ? value.GetValueType() : dataValueType,
                        value));
            }
            else
            {
                dataElement.WithItems(value);
            }

            return true;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the constraint with the specified name.
        /// </summary>
        /// <param name="name">The name of the item to return.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public IBdoRoutineConfiguration GetConstraint(string name)
        {
            return Get(name) as BdoRoutineConfiguration;
        }

        /// <summary>
        /// Returns the constraint parameter.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public IDataElement GetConstraintParameter(
            string constraintName,
            string parameterName = null)
        {
            IBdoRoutineConfiguration routine = GetConstraint(constraintName);
            return routine?[parameterName];
        }

        /// <summary>
        /// Returns the constraint parameter value.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public object GetConstraintParameterValue(
            string constraintName,
            string parameterName = null)
        {
            IBdoRoutineConfiguration routine = GetConstraint(constraintName);
            return routine?.Get(parameterName);
        }

        #endregion

        // --------------------------------------------------
        // CHECKING
        // --------------------------------------------------

        #region Checking

        /// <summary>
        /// Check value.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="isDeepCheck">Indicates whether other rules than allowed and forbidden values are checked.</param>
        /// <returns>The log of check log.</returns>
        public IBdoLog CheckItem(
            object item,
            IDataElement dataElement,
            bool isDeepCheck = false)
        {
            var log = new BdoLog();

            return log;
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            DataConstraintStatement dataConstraintStatement = base.Clone(areas) as DataConstraintStatement;
            return dataConstraintStatement;
        }

        #endregion
    }
}
