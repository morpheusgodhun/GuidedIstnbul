namespace GuideAPI.Models.ApiToWebModels.Reservation
{
    public class ReservationListAgent
    {
        public int ReservationID { get; set; }
        public string StartDatePlaceholder_FixedValue { get; set; }
        public string EndDatePlaceholder_FixedValue { get; set; }
        public string FilterButton_FixedValue { get; set; }
        public string DetailButton_FixedValue { get; set; }
        public string ConfirmButton_FixedValue { get; set; }

        public string TableDetailTitle_FixedValue { get; set; }
        public string TableConfirmTitle_FixedValue { get; set; }
        public string TableStatusTitle_FixedValue { get; set; }
        public string TableDateTitle_FixedValue { get; set; }
        public string TableNumberTitle_FixedValue { get; set; }
        public string TableTotalPriceTitle_FixedValue { get; set; }
        public string TableProductTypeTitle_FixedValue { get; set; }
        public string TableProductNameTitle_FixedValue { get; set; }
    }
}
