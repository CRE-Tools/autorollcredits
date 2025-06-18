using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUCPR.AutoRollCredits
{
    [Serializable]
    public class CreditsSubTexts
    {
        public string subTitle;
        public List<String> names;

        public CreditsSubTexts()
        {
            subTitle = string.Empty;
            names = new List<String>();
        }
    }
}
