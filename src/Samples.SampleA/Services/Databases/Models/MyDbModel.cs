﻿using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Models;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Carriers;
using BindOpen.System.Diagnostics;

namespace Samples.SampleA.Services.Databases
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public partial class MyDbModel : BdoDbModel, IMyDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public override void OnCreating(IBdoDbModelBuilder builder)
        {
            builder
                .AddTable("Employee", DbFluent.Table(nameof(DbEmployee).Substring(2), "Mdm"))
                .AddTable<DbCountry>(nameof(DbCountry).Substring(2))
                .AddTable("RegionalDirectorate", DbFluent.Table(nameof(DbRegionalDirectorate).Substring(2), "Mdm"));

            builder
                .AddJoinCondition("Employee_RegionalDirectorate",
                    DbFluent.JoinCondition(
                        DbFluent.Field<DbEmployee, int>(p => p.EmployeeId),
                        DbFluent.Field<DbRegionalDirectorate, int>(p => p.RegionalDirectorateId)));

            builder
                .AddTuple("Fields_SelectEmployee",
                    new DbField[]
                    {
                        DbFluent.FieldAsAll(Table("Employee")),
                        DbFluent.Field(nameof(DbRegionalDirectorate.RegionalDirectorateId), Table("RegionalDirectorate")),
                        DbFluent.Field(nameof(DbRegionalDirectorate.Code), Table("RegionalDirectorate"))
                    });

            builder
                .AddQuery(
                    new StoredDbQuery(
                    DbFluent.SelectBasic(Table("Employee"))
                    .WithFroms(DbFluent.From(Table("Employee"))
                        .WithJoins(DbFluent.Join(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                            .WithCondition(JoinCondition("Employee_RegionalDirectorate"))))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .AddIdField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), p.UseParameter("code")))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="q"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageToken"></param>
        /// <returns></returns>
        internal IDbQuery ListEmployees(string q, string orderBy, int? pageSize = null, int? pageToken = null,
            IBdoLog log = null, string filterQuery = null, string orderByQuery = null)
            => DbFluent.SelectAdvanced("GetMyTables", DbFluent.Table())
                .AsDistinct()
                .WithFields(
                    DbFluent.FieldAsAll(DbFluent.Table(DbFluent.Table("table"))),
                    DbFluent.Field("Field1", DbFluent.Table(DbFluent.Table("table"))),
                    DbFluent.Field("Field2", DbFluent.Table(DbFluent.Table("table"))))
                .Froms(
                    DbFluent.From(DbFluent.Table(nameof(DbCountry).Substring(2), "schema1").WithAlias(DbFluent.Table("table")))
                        .WithJoins(
                            DbFluent.Join(DbQueryJoinKind.Left, DbFluent.Table("DbTable1".Substring(2), "schema2").WithAlias(DbFluent.Table("table1")))
                                .WithCondition(
                                    DbFluent.And(
                                        DbFluent.JoinCondition(
                                            DbFluent.Field("table1key", DbFluent.Table("table1")),
                                            DbFluent.Field(nameof(DbCountry.MyTableId), DbFluent.Table("table"))),
                                        DbFluent.JoinCondition(
                                            DbFluent.Field("table2key", DbFluent.Table("table2")),
                                            DbFluent.Field(nameof(DbCountry.MyTableId), DbFluent.Table("table"))))))
                        .WithJoins(
                            DbFluent.Join(DbQueryJoinKind.Left, DbFluent.Table("DbTable1".Substring(2), "schema2").WithAlias(DbFluent.Table("table2")))
                                .WithCondition(
                                    DbFluent.JoinCondition(
                                        DbFluent.Field("table1key", DbFluent.Table("table2")),
                                        DbFluent.Field("Field1", DbFluent.Table("table")))))
                )
                .Filter(
                    filterQuery,
                    log,
                    new ApiScriptFilteringDefinition(
                        new ApiScriptClause("startCreationDate", DbFluent.Field("CreationDate", DbFluent.Table("table"))),
                        new ApiScriptClause("endCreationDate", DbFluent.Field("CreationDate", DbFluent.Table("table"))),
                        new ApiScriptClause("name", DbFluent.Field("Name", DbFluent.Table("table")), DataOperator.Equal)
                    //new ApiScriptClause(DbFluent.Table("table"), null, DataOperator.,
                    //    new ApiScriptFilteringDefinition(
                    //        new ApiScriptClause("CreationDate", DbFieldFactory.Create("CreationDate", "MyTable", nameof(DbSchemas.Iam), null), DataOperator.GreaterOrEqual)))
                    ))
                .Sort(
                    orderByQuery,
                    log,
                    new ApiScriptSortingDefinition(
                        new ApiScriptField("CreationDate", DbFluent.Field("CreationDate", DbFluent.Table("table")))
                        , new ApiScriptField("Id", DbFluent.Field("Name", DbFluent.Table("table")))
                        , new ApiScriptField("LastModificationDate", DbFluent.Field("LastModificationDate", DbFluent.Table("table")))));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery GetEmployeeWithCode(string code)
        {
            return this.UseQuery("GetEmployeeWithCode", p =>
                DbFluent.SelectBasic(Table("Employee"))
                    .WithFroms(
                        DbFluent.From(Table("Employee"))
                        .WithJoins(
                            DbFluent.Join(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                                .WithCondition(JoinCondition("Employee_RegionalDirectorate"))))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text))))
                .WithParameters(
                    ElementFactory.Create("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery UpsertEmployee(EmployeeDto employee)
        {
            return DbFluent.Upsert(Table("Employee"))
                .Select(DbFluent.Join(
                        DbFluent.SelectBasic(null)
                        .WithFields(q => new[]
                        {
                            DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text))
                        }).WithAlias("T"))
                    .WithCondition(DbFluent.JoinCondition(
                        DbFluent.Field(nameof(DbEmployee.Code), Table("Employee")),
                        DbFluent.Field(nameof(DbEmployee.Code), Table("T")))))
                .Matching(
                    DbFluent.UpdateBasic(Table("Employee"))
                    .WithFields(q => new[]
                    {
                        DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.ContactEmail), q.UseParameter("contactEmail", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), q.UseParameter("fisrtName", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), q.UseParameter("fisrtName", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.LastName), q.UseParameter("lastName", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.StaffNumber), q.UseParameter("staffNumber", DataValueType.Text))
                    }))
                .NotMatching(
                    DbFluent.InsertBasic(Table("Employee"), true)
                    .WithFroms(DbFluent.From(Table("Employee"))
                        .WithJoins(
                            DbFluent.Join(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                                .WithCondition(JoinCondition("Employee_RegionalDirectorate")))))
                .WithParameters(
                    ElementFactory.Create("code", employee.Code),
                    ElementFactory.Create("contactEmail", employee.ContactEmail),
                    ElementFactory.Create("fisrtName", employee.FisrtName),
                    ElementFactory.Create("lastName", employee.LastName),
                    ElementFactory.Create("staffNumber", employee.StaffNumber));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery InsertEmployee(EmployeeDto employee)
        {
            return DbFluent.InsertBasic(Table("Employee"))
                .WithFields(q => new[]
                {
                    DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.ContactEmail), q.UseParameter("contactEmail", DataValueType.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), q.UseParameter("fisrtName", DataValueType.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.LastName), q.UseParameter("lastName", DataValueType.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.StaffNumber), q.UseParameter("staffNumber", DataValueType.Text))
                })
                .WithParameters(
                    ElementFactory.Create("code", employee.Code),
                    ElementFactory.Create("contactEmail", employee.ContactEmail),
                    ElementFactory.Create("fisrtName", employee.FisrtName),
                    ElementFactory.Create("lastName", employee.LastName),
                    ElementFactory.Create("staffNumber", employee.StaffNumber));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isPartialUpdate"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery UpdateEmployee(string code, bool isPartialUpdate, EmployeeDto employee)
        {
            return this.UseQuery("UpdateEmployee",
                p =>
                {
                    var query = DbFluent.UpdateBasic(Table("Employee"))
                        .WithFroms(
                            DbFluent.From()
                            .WithJoins(
                                DbFluent.Join(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                                    .WithCondition(JoinCondition("Employee_RegionalDirectorate"))));

                    query.AddField(
                        !isPartialUpdate || employee?.Code?.Length > 0,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)));

                    query.AddField(
                        !isPartialUpdate || employee?.ContactEmail?.Length > 0,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.ContactEmail), q.UseParameter("contactEmail", DataValueType.Text)));

                    query.AddField(
                        !isPartialUpdate || employee?.FisrtName?.Length > 0,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), q.UseParameter("fisrtName", DataValueType.Text)));

                    query.AddField(
                        !isPartialUpdate || employee?.LastName?.Length > 0,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.LastName), q.UseParameter("lastName", DataValueType.Text)));

                    query.AddField(
                        !isPartialUpdate || employee?.StaffNumber?.Length > 0,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.StaffNumber), q.UseParameter("staffNumber", DataValueType.Text)));

                    return query;
                })
                .WithParameters(
                    ElementFactory.Create("code", employee.Code),
                    ElementFactory.Create("contactEmail", employee.ContactEmail),
                    ElementFactory.Create("fisrtName", employee.FisrtName),
                    ElementFactory.Create("lastName", employee.LastName),
                    ElementFactory.Create("staffNumber", employee.StaffNumber));
        }

        /// <summary>
        /// Delete the specified  employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee(string code)
        {
            var query = DbFluent.DeleteBasic(Table("Employee"))
                .AddIdField(DbFluent.Field(code));

            return query;
        }
    }
}
