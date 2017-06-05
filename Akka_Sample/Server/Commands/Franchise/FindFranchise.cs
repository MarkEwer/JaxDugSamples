namespace Server.Commands.Franchise
{
    public class FindFranchise
    {
        public string SearchTerm { get; protected set; }
        public FindFranchise(string searchTerm)
        {
            this.SearchTerm = searchTerm;
        }
    }
}
