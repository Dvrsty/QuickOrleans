using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quick.Common.Extensions
{
    public static class DoubleExtension
    {
        public static decimal ToDecimal(this double d)
        {
            return Convert.ToDecimal(d);
        }

        public static decimal ToDecimal(this double? d)
        {
            if (d == null)
                return 0;
            return Convert.ToDecimal(d);
        }

        public static decimal ToDecimal(this decimal? d)
        {
            if (d == null)
                return 0;

            return Convert.ToDecimal(d);
        }

		public static string ToString(this decimal? d, string format)
		{
			if (!d.HasValue)
				return string.Empty;

			return d.Value.ToString(format);
		}

        /// <summary>
        /// 计算最终赔率
        /// </summary>
        /// <param name="baseMutiple"></param>
        /// <param name="maxValue"></param>
        /// <param name="parentValue"></param>
        /// <returns></returns>
        public static decimal ToGetMutiple(decimal baseMutiple,decimal maxValue,decimal parentValue)
        {
            if (parentValue <= 0)
                return baseMutiple;
            if (maxValue <= parentValue)
                return baseMutiple;
            //var data = Math.Floor(baseMutiple / (maxValue * 10000) * (parentValue * 10000) * 100);
            var data = Math.Floor(baseMutiple/maxValue*parentValue*100);
            decimal mutiple = data / 100;
            return mutiple;
        }
	}
}
