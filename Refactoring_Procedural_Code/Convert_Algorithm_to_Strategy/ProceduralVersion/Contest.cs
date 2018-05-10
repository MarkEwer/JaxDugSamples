using System.Collections.Generic;

namespace Convert_Algorithm_to_Strategy.ProceduralVersion
{
    public class Contest
    {
        IEnumerable<Register> _registers;
        public Contest(IEnumerable<Register> registers)
        {
            _registers = registers;
        }
        public string GetWinner()
        {
            var winner = this._registers
                .WithMaximum(x => x.CustomerEngagement);
            return winner?.Name;
        }
    }
}
