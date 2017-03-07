using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Test
{
    public class ServiceTemp
    {

        //属性
        #region  主键ID
        /// <summary>
        /// 主键ID
        /// </summary>
        public virtual UInt32 ID { get; set; }
        #endregion

        #region  数据状态
        /// <summary>
        /// 数据状态
        /// </summary>
        public virtual Byte State { get; set; }
        #endregion

        #region  创建时间
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }
        #endregion

        #region  更新时间
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }
        #endregion

        #region  服务名称
        /// <summary>
        /// 服务名称
        /// </summary>
        public virtual String ServiceName { get; set; }
        #endregion

        #region  服务上次执行时间
        /// <summary>
        /// 服务上次执行时间
        /// </summary>
        public virtual DateTime? ServiceTime { get; set; }
        #endregion
    }
}
