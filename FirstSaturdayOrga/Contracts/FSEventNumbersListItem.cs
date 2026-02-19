namespace FirstSaturdayOrga.Contracts {

    public sealed record FSEventNumbersListItem(
        int Year,
        string Month,
        string City,
        int? AgentsEnl,
        int? AgentsRes,
        int? AgentsTotal,
        string? MostAgents,
        long? ApEnl,
        long? ApRes,
        long? ApTotal,
        string? MostAp
   );
}
