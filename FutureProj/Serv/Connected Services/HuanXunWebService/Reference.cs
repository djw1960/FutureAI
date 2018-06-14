﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PayService.Serv.HuanXunWebService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://payat.ips.com.cn/WebService/TradeQuery", ConfigurationName="HuanXunWebService.TradeQueryService")]
    public interface TradeQueryService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://payat.ips.com.cn/WebService/TradeQuery/getTradeByTime", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        string getTradeByTime(string tradeQuery);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://payat.ips.com.cn/WebService/TradeQuery/getTradeByTime", ReplyAction="*")]
        System.Threading.Tasks.Task<string> getTradeByTimeAsync(string tradeQuery);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://payat.ips.com.cn/WebService/TradeQuery/getTradeByMerBillNo", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        string getTradeByMerBillNo(string tradeQuery);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://payat.ips.com.cn/WebService/TradeQuery/getTradeByMerBillNo", ReplyAction="*")]
        System.Threading.Tasks.Task<string> getTradeByMerBillNoAsync(string tradeQuery);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://payat.ips.com.cn/WebService/TradeQuery/getTradeByNo", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        string getTradeByNo(string tradeQuery);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://payat.ips.com.cn/WebService/TradeQuery/getTradeByNo", ReplyAction="*")]
        System.Threading.Tasks.Task<string> getTradeByNoAsync(string tradeQuery);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface TradeQueryServiceChannel : PayService.Serv.HuanXunWebService.TradeQueryService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TradeQueryServiceClient : System.ServiceModel.ClientBase<PayService.Serv.HuanXunWebService.TradeQueryService>, PayService.Serv.HuanXunWebService.TradeQueryService {
        
        public TradeQueryServiceClient() {
        }
        
        public TradeQueryServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TradeQueryServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TradeQueryServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TradeQueryServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string getTradeByTime(string tradeQuery) {
            return base.Channel.getTradeByTime(tradeQuery);
        }
        
        public System.Threading.Tasks.Task<string> getTradeByTimeAsync(string tradeQuery) {
            return base.Channel.getTradeByTimeAsync(tradeQuery);
        }
        
        public string getTradeByMerBillNo(string tradeQuery) {
            return base.Channel.getTradeByMerBillNo(tradeQuery);
        }
        
        public System.Threading.Tasks.Task<string> getTradeByMerBillNoAsync(string tradeQuery) {
            return base.Channel.getTradeByMerBillNoAsync(tradeQuery);
        }
        
        public string getTradeByNo(string tradeQuery) {
            return base.Channel.getTradeByNo(tradeQuery);
        }
        
        public System.Threading.Tasks.Task<string> getTradeByNoAsync(string tradeQuery) {
            return base.Channel.getTradeByNoAsync(tradeQuery);
        }
    }
}
