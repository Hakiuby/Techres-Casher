namespace TechresStandaloneSale.Helpers
{
    public enum OrderSessionStatusEnum
    {
        SESSION_NOT_OPEN = 1, // CHƯA MỞ CA 
        SESSION_OPENED = 2, //ĐÃ MỞ CA 
        SESSION_EXPIRED = 3,  // CA ĐÃ HẾT HẠN 
        SESSION_OPENER = 4 // VÀ LẠI CA CHÍNH 
    }
}
