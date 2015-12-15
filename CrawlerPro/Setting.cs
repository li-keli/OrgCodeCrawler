using System;
using System.IO;

namespace CrawlerPro
{
    /// <summary>
    /// 爬虫外部配置
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// 线程是否工作（用于强制停止任务）
        /// </summary>
        public bool IsWork { set; get; }
        /// <summary>
        /// 剩余任务列表输出
        /// </summary>
        public string OutFile { get { return "outFile"; } }

        private int threadcount;
        /// <summary>
        /// 启用最大线程数
        /// </summary>
        public int ThreadCount { set { threadcount = value; } get { return threadcount; } }

        /// <summary>
        /// 解密算法
        /// </summary>
        public string Key { get { return File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "CrawlerKey"); } }
    }
}
