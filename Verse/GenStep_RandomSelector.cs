using System;
using System.Collections.Generic;

namespace Verse
{
	public class GenStep_RandomSelector : GenStep
	{
		public List<RandomGenStepSelectorOption> options;

		public override void Generate()
		{
			RandomGenStepSelectorOption randomGenStepSelectorOption = this.options.RandomElementByWeight((RandomGenStepSelectorOption opt) => opt.weight);
			if (randomGenStepSelectorOption.genStep != null)
			{
				randomGenStepSelectorOption.genStep.Generate();
			}
			if (randomGenStepSelectorOption.def != null)
			{
				randomGenStepSelectorOption.def.genStep.Generate();
			}
		}
	}
}