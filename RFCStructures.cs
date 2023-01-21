using System.Data;
using SAP.Middleware.Connector;

namespace ParsingUtils.RFC
{
    public static class Structures
    {
        public static DataTable ConvertStructure(IRfcStructure myrefcTable) 

        {

            DataTable rowTable = new DataTable();

            for (int i = 0; i <= myrefcTable.ElementCount - 1; i++)

            {

                rowTable.Columns.Add(myrefcTable.GetElementMetadata(i).Name);

            }

            DataRow row = rowTable.NewRow();

            for (int j = 0; j <= myrefcTable.ElementCount - 1; j++)

            {

                row[j] = myrefcTable.GetValue(j);

            }

            rowTable.Rows.Add(row);

            return rowTable;

        }
    }
}