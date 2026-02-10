namespace FirstSaturdayOrga.Contracts {

    public sealed record FSEventPostCovidListItem(
        string Month,
        string City,
        string Province,
        int? AgentsEnl,
        int? AgentsRes,
        int? AgentsTotal,
        long? ApEnl,
        long? ApRes,
        long? ApTotal
   );
}
