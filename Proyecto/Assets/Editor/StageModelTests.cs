using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Proyect;

public class StageModelTest
{ 
	[Test]
    public void CorrectOptionDetectsWinnerTest()
    {
		StageModel stageModel = Singleton<StageModel>.Instance;
		string stringOption = "happy";
		Option Option = new Option(stringOption);
		stageModel.ResetImages();
		stageModel.ResetOptions();
        stageModel.AddOption(stringOption);
		stageModel.LoadImageIntoQueue(stringOption + "1");
		stageModel.Initialize();
        Assert.AreEqual(stageModel.CheckWinner(Option), true);
    }
}
