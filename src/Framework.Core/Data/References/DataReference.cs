﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Definition.Handlers;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.References
{

    /// <summary>
    /// This class represents a data reference.
    /// </summary>
    [Serializable()]
    [XmlType("DataReference", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "data.reference", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataReference : DataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private HandlerDefinition _HandlerDefinition = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The dtaa handler unique name of this instance.
        /// </summary>
        [XmlAttribute("handler")]
        public String DataHandlerUniqueName { get; set; } = null;

        /// <summary>
        /// Source element of this instance.
        /// </summary>
        [XmlElement("carrier", typeof(CarrierElement))]
        [XmlElement("source", typeof(SourceElement))]
        public DataElement SourceElement { get; set; } = null;

        /// <summary>
        /// The path detail of this instance.
        /// </summary>
        [XmlElement("path")]
        public DataElementSet PathDetail { get; set; } = new DataElementSet();

        /// <summary>
        /// Specification of the PathDetail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean PathDetailSpecified => PathDetail != null && (this.PathDetail.ElementsSpecified || this.PathDetail.DescriptionSpecified);

        /// <summary>
        /// Source item of this instance.
        /// </summary>
        [XmlIgnore()]
        public Object Source => this.SourceElement?.FirstItem;

        /// <summary>
        /// Target item of this instance.
        /// </summary>
        [XmlIgnore()]
        public Object TargetItem { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        public DataReference()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        /// <param name="dataHandlerUniqueName">The handler unique name to consider.</param>
        /// <param name="sourceElement">The source element to consider.</param>
        /// <param name="dynamicObject">The dynamic object of this instance.</param>
        public DataReference(String dataHandlerUniqueName, DataElement sourceElement, DynamicObject dynamicObject) : this()
        {
            this.DataHandlerUniqueName = dataHandlerUniqueName;
            this.SourceElement = sourceElement;
            this.PathDetail.Update(DataElementSet.Create(dynamicObject));
        }

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        /// <param name="dataHandlerUniqueName">The handler unique name to consider.</param>
        /// <param name="sourceElement">The source element to consider.</param>
        /// <param name="pathDetail">The path detail to consider.</param>
        public DataReference(String dataHandlerUniqueName, DataElement sourceElement = null, DataElementSet pathDetail = null) : this()
        {
            this.DataHandlerUniqueName = dataHandlerUniqueName;
            this.SourceElement = sourceElement;
            this.PathDetail = pathDetail;
        }

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        /// <param name="dataHandlerUniqueName">The handler unique name to consider.</param>
        /// <param name="sourceItem">The source item to consider.</param>
        /// <param name="dynamicObject">The dynamic object of this instance.</param>
        public DataReference(String dataHandlerUniqueName, DataItem sourceItem, DynamicObject dynamicObject) : this()
        {
            this.DataHandlerUniqueName = dataHandlerUniqueName;
            this.SourceElement = DataElement.Create(sourceItem);
            this.PathDetail.Update(DataElementSet.Create(dynamicObject));
        }

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        /// <param name="dataHandlerUniqueName">The handler unique name to consider.</param>
        /// <param name="sourceItem">The source item to consider.</param>
        /// <param name="pathDetail">The path detail to consider.</param>
        public DataReference(String dataHandlerUniqueName, DataItem sourceItem = null, DataElementSet pathDetail = null) : this()
        {
            this.DataHandlerUniqueName = dataHandlerUniqueName;
            this.SourceElement = DataElement.Create(sourceItem);
            this.PathDetail = pathDetail;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        // Source ------------------------------------

        /// <summary>
        /// Gets the primary source of this instance.
        /// </summary>
        /// <returns>Returns the initial source of this instance.</returns>
        public StoredDataItem GetPrimarySource() => SourceElement != null ? this.GetPrimarySource() : this.SourceElement;

        /// <summary>
        /// Gets the value type of the primary source of this instance.
        /// </summary>
        /// <returns>Returns the value type of the primary source of this instance.</returns>
        public DataValueType GetPrimarySourceValueType()
        {
            StoredDataItem storedDataItem = GetPrimarySource();
            if (storedDataItem != null)
                return storedDataItem.GetValueType();

            return DataValueType.None;
        }

        /// <summary>
        /// Gets the initial data source of this instance.
        /// </summary>
        /// <returns>Returns the initial data source of this instance.</returns>
        public DataSource GetDataSource()
        {
            return (this.SourceElement != null ? this.GetPrimarySource() : this.SourceElement) as DataSource;
        }

        /// <summary>
        /// Gets the initial data source kind of this instance.
        /// </summary>
        /// <returns>Returns the initial data source kind of this instance.</returns>
        public DataSourceKind GetDataSourceKind()
        {
            DataSource dataSource = this.GetDataSource();
            if (dataSource != null)
                return dataSource.Kind;

            return DataSourceKind.None;
        }

        //private DataHandler GetHandler(AppScope appScope)
        //{
        //    if (this._DataHandler != null)
        //        return this._DataHandler;
        //    else if ((appScope != null) && (appScope.AppExtension != null))
        //        return appScope.AppExtension.CreateItemInstance(AppExtensionItemKind.Handler, this._DataHandlerUniqueName) as DataHandler;

        //    return null;
        //}

        // Items ------------------------------------

        /// <summary>
        /// Gets the items from the source of this instance and update the target items.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the retrieved items.</returns>
        public Object Get(
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            Object item = null;

            log = (log ?? new Log());

            if (!log.Append(appScope.Check(true, true)).HasErrorsOrExceptions())
                if ((this._HandlerDefinition = appScope.AppExtension.GetItemDefinitionWithUniqueId<HandlerDefinition>(this.DataHandlerUniqueName)) == null)
                    log.AddError(title: "Data handler definition '" + this.DataHandlerUniqueName + "' not found");
                else if (this._HandlerDefinition.RuntimeFunction_Get == null)
                    log.AddError(title: "Get function missing in handler definition '" + this.DataHandlerUniqueName + "'");
                else
                {
                    log.AddEvents(this.Check<DataReference>());

                    if (!log.HasErrorsOrExceptions())
                    {
                        item = this.TargetItem = this._HandlerDefinition.RuntimeFunction_Get(
                           this.SourceElement, this.PathDetail, appScope, scriptVariableSet, log).GetObjectAtIndex(0);
                    }
                }

            return item;
        }

        /// <summary>
        /// Posts the items to the source of this instance.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the posted items.</returns>
        public List<Object> Post(
            List<Object> items,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            List<Object> dataItems = new List<Object>();

            log = (log ?? new Log());

            if (!log.Append(appScope.Check(true, true)).HasErrorsOrExceptions())
            {
                if ((this._HandlerDefinition = appScope.AppExtension.GetItemDefinitionWithUniqueId<HandlerDefinition>(this.DataHandlerUniqueName)) == null)
                    log.AddError(title: "Data reference definition not found");
                else if (this._HandlerDefinition.RuntimeFunction_Post == null)
                    log.AddError(title: "Post function missing in handler definition");
                else if (items != null)
                {
                    log.AddEvents(this.Check<DataReference>());

                    DataElement sourceElement = null;
                    if (!log.HasErrorsOrExceptions())
                    {
                        foreach (Object aItem in items)
                        {
                            dataItems.AddRange(this._HandlerDefinition.RuntimeFunction_Post(
                                aItem, ref sourceElement, appScope, scriptVariableSet, log));
                        }
                    }
                }
            }

            return dataItems;
        }

        #endregion

        // --------------------------------------------------
        // CHECK, UPDATE, REPAIR
        // --------------------------------------------------

        #region Check_Update_Repair


        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override Object Clone()
        {
            DataReference dataReference = base.Clone() as DataReference;
            if (this.SourceElement != null)
                dataReference.SourceElement = this.SourceElement.Clone() as DataElement;
            if (this.PathDetail != null)
                dataReference.PathDetail = this.PathDetail.Clone() as DataElementSet;
            dataReference.TargetItem = this.TargetItem;
            return dataReference;
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(Log log = null)
        {
            if (this.PathDetail != null)
                foreach (DataElement dataElement in this.PathDetail.Elements)
                    dataElement.UpdateStorageInfo(log);
            if (this.SourceElement != null)
                this.SourceElement.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null,  Log log = null)
        {
            log = (log ?? new Log());

            if (this.PathDetail != null)
                foreach (DataElement dataElement in this.PathDetail.Elements)
                    dataElement.UpdateRuntimeInfo(appScope, log);
            if (this.SourceElement != null)
                this.SourceElement.UpdateRuntimeInfo(appScope, log);
        }

        #endregion
    }
}