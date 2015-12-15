using CrawlerPro.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CrawlerPro
{
    /// <summary>
    /// 任务池
    /// </summary>
    public class TaskList
    {
        private readonly List<string> _listNum = new List<string>(); //初始化任务数
        private readonly List<string> _lists = new List<string>(); //任务列表
        private int _listPosition = 0; //位置
        /// <summary>
        /// 在执行线程数
        /// </summary>
        public int Threads { set; get; }

        /// <summary>
        /// 线程完成，减少一个线程数
        /// </summary>
        public void StopThead()
        {
            if (Threads > 0)
                Threads--;
        }

        /// <summary>
        /// 初始化任务池
        /// </summary>
        /// <param name="fileUrl">任务列表文件路径</param>
        public TaskList(string fileUrl)
        {
            try
            {
                _lists = File.ReadAllLines(@fileUrl, Encoding.Default).ToList(); _listNum = _lists;
            }
            catch (Exception e)
            {
                Console.WriteLine("任务列表初始化错误");
            }
        }

        /// <summary>
        /// 获取初始化的任务总数
        /// </summary>
        /// <returns></returns>
        public int GetListNum()
        {
            return _listNum.Count();
        }

        /// <summary>
        /// 获取下一条任务
        /// </summary>
        /// <returns></returns>
        public string GetNext()
        {
            string result = null;
            if (_listPosition < _lists.Count)
                result = _lists[_listPosition]; _listPosition++;
            return result;
        }

        /// <summary>
        /// 获取剩余任务数目
        /// </summary>
        /// <returns></returns>
        public int GetSerplusNum()
        {
            return _lists.Count - _listPosition;
        }

        /// <summary>
        /// 获取剩余任务列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetSurplusTask()
        {
            List<string> resultList = new List<string>();
            for (int i = _listPosition; i < _lists.Count; i++)
            {
                resultList.Add(_lists[i]);
            }
            return resultList;
        }

        /// <summary>
        /// 输出剩余任务列表(文件)
        /// </summary>
        /// <returns></returns>
        public void GetSurplusTask(string outFileUrl)
        {
            try
            {
                List<string> resultList = new List<string>();
                for (int i = _listPosition; i < _lists.Count; i++)
                {
                    resultList.Add(_lists[i]);
                }
                File.WriteAllLines(outFileUrl, resultList, Encoding.UTF8);
            }
            catch (Exception e)
            {
                throw new Exception("剩余任务列表输出错误。");
            }
        }

        /// <summary>
        /// 数组分割
        /// </summary>
        /// <param name="listNames"></param>
        /// <param name="avgNum"></param>
        /// <returns></returns>
        public List<string[]> AvgStrings(string[] listNames, int avgNum)
        {
            List<string[]> lists = new List<string[]>();
            int thresholdNum = (int)Math.Ceiling(Conv.ToDecimal(listNames.Length) / Conv.ToDecimal(avgNum));
            for (int i = 0; i < avgNum; i++)
            {
                var end = (thresholdNum * (i + 1) - 1) >= listNames.Length ? listNames.Length - 1 : (thresholdNum * (i + 1) - 1);
                lists.Add(SplitArray(listNames, thresholdNum * i, end));
            }
            return lists;
        }

        /// <summary>
        /// 数据断点截取
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="startIndex">原数组的起始位置</param>
        /// <param name="endIndex">原数组的截止位置</param>
        /// <returns></returns>
        private string[] SplitArray(string[] source, int startIndex, int endIndex)
        {
            try
            {
                string[] result = new string[endIndex - startIndex + 1];
                for (int i = 0; i <= endIndex - startIndex; i++) result[i] = source[i + startIndex];
                return result;
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 数组合并
        /// </summary>
        /// <returns></returns>
        private string MergeStrings(string[] targetStrings)
        {
            var result = targetStrings.Aggregate("", (current, item) => current + (item + ","));
            return result.Trim(',');
        }
    }
}
