using Stringer.Data.Domain.Extentions;
using Stringer.Data.Domain.Models.Base;

namespace Stringer.Data.Domain.Models;

public class StringEntity : BaseEntity
{
    public string Text { get; set; }
    public int Length => Text.Length;
    public int SpecialSymbolCount => Text.Count(t => SpecialSymbols.Symbols.Contains(t));
    public int UpperCaseCount => Text.Count(char.IsUpper);

}