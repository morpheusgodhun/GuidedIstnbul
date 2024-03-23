namespace GuideAPI.Models.ApiToWebModels.Reservation
{
    public class _ReservationListItem
    {
        public int ReservationID { get; set; }
        public bool Confirmable { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string ReservationNumber { get; set; }
        public int TotalPrice { get; set; }
        public string TourType { get; set; }
        public string ProductName { get; set; }
    }
}
