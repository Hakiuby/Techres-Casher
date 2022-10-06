using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using TechresStandaloneSale.Helpers;
using TechresStandaloneSale.Interface;
using TechresStandaloneSale.Models.Request;
using TechresStandaloneSale.Models.Response;

namespace TechresStandaloneSale.Services
{
    public class BookingClient : BaseClient
    {
        public BookingClient(ICacheService cache, IDeserializer serializer, IErrorLogger errorLogger)
    : base(cache, serializer, errorLogger) { }

        public BookingResponse CreateBooking(CreateBookingWrapper wrapper)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_CREATE_BOOKING, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<BookingResponse>(request,callApiWrapper);
        }
        public BookingResponse UpdateBooking(EditBookingWrapper wrapper,long id)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_UPDATE_BOOKING, id), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);

            return Get<BookingResponse>(request, callApiWrapper);
        }

        public BookingListResponse GetListBooking(int brandId, long branchId, string fromDate, string toDate, string bookingStatuses, int isJustTakeHavingDeposit, int isJustTakeWaitingConfirmDeposit, int page, long limit)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_BOOKING, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("restaurant_brand_id", brandId.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("from", fromDate);
            request.AddQueryParameter("to", toDate);
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            request.AddQueryParameter("booking_statuses", bookingStatuses);
            request.AddQueryParameter("is_just_take_having_deposit", isJustTakeHavingDeposit.ToString());
            request.AddQueryParameter("is_just_take_waiting_confirm_deposit", isJustTakeWaitingConfirmDeposit.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BookingListResponse>(request, callApiWrapper);
        }
        public BookingResponse ReceiveDeposit(ReceiveDepositWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_RECEIVE_DEPOSIT_BOOKING, wrapper.BookingId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BookingResponse>(request,callApiWrapper);
        }

        public BookingResponse ReturnDeposit(ReturnDepositWrapper wrapper, long bookingId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_RETURN_DEPOSIT_BOOKING, bookingId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BookingResponse>(request, callApiWrapper);
        }
        public BookingResponse ConfirmBooking(long bookingId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_CONFIRM_BOOKING, bookingId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BookingResponse>(request, callApiWrapper);
        }
        public BookingResponse ConfirmDepositBooking(long bookingId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_CONFIRM_DEPOSIT_BOOKING, bookingId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BookingResponse>(request, callApiWrapper);
        }
        public BookingResponse StartBooking(StartBookingWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_START_BOOKING, wrapper.BookingId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BookingResponse>(request, callApiWrapper);
        }
        public BookingResponse SetupBooking(StartBookingWrapper wrapper)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_SETUP_BOOKING, wrapper.BookingId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BookingResponse>(request,callApiWrapper);
        }
        public BookingResponse ArrangeTableBooking(ArrangeTableWrapper wrapper, long bookingId)
        {
            RestRequest request = new RestRequest(string.Format(LinkCallApi.API_ARRANGE_TABLE_BOOKING, bookingId), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js);
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BookingResponse>(request, callApiWrapper);
        }
        public TableBookingResponse GetTableBookingResponse(int areaid, int branchId, long bookingId)
        {
            RestRequest request = new RestRequest(LinkCallApi.API_BOOKING_TABLE, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("area_id", areaid.ToString());
            request.AddQueryParameter("branch_id", branchId.ToString());
            request.AddQueryParameter("booking_id", bookingId.ToString());
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<TableBookingResponse>(request, callApiWrapper);
        }
        public BookingResponse CancelBookingReson(long id, BookingResonCancelResponse wrapper)
        {
            RestRequest request = new RestRequest(string.Format((LinkCallApi.API_CANCEL_BOOKING),id), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var js = JsonConvert.SerializeObject(wrapper);
            WriteLog.logs(js); 
            request.AddJsonBody(js);
            CallApiWrapper callApiWrapper = new CallApiWrapper((long)ProjectIdEnum.ORDER, request);
            return Get<BookingResponse>(request, callApiWrapper);  
         
        }
    }
}
