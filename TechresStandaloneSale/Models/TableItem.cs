using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TechresStandaloneSale.Models
{
    public class TableItem
    {
        public long Id { get; set; }
        public string TableName { get; set; }
        public BitmapImage LinkImage { get; set; }
        public int TableStatus { get; set; }
        public string TableNameString
        {
            get
            {
                if (!string.IsNullOrEmpty(TableName))
                {
                    return TableName.Length > 3 ? string.Format("{0}...", TableName.Substring(0, 3).ToUpper()) : TableName.ToUpper();
                }
                else
                {
                    return "";
                }

            }
            set
            {
                TableNameString = value;
            }
        }
        public TableItem(long id, string tableName, BitmapImage linkImage, int tableStatus)
        {
            Id = id;
            TableName = tableName;
            LinkImage = linkImage;
            TableStatus = tableStatus;
        }
    }
}
