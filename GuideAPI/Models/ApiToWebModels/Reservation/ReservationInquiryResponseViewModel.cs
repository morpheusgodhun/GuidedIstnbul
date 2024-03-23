namespace GuideAPI.Models.ApiToWebModels.Reservation
{
    public class ReservationInquiryResponseViewModel
    {
        public string LabelReservationNumber_FixedValue { get; set; }
        public string SearchButton_FixedValue { get; set; }


        public string TableCommentLabel_FixedValue { get; set; }
        public string TablePayLabel_FixedValue { get; set; }
        public string TableCancelLabel_FixedValue { get; set; }
        public string TableUpdateLabel_FixedValue { get; set; }
        public string TableDetailLabel_FixedValue { get; set; }
        public string TableReservationNumberLabel_FixedValue { get; set; }
        public string TableProductNameLabel_FixedValue { get; set; }
        public string TableProductPriceLabel_FixedValue { get; set; }


        public string PayButton_FixedValue { get; set; }
        public string CancelButton_FixedValue { get; set; }
        public string UpdateButton_FixedValue { get; set; }

        public List<_ReservationInquiryListItem> Reservations { get; set; }


    }
}
