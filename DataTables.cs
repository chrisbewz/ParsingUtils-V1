using System.Data;
using System.IO;
using SAP.Middleware.Connector;
using System.Collections.Generic;
using System.Linq;

namespace ParsingUtils.DataTables
{
    public static class Parsing
    {
        public static DataTable ConvertRFCTable(IRfcTable SAPTable)
        {
            DataTable DotNetTable = new DataTable();

            for (int RfcTableItem = 0; RfcTableItem < SAPTable.ElementCount; RfcTableItem++)
            {
                RfcElementMetadata TableElementData = SAPTable.GetElementMetadata(RfcTableItem);
                DotNetTable.Columns.Add(TableElementData.Name);

            }

            foreach (IRfcStructure RfcTableRow in SAPTable)
            {
                DataRow Tablerow = DotNetTable.NewRow();

                for (int RfcTableItem = 0; RfcTableItem < SAPTable.ElementCount; RfcTableItem++)
                {
                    RfcElementMetadata TableElementData = SAPTable.GetElementMetadata(RfcTableItem);
                    if (TableElementData.DataType == RfcDataType.BCD && TableElementData.Name == "ABC")
                    {
                        Tablerow[RfcTableItem] = RfcTableRow.GetInt(TableElementData.Name);
                    }
                    else
                    {
                        Tablerow[RfcTableItem] = RfcTableRow.GetString(TableElementData.Name);

                    }

                }
                DotNetTable.Rows.Add(Tablerow);
            }
            return DotNetTable;
        }
        
        public static List<Dictionary<string, object>> DataTableToDictionaries(DataTable dt)
        {
            var dictionaries = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> dictionary = Enumerable.Range(0, dt.Columns.Count).ToDictionary(i => dt.Columns[i].ColumnName, i => row.ItemArray[i]);
                dictionaries.Add(dictionary);
            }

            return dictionaries;
        }

        public static DataTable RemoveDuplicatesRecords(DataTable dt)
        {
            //Returns just 5 unique rows
            var UniqueRows = dt.AsEnumerable().Distinct(DataRowComparer.Default);
            DataTable dt2 = UniqueRows.CopyToDataTable();
            return dt2;
        }
    }
}