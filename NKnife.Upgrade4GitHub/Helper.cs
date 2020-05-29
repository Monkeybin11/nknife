using System;
using NKnife.Upgrade4Github.Base;
using NKnife.Upgrade4Github.Properties;
using NKnife.Upgrade4Github.Util;

namespace NKnife.Upgrade4Github
{
    /// <summary>
    /// 本组件提供的从github获取更新方法的包装
    /// </summary>
    public static class Helper
    {
        /// <summary>
        ///     获取github的最新更新API的URL
        /// </summary>
        public static string BuildUrl(string userName, string project)
        {
            return Resources.GithubAPI
                .Replace("Username", userName)
                .Replace("Project", project);
        }

        /// <summary>
        /// 从指定的github账户获取指定的项目的最后更新信息
        /// </summary>
        /// <param name="userName">指定的github账户</param>
        /// <param name="project">指定的项目</param>
        /// <param name="latestRelease">最后更新信息</param>
        /// <param name="errorMessage">如果更新失败的异常信息</param>
        /// <returns>获取是否成功</returns>
        public static bool TryGetLatestRelease(string userName, string project, out LatestRelease latestRelease, out string errorMessage)
        {
            latestRelease = null;
            errorMessage = string.Empty;
            //组装更新的github路径
            var myUrl = BuildUrl(userName, project);
            //从github获取更新信息数据字符串
            string restResult;
            try
            {
                restResult = HttpUtil.GetRestResult(myUrl);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return false;
            }

            if (string.IsNullOrEmpty(restResult) || string.IsNullOrWhiteSpace(restResult))
            {
                errorMessage = "项目无返回更新信息（从未更新），或访问超时。";
                return false;
            }

            //解析数据字符串
            try
            {
                latestRelease = fastJSON.JSON.ToObject<LatestRelease>(restResult);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return false;
            }

            return true;
        }
    }
}
