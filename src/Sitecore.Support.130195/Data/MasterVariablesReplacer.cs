namespace Sitecore.Support.Data
{
    using Sitecore;
    using Sitecore.Collections;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Text;
    using System;
    using System.Collections.Generic;
    public class MasterVariablesReplacer : Sitecore.Data.MasterVariablesReplacer
    {
        public MasterVariablesReplacer() : base()
        {

        }

        protected override string ReplaceValues(string text, Func<string> defaultName, Func<string> defaultId, Func<string> defaultParentName, Func<string> defaultParentId)
        {
            if (text.Length == 0 || text.IndexOf('$') < 0)
            {
                return text;
            }
            ReplacerContext context = this.GetContext();
            if (context != null)
            {
                foreach (KeyValuePair<string, string> value in context.Values)
                {
                    text = text.Replace(value.Key, value.Value);
                }
            }
            text = ReplaceWithDefault(text, "$name", defaultName, context);
            text = ReplaceWithDefault(text, "$id", defaultId, context);
            text = ReplaceWithDefault(text, "$parentid", defaultParentId, context);
            text = ReplaceWithDefault(text, "$parentname", defaultParentName, context);
            text = ReplaceWithDefault(text, "$date", () => DateUtil.ToIsoDate(DateUtil.ToServerTime(DateTime.UtcNow).Date, false, true), context);
            text = ReplaceWithDefault(text, "$time", () => DateUtil.IsoNowTime, context);
            text = ReplaceWithDefault(text, "$now", () => DateUtil.IsoNow, context);
            return text;
        }
    }
}