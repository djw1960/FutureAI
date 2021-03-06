﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv.Entitys
{
    /// <summary>
    /// 基础配置 0-10 公用
    /// </summary>
    public class ApiConfig
    {
        public const int SERVICE_SignIn = 1000;//登录
        public const int SERVICE_SignOut = 1001;//退出登录
        public const int SERVICE_GetMenuList = 1002;//获取登录之后的权限菜单列表
        public const int SERVICE_SignUp = 1003;//退出登录
        

        public const int SERVICE_GetNewsList = 1010;//获取资讯列表
        public const int SERVICE_GetNewsInfo = 1011;//获取资讯详情
        //--仓单
        public const int SERVICE_GetReposList = 1020;//获取仓单列表数据
        //--统计数据
        public const int SERVICE_GetMaterialList = 1030;//获取生产资料统计数据
        public const int SERVICE_GetMaterList_TwoCate = 1031;//获取两个品种数据对比

        public const int SERVICE_GetMomentList = 1040;//获取朋友圈列表
        public const int SERVICE_GetMomentDetail = 1041;//获取帖子详情

        public const int SERVICE_GetAIList = 1060;//当前获取AI列表
        
        public const int SERVICE_GetCateList = 1075;//获取菜单列表
        public const int SERVICE_GetSX_NewsList = 1080;//获取菜单列表

        #region Manager
        public const int Admin_SignIn = 2000;//后台登录
        public const int Admin_SignOut = 2001;//退出后台登录
        public const int Admin_GetMenuList = 2002;//获取登录之后的权限菜单列表
        public const int Admin_GetCategoryList = 2003;//获取分类列表

        public const int Admin_NewsList = 2010;//资讯列表 发布 删除

        public const int Admin_MomentList = 2040;//管理帖子列表
        public const int Admin_MomentCommentList = 2041;//帖子评论管理

        public const int Admin_AIList = 2060;//AI 列表，发布删除
        public const int Admin_AIItemInfo = 2061;//某一个预测详情，用于编辑
        public const int Admin_AIItemAdd = 2062;//新增一条预测

        public const int Admin_GetIP = 2099;//获取IP地址
        #endregion
    }
    public class RespCodeConfig
    {
        public const int TokenTimeOut = -4;
        public const int OK = 1;
        public const int NotOK = 0;
        /// <summary>
        /// 请求正常响应，成功
        /// </summary>
        public const int Normal = 0;
        /// <summary>
        /// 参数为空
        /// </summary>
        public const int NullParam = 1;
        /// <summary>
        /// 参数格式错误
        /// </summary>
        public const int ArgumentExp = 2;
        /// <summary>
        /// 流程错误，一般性错误
        /// </summary>
        public const int Faild = 3;
        /// <summary>
        /// 序异常
        /// </summary>
        public const int ServerError = 4;
        /// <summary>
        /// 验证签名失败
        /// </summary>
        public const int SignError = 10;
    }
    public class LogTypeConfig
    {
        /// <summary>
        /// 产品与收银台交互
        /// </summary>
        public const int PayMC = 1;
        /// <summary>
        /// 收银台与支付服务交互
        /// </summary>
        public const int PayMS = 2;
        /// <summary>
        /// 支付服务给收银台的异步结果通知
        /// </summary>
        public const int NotifyMS = 3;
        /// <summary>
        /// 收银台给产品的异步结果通知
        /// </summary>
        public const int NotifyMC = 4;
        /// <summary>
        /// 查询
        /// </summary>
        public const int Query = 5;
    }
    public class PayContentTypeConfig
    {
        /// <summary>
        /// 返回Url信息，此URL可以在浏览器中打开
        /// </summary>
        public const int QueryString = 1;
        /// <summary>
        /// 返回Form表单信息，可以在客户端自动提交
        /// </summary>
        public const int FormStr = 2;
        /// <summary>
        /// 返回Url信息，需要客户端根据Url生成二维码
        /// </summary>
        public const int ScanURL = 3;
        /// <summary>
        /// 表示走API接口，需要接着调用下个接口
        /// </summary>
        public const int APIKJ = 4;
    }
}
