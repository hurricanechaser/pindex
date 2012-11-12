using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;
using System.Data;

namespace Utilities
{
    public static class SchemaHelper
    {
        static List<object> GetSchema(string connectionString, string providerName, string table)
        {
            SchemaReader reader = new SchemaReader(connectionString, providerName);
            DataTable columns = reader.Columns(table);
            List<object> list = new List<object>();
            foreach (DataColumn column in columns.Columns)
            {
                list.Add(new
                 {
                     name = column.ColumnName,
                     type = GetSenchaType(column)
                 });
            }
            return list;
        }

        public static string GetSenchaType(DataColumn column)
        {
            string type = string.Empty;
            switch (column.DataType.Name)
            {
                default:
                    type = "string";
                    break;
                case "Int32":
                    type = "int";
                    break;
                case "Boolean":
                    type = "bool";
                    break;
                case "DateTime":
                    type = "date";
                    break;
            }
            return type;
        }
    }
}




