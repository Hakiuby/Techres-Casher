using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
    class TablesClient : BaseClient
    {
        public TablesClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
    : base(cache, serializer, errorLogger) { }

        public TableResponse GetListTable(string tableStatus)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ALL_TABLE_OPENING, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("area_id", "-1");
            request.AddQueryParameter("table_status", tableStatus);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<TableResponse>(request,callApiWrapper);
        }
        public TableResponse GetListTableByAreaId(int AreaesId, long branchId, int status)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ALL_TABLE_OPENING, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("area_id", AreaesId.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<TableResponse>(request,callApiWrapper);
        }
        public MergeTableResponse GetListMergeTableByAreaes(int AreaesId, long branchId, int status)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_ALL_TABLE_OPENING, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("area_id", AreaesId.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<MergeTableResponse>(request,callApiWrapper);
        }
        public BaseResponse MoveTable(long tableId, long tableMergeId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_TABLE_MOVE, tableId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            TableMoveWrapper wrapper = new TableMoveWrapper();
            wrapper.TableId = tableMergeId;
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request,callApiWrapper);

        }

        public BaseResponse MergeTable(long tableId, List<int> tables)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_TABLE_MERGE, tableId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            TableMergeWrapper wrapper = new TableMergeWrapper();
            wrapper.TableIds = tables;
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request,callApiWrapper);

        }

        public BaseResponse MoveFoodTable(long tableId, MoveFoodWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_TABLE_MOVE_FOOD, tableId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request,callApiWrapper);

        }
        public BaseResponse CancelTable(long tableId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_TABLE_CLOSE, tableId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request,callApiWrapper);

        }
        public BaseResponse SetTableByOrder(long orderId, long tableMergeId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_SET_TABLE, orderId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            TableMoveWrapper wrapper = new TableMoveWrapper();
            wrapper.TableId = tableMergeId;
            var js = JsonConvert.SerializeObject(wrapper);
            request.AddJsonBody(js);
            WriteLog.logs(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BaseResponse>(request,callApiWrapper);

        }
    }
}
