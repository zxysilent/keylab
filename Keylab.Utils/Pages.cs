using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.Utils {
    public class Pages {
        /// <summary>
        /// 生成分页字符串
        /// </summary>
        /// <param name="ps">一页多少条</param>
        /// <param name="pi">当前页</param>
        /// <param name="rc">总条数</param>
        /// <returns>分页字符串</returns>
        public static string Navigate(int pi, int ps, int rc) {
            ps = ps == 0 ? 3 : ps;
            var totalPages = Math.Max((rc + ps - 1) / ps, 1); //总页数
            var output = new StringBuilder();
            if (totalPages > 1) {
                if (pi != 1) {//处理首页连接
                    output.AppendFormat("<li><a href='?pi=1&ps={0}'>home</a></li> ", ps);
                }
                if (pi > 1) {//处理上一页的连接
                    output.AppendFormat("<li><a href='?pi={0}&ps={1}'>&laquo;</a></li> ", pi - 1, ps);
                }
                output.Append(" ");
                int currint = 3;
                for (int i = 0; i <= 7; i++) {//一共最多显示10个页码，前面5个，后面5个
                    if ((pi + i - currint) >= 1 && (pi + i - currint) <= totalPages) {
                        if (currint == i) {//当前页处理
                            //output.Append(string.Format("[{0}]", pi));
                            output.AppendFormat("<li><a class='am-active' href='?pi={0}&ps={1}'>{2}</a></li> ", pi, ps, pi);
                        } else {//一般页处理
                            output.AppendFormat("<li><a href='?pi={0}&ps={1}'>{2}</a></li> ", pi + i - currint, ps, pi + i - currint);
                        }
                    }
                }
                if (pi < totalPages) {//处理下一页的链接
                    output.AppendFormat("<li><a href='?pi={0}&ps={1}'>&raquo;</a></li> ", pi + 1, ps);
                }
                output.Append(" ");
                if (pi != totalPages) {
                    output.AppendFormat("<li><a href='?pi={0}&ps={1}'>foot</a></li> ", totalPages, ps);
                }
                output.Append(" ");
            }
            output.AppendFormat("第{0}页 / 共{1}页（共{2}条数据）", pi, totalPages, rc);//这个统计加不加都行
            return output.ToString();
        }
    }
}
