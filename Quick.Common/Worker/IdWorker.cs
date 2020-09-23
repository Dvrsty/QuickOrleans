using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Common.Worker
{
    public class IdWorker
    {
        private static long workerId;
        private static long twepoch = DateTime.UtcNow.Ticks;
        private static long sequence = 0L;
        private static int workerIdBits = 4; 
        public static long maxWorkerId = -1L ^ -1L << workerIdBits;
        private static int sequenceBits = 10; 
        private static int workerIdShift = sequenceBits; 
        private static int timestampLeftShift = sequenceBits + workerIdBits;
        public static long sequenceMask = -1L ^ -1L << sequenceBits; 
        private long lastTimestamp = -1L;

        /// <summary>
        /// 机器码
        /// </summary>
        /// <param name="workerId"></param>
        public IdWorker(long workerId)
        {
            if (workerId > maxWorkerId || workerId < 0)
                throw new Exception(string.Format("worker Id can't be greater than {0} or less than 0 ", workerId));
            IdWorker.workerId = workerId;
        }

        public long nextId()
        {
            lock (this)
            {
                long timestamp = timeGen();
                if (this.lastTimestamp == timestamp)
                { //同一微妙中生成ID
                    IdWorker.sequence = (IdWorker.sequence + 1) & IdWorker.sequenceMask; //用&运算计算该微秒内产生的计数是否已经到达上限
                    if (IdWorker.sequence == 0)
                    {
                        //一微妙内产生的ID计数已达上限，等待下一微妙
                        timestamp = tillNextMillis(this.lastTimestamp);
                    }
                }
                else
                { //不同微秒生成ID
                    IdWorker.sequence = 0; //计数清0
                }
                if (timestamp < lastTimestamp)
                { //如果当前时间戳比上一次生成ID时时间戳还小，抛出异常，因为不能保证现在生成的ID之前没有生成过
                    throw new Exception(string.Format("Clock moved backwards.  Refusing to generate id for {0} milliseconds",
                        this.lastTimestamp - timestamp));
                }
                this.lastTimestamp = timestamp; //把当前时间戳保存为最后生成ID的时间戳
                long nextId = (timestamp - twepoch << timestampLeftShift) | IdWorker.workerId << IdWorker.workerIdShift | IdWorker.sequence;
                return -nextId;
            }
        }

        /// <summary>
        /// 获取下一微秒时间戳
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private long tillNextMillis(long lastTimestamp)
        {
            long timestamp = timeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = timeGen();
            }
            return timestamp;
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns></returns>
        private long timeGen()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}
