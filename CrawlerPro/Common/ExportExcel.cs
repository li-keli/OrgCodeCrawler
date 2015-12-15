using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace CrawlerPro.Common
{
    /// <summary>
    /// Export扩展 - DataTable、泛型导出Excel
    /// </summary>
    public static class ExportExcel
    {
        /// <summary>
        /// 导出Excel(03-07) 泛型集合操作
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="lists">数据源</param>
        /// <param name="fileName">下载文件名</param>
        /// <returns></returns>
        public static byte[] ListToExcel<T>(this IList<T> lists, string fileName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                //创建一个名称为Payment的工作表
                ISheet paymentSheet = workbook.CreateSheet("Payment");
                //头部标题
                IRow paymentHeaderRow = paymentSheet.CreateRow(0);

                PropertyInfo[] propertys = lists[0].GetType().GetProperties();
                //循环添加标题
                for (int i = 0; i < propertys.Count(); i++)
                    paymentHeaderRow.CreateCell(i).SetCellValue(propertys[i].Name);
                // 内容
                int paymentRowIndex = 1;
                foreach (var each in lists)
                {
                    IRow newRow = paymentSheet.CreateRow(paymentRowIndex);
                    //循环添加列的对应内容
                    for (int i = 0; i < propertys.Count(); i++)
                    {
                        var obj = propertys[i].GetValue(each, null);
                        newRow.CreateCell(i).SetCellValue((obj ?? "").ToString());
                    }
                    paymentRowIndex++;
                }

                //列宽自适应，只对英文和数字有效
                for (int i = 0; i <= lists.Count; i++)
                    paymentSheet.AutoSizeColumn(i);
                //将表内容写入流 等待下一步操作
                workbook.Write(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 导出Excel(03-07) DataTable操作
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="fileName">下载文件名</param>
        /// <returns></returns>
        public static byte[] ListToExcel(this DataTable dt, string fileName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                //创建一个名称为Payment的工作表
                ISheet paymentSheet = workbook.CreateSheet("Payment");
                //数据源
                DataTable tbPayment = dt;
                //头部标题
                IRow paymentHeaderRow = paymentSheet.CreateRow(0);
                //循环添加标题
                foreach (DataColumn column in tbPayment.Columns)
                    paymentHeaderRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                // 内容
                int paymentRowIndex = 1;
                foreach (DataRow row in tbPayment.Rows)
                {
                    IRow newRow = paymentSheet.CreateRow(paymentRowIndex);
                    //循环添加列的对应内容
                    foreach (DataColumn column in tbPayment.Columns)
                        newRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    paymentRowIndex++;
                }

                //列宽自适应，只对英文和数字有效
                for (int i = 0; i <= dt.Rows.Count; i++)
                    paymentSheet.AutoSizeColumn(i);
                //获取当前列的宽度，然后对比本列的长度，取最大值
                //for (int columnNum = 0; columnNum <= dt.Columns.Count; columnNum++)
                //{
                //    int columnWidth = paymentSheet.GetColumnWidth(columnNum) / 256;
                //    for (int rowNum = 1; rowNum <= paymentSheet.LastRowNum; rowNum++)
                //    {
                //        //当前行未被使用过
                //        var currentRow = paymentSheet.GetRow(rowNum) ?? paymentSheet.CreateRow(rowNum);
                //        if (currentRow.GetCell(columnNum) != null)
                //        {
                //            ICell currentCell = currentRow.GetCell(columnNum);
                //            int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                //            if (columnWidth < length)
                //                columnWidth = length;
                //        }
                //    }
                //    paymentSheet.SetColumnWidth(columnNum, columnWidth * 256);
                //}
                //将表内容写入流 等待其他操作
                workbook.Write(ms);
                return ms.ToArray();
            }
        }
    }
}
