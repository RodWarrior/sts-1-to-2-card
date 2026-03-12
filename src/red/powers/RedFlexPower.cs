using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using sts1to2card.src.red.cards;

namespace sts1to2card.src.red.powers;

public class RedFlexPower : TemporaryStrengthPower
{
	public override AbstractModel OriginModel => ModelDb.Card<RedFlex>();
}