using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUCPR.AutoRollCredits
{
    [Serializable]
    public class CreditsTexts
    {
        public string title;
        public List<CreditsSubTexts> content;

        public CreditsTexts()
        {
            title = string.Empty;
            content = new List<CreditsSubTexts>();
        }
    }
}
