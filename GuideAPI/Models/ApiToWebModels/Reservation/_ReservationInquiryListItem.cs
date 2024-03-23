namespace GuideAPI.Models.ApiToWebModels.Reservation
{
    public class _ReservationInquiryListItem
    {
        public int ReservationID { get; set; }
        public string Comment { get; set; }
        public bool Payed { get; set; }
        public string Detail { get; set; }
        public string ReservationNumber { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
