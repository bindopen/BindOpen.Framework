﻿using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Carriers;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Databases.Data.Queries;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Databases.Extensions.Carriers
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    [Serializable()]
    [XmlType("DbField", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dbField", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [BdoCarrier(
        Name = "databases$dbField",
        DatasourceKind = DatasourceKind.Database,
        Description = "Database field.",
        CreationDate = "2016-09-14"
    )]
    public class DbField : BdoCarrier
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Indicates wheteher this instance represents all the fields.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isAll")]
        public Boolean IsAll { get; set; }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "dataModule")]
        public string DataModule { get; set; }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "schema")]
        public string Schema { get; set; }

        /// <summary>
        /// Data table of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "dataTable")]
        public string DataTable { get; set; }

        /// <summary>
        /// Alias of the data table of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "dataTableAlias")]
        public string DataTableAlias { get; set; }

        /// <summary>
        /// Alias of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Size of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "size")]
        public int Size { get; set; }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "value")]
        public DataExpression Value { get; set; }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "query")]
        public DbQuery Query { get; set; }

        /// <summary>
        /// Indicates wheteher this instance is a key.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isKey")]
        public bool IsKey { get; set; }

        /// <summary>
        /// Indicates wheteher this instance is a foreign key.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isForeignKey")]
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// Indicates wheteher the name of this instance can be defined by a script.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isNameAsScript")]
        public bool IsNameAsScript { get; set; }

        /// <summary>
        /// Type of value of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "valueType")]
        public DataValueType ValueType { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        public DbField() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Get the name of this instance that is the alias if there is or the name otherwise.
        /// </summary>
        public string GetName()
        {
            string alias = Alias;
            if (!string.IsNullOrEmpty(alias))
                return alias;
            else
                return Name ?? "";
        }

        /// <summary>
        /// Gets the data expression of this instance.
        /// </summary>
        /// <returns>Returns the data expression of this instance.</returns>
        public DataExpression ToDataExpression()
        {
            string st = "$";

            if (!string.IsNullOrEmpty(DataModule))
            {
                st += "sqlDatabase('" + DataModule + "').";
            }
            if (!string.IsNullOrEmpty(Schema))
            {
                st += "sqlSchema('" + DataModule + "').";
            }
            if (!string.IsNullOrEmpty(DataTable))
            {
                st += "sqlTable('" + DataTable + "').";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                st += "sqlField('" + Name + "')";
            }

            return DataExpressionFactory.CreateScript(st);
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the expression value of this instance.
        /// </summary>
        /// <param name="expression">Data expression value of the instance.</param>
        public void SetValue(DataExpression expression)
        {
            Value = expression;
        }

        /// <summary>
        /// Sets the literal value of this instance.
        /// </summary>
        /// <param name="text">The literal value.</param>
        public void SetLiteralValue(string text)
        {
            Value = text.CreateLiteral();
        }

        /// <summary>
        /// Sets the script value of this instance.
        /// </summary>
        /// <param name="text">The script value.</param>
        public void SetScriptValue(string text)
        {
            Value = text.CreateScript();
        }

        /// <summary>
        /// Sets the specified alias.
        /// </summary>
        /// <param name="alias">The alias to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbField WithAlias(string alias)
        {
            Alias = alias;
            return this;
        }

        /// <summary>
        /// Sets the specified size.
        /// </summary>
        /// <param name="size">The size to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbField WithSize(int size)
        {
            Size = size;
            return this;
        }

        /// <summary>
        /// Indicates that this instance represents all fields.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public DbField WithAll()
        {
            IsAll = true;
            return this;
        }

        /// <summary>
        /// Indicates that this instance represents a key.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public DbField AsKey()
        {
            IsKey = true;
            return this;
        }

        /// <summary>
        /// Indicates that the name of this instance is as script.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public DbField WithNameAsScript()
        {
            IsNameAsScript = true;
            return this;
        }

        #endregion
    }
}