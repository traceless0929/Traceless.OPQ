using System.Collections.Generic;

/// <summary> Copyright (c) 2016 21CN.COM . All rights reserved.<br> /// Description: fudannlp<br>
/// /// Modified log:<br>
/// ------------------------------------------------------<br> Ver. Date Author Description<br>
/// ------------------------------------------------------<br> 1.0 2016年5月3日 kexm created.<br> </summary>
namespace Traceless.Utils.TimeNLP.Enums
{
    /// <summary>
    /// <para>范围时间的默认时间点</para>
    /// <para>@author <a href="mailto:kexm@corp.21cn.com">kexm</a> @version @since 2016年5月3日 ///</para>
    /// </summary>
    public sealed class RangeTimeEnum
    {
        public static readonly RangeTimeEnum day_break = new RangeTimeEnum("day_break", InnerEnum.day_break, 3);
        public static readonly RangeTimeEnum early_morning = new RangeTimeEnum("early_morning", InnerEnum.early_morning, 8); //早
        public static readonly RangeTimeEnum morning = new RangeTimeEnum("morning", InnerEnum.morning, 10); //上午
        public static readonly RangeTimeEnum noon = new RangeTimeEnum("noon", InnerEnum.noon, 12); //中午、午间
        public static readonly RangeTimeEnum afternoon = new RangeTimeEnum("afternoon", InnerEnum.afternoon, 15); //下午、午后
        public static readonly RangeTimeEnum night = new RangeTimeEnum("night", InnerEnum.night, 18); //晚上、傍晚
        public static readonly RangeTimeEnum lateNight = new RangeTimeEnum("lateNight", InnerEnum.lateNight, 20); //晚、晚间
        public static readonly RangeTimeEnum midNight = new RangeTimeEnum("midNight", InnerEnum.midNight, 23); //深夜

        private static readonly IList<RangeTimeEnum> valueList = new List<RangeTimeEnum>();

        static RangeTimeEnum()
        {
            valueList.Add(day_break);
            valueList.Add(early_morning);
            valueList.Add(morning);
            valueList.Add(noon);
            valueList.Add(afternoon);
            valueList.Add(night);
            valueList.Add(lateNight);
            valueList.Add(midNight);
        }

        public enum InnerEnum
        {
            day_break,
            early_morning,
            morning,
            noon,
            afternoon,
            night,
            lateNight,
            midNight
        }

        public readonly InnerEnum innerEnumValue;
        private readonly string nameValue;
        private readonly int ordinalValue;
        private static int nextOrdinal = 0;

        private int hourTime = 0;

        /// <param name="hourTime"></param>
        private RangeTimeEnum(string name, InnerEnum innerEnum, int hourTime)
        {
            this.HourTime = hourTime;

            nameValue = name;
            ordinalValue = nextOrdinal++;
            innerEnumValue = innerEnum;
        }

        /// <returns>the hourTime</returns>
        public int HourTime
        {
            get
            {
                return hourTime;
            }
            set
            {
                this.hourTime = value;
            }
        }

        public static IList<RangeTimeEnum> values()
        {
            return valueList;
        }

        public int ordinal()
        {
            return ordinalValue;
        }

        public override string ToString()
        {
            return nameValue;
        }

        public static RangeTimeEnum valueOf(string name)
        {
            foreach (RangeTimeEnum enumInstance in RangeTimeEnum.valueList)
            {
                if (enumInstance.nameValue == name)
                {
                    return enumInstance;
                }
            }
            throw new System.ArgumentException(name);
        }
    }
}