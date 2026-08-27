public class Division
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; } // e.g., 1 = Premier, 2 = Division 1
    public int PromotionSlots { get; set; }
    public int RelegationSlots { get; set; }

    public ICollection<Player>? Players { get; set; }
}
