namespace BeerTapV2.Model
{
    /// <summary>
    /// iQmetrix link relation names
    /// </summary>
    public static class LinkRelations
    {
        /// <summary>
        /// link relation to describe the Identity resource.
        /// </summary>
        public const string Office = "iq:Office";
        public const string Tap = "iq:Tap";
        public const string Keg = "iq:Keg";

        public class Kegs
        {
            public const string TapsUrl = "iq:Kegs";
            public const string GetAllYouWant = "iq:GetAllYouWantBeer";
            public const string GetSomeMore = "iq:GetSomeMore";
            public const string GetWhatIsLeft = "iq:GetWhatIsLeft";
            public const string ReplaceKeg = "iq:ReplaceKeg";
        }
    }
}
